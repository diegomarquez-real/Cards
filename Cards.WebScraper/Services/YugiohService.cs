using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Cards.WebScraper.Services
{
    public class YugiohService : Abstractions.IYugiohService
    {
        private readonly ILogger<YugiohService> _logger;
        private readonly IOptions<Options.AppSettingsOptions> _options;
        private readonly Abstractions.IProgressService _progressService;
        private readonly Api.Client.Abstractions.Yugioh.ISetClient _setClient;
        private readonly Api.Client.Abstractions.Yugioh.ICardClient _cardClient;
        private readonly Api.Client.Abstractions.Yugioh.IAttributeClient _attributeClient;
        private readonly Api.Client.Abstractions.Yugioh.ISpeciesClient _speciesClient;
        private readonly Api.Client.Abstractions.Yugioh.IPowerClient _powerClient;
        private readonly Api.Client.Abstractions.Yugioh.IEffectTypeClient _effectTypeClient;
        private readonly Api.Client.Abstractions.Yugioh.ICardEffectTypeAssociationClient _cardEffectTypeAssociationClient;
        private readonly Api.Client.Abstractions.Yugioh.ICardSetAssociationClient _cardSetAssociationClient;
        private readonly Api.Client.Abstractions.Yugioh.ICardSpeciesAssociationClient _cardSpeciesAssociationClient;

        public YugiohService(ILogger<YugiohService> logger,
            IOptions<Options.AppSettingsOptions> options,
            Abstractions.IProgressService progressService,
            Api.Client.Abstractions.Yugioh.ISetClient setClient,
            Api.Client.Abstractions.Yugioh.ICardClient cardClient,
            Api.Client.Abstractions.Yugioh.IAttributeClient attributeClient,
            Api.Client.Abstractions.Yugioh.ISpeciesClient speciesClient,
            Api.Client.Abstractions.Yugioh.IPowerClient powerClient,
            Api.Client.Abstractions.Yugioh.IEffectTypeClient effectTypeClient,
            Api.Client.Abstractions.Yugioh.ICardEffectTypeAssociationClient cardEffectTypeAssociationClient,
            Api.Client.Abstractions.Yugioh.ICardSetAssociationClient cardSetAssociationClient,
            Api.Client.Abstractions.Yugioh.ICardSpeciesAssociationClient cardSpeciesAssociationClient)
        {
            _logger = logger;
            _options = options;
            _progressService = progressService;
            _setClient = setClient;
            _cardClient = cardClient;
            _attributeClient = attributeClient;
            _speciesClient = speciesClient;
            _powerClient = powerClient;
            _effectTypeClient = effectTypeClient;
            _cardEffectTypeAssociationClient = cardEffectTypeAssociationClient;
            _cardSetAssociationClient = cardSetAssociationClient;
            _cardSpeciesAssociationClient = cardSpeciesAssociationClient;
        }

        public async Task AddCardsFullAsync()
        {
            Uri uri = new Uri(_options.Value.YugiohDbUrl);
            string baseUrl = uri.GetLeftPart(UriPartial.Authority);
            var currentWebHtml = new HtmlWeb();
            var currentPageHtml = currentWebHtml.Load(uri.OriginalString);
            var packHtmlNodes = currentPageHtml.DocumentNode.SelectNodes("//div[@id=\"card_list_1\"]//div[@id=\"list_title_1\"]//div[contains(@class, \"list_body\")]//div[contains(@class, \"pack\")]//input[contains(@class, \"link_value\")]");
            var packUrls = packHtmlNodes.Select(x => HttpUtility.HtmlDecode(baseUrl + x.Attributes["value"].Value)).ToList();
            var cardNamesPairs = new Dictionary<string, CardLineItem>(StringComparer.OrdinalIgnoreCase); 
            var attributeNameIdPairs = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase); 
            var effectTypeNameIdPairs = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase); 
            var speciesNameIdPairs = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase); 
            var setNameIdPairs = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase); 

            for (int i = 0; i < packUrls.Count; i++)
            {
                currentWebHtml.Load(packUrls[i]);
                currentPageHtml = currentWebHtml.Load(packUrls[i]);

                // Location of the set name and set date.
                var titleBar = currentPageHtml.DocumentNode.SelectSingleNode("//header[@id=\"broad_title\"]//div");
                var setName = titleBar.SelectSingleNode(".//h1").InnerText;
                var setDate = Regex.Match(titleBar.SelectSingleNode(".//p[@id=\"previewed\"]").InnerText.RemoveWhitespaceCharacters(), @"\d{2}\/\d{2}\/\d{4}").Value;
                var setLineItem = new SetLineItem
                {
                    Name = setName,
                    Date = DateTime.Parse(setDate)
                };

                var cardHtmlNodes = currentPageHtml.DocumentNode.SelectNodes("//div[@id=\"card_list\"]//div[contains(@class, \"t_row\")]");
                this.BuildCards(cardHtmlNodes, setLineItem, cardNamesPairs);           
                _progressService.ProgressBar(i + 1, packUrls.Count);
            }

            foreach (var cnp in cardNamesPairs)
            {
                Guid cardId, attributeId;

                var card = await _cardClient.GetCardByNameAsync(cnp.Key);

                if (card != null)
                {
                    //TODO: Update card
                    continue;
                }

                #region Attribute

                if (attributeNameIdPairs.ContainsKey(cnp.Value.Attribute))
                {
                    attributeId = attributeNameIdPairs[cnp.Value.Attribute];
                }
                else
                {
                    var attribute = await _attributeClient.GetAttributeByNameAsync(cnp.Value.Attribute);

                    if (attribute != null)
                    {
                        attributeId = attribute.AttributeId;
                    }
                    else
                    {
                        attributeId = await _attributeClient.CreateAttributeAsync(new Api.Models.Yugioh.Create.CreateAttributeModel
                        {
                            Name = cnp.Value.Attribute
                        });
                    }

                    attributeNameIdPairs.Add(cnp.Value.Attribute, attributeId);
                }

                #endregion

                #region Card

                cardId = await _cardClient.CreateCardAsync(new Api.Models.Yugioh.Create.CreateCardModel
                {
                    Name = cnp.Value.Name,
                    Description = cnp.Value.Description,
                    AttributeId = attributeId,
                });

                #endregion

                #region Monster Or Spell/Trap

                if (cnp.Value.Attribute.Equals("TRAP", StringComparison.OrdinalIgnoreCase) ||
                   cnp.Value.Attribute.Equals("SPELL", StringComparison.OrdinalIgnoreCase))
                {
                    Guid effectTypeId;

                    if (!String.IsNullOrEmpty(cnp.Value.Effect))
                    {
                        if (effectTypeNameIdPairs.ContainsKey(cnp.Value.Effect))
                        {
                            effectTypeId = effectTypeNameIdPairs[cnp.Value.Effect];
                        }
                        else
                        {
                            var effectType = await _effectTypeClient.GetEffectTypeByNameAsync(cnp.Value.Effect);

                            if (effectType != null)
                            {
                                effectTypeId = effectType.EffectTypeId;
                            }
                            else
                            {
                                effectTypeId = await _effectTypeClient.CreateEffectTypeAsync(new Api.Models.Yugioh.Create.CreateEffectTypeModel
                                {
                                    Name = cnp.Value.Effect
                                });
                            }

                            effectTypeNameIdPairs.Add(cnp.Value.Effect, effectTypeId);
                        }

                        await _cardEffectTypeAssociationClient.CreateCardEffectTypeAssociationAsync(new Api.Models.Yugioh.Create.CreateCardEffectTypeAssociationModel
                        {
                            CardId = cardId,
                            EffectTypeId = effectTypeId
                        });
                    }  
                }
                else
                {
                    if (cnp.Value.Species?.Any() == true)
                    {
                        foreach (var s in cnp.Value.Species)
                        {
                            Guid speciesId;

                            if (speciesNameIdPairs.ContainsKey(s))
                            {
                                speciesId = speciesNameIdPairs[s];
                            }
                            else
                            {
                                var species = await _speciesClient.GetSpeciesByNameAsync(s);

                                if (species != null)
                                {
                                    speciesId = species.SpeciesId;
                                }
                                else
                                {
                                    speciesId = await _speciesClient.CreateSpeciesAsync(new Api.Models.Yugioh.Create.CreateSpeciesModel
                                    {
                                        Name = s
                                    });
                                }

                                speciesNameIdPairs.Add(s, speciesId);
                            }

                            await _cardSpeciesAssociationClient.CreateCardSpeciesAssociationAsync(new Api.Models.Yugioh.Create.CreateCardSpeciesAssociationModel
                            {
                                CardId = cardId,
                                SpeciesId = speciesId
                            });
                        }
                    }

                    Guid powerId = await _powerClient.CreatePowerAsync(new Api.Models.Yugioh.Create.CreatePowerModel
                    {
                        CardId = cardId,
                        Level = cnp.Value.Level,
                        Rank = cnp.Value.Rank,
                        Link = cnp.Value.Link,
                        PScale = cnp.Value.PScale,
                        Attack = cnp.Value.AtkPower,
                        Defense = cnp.Value.DefPower,
                    });
                }

                #endregion

                #region Set

                foreach(var s in cnp.Value.Sets)
                {
                    Guid setId;

                    if (setNameIdPairs.ContainsKey(s.Name))
                    {
                        setId = setNameIdPairs[s.Name];
                    }
                    else
                    {
                        var set = await _setClient.GetSetByNameAsync(s.Name);

                        if (set != null)
                        {
                            setId = set.SetId;
                        }
                        else
                        {
                            setId = await _setClient.CreateSetAsync(new Api.Models.Yugioh.Create.CreateSetModel
                            {
                                Name = s.Name,
                                ReleaseDate = s.Date
                            });
                        }

                        setNameIdPairs.Add(s.Name, setId);
                    }

                    await _cardSetAssociationClient.CreateCardSetAssociationAsync(new Api.Models.Yugioh.Create.CreateCardSetAssociationModel
                    {
                        CardId = cardId,
                        SetId = setId
                    });
                }

                #endregion
            }
        }

        public async Task AddCardImagesFullAsync()
        {
            int pageNumber = 1;
            int cardCount = 0;
            Api.Models.Yugioh.Query.CardQueryModel cardQueryModel = new Api.Models.Yugioh.Query.CardQueryModel()
            {
                PageNumber = pageNumber,
                PageSize = 100
            };
            List<Api.Models.Yugioh.CardModel> allCards = new List<Api.Models.Yugioh.CardModel>();
            List<Api.Models.Yugioh.CardModel> cards = await _cardClient.GetAllOrQueryAsync(cardQueryModel);
            allCards.AddRange(cards);
            pageNumber++;
            cardQueryModel.PageNumber = pageNumber;
            cardCount = cards.Count;
            while (cardCount >= 100)
            {
                cards = await _cardClient.GetAllOrQueryAsync(cardQueryModel);
                allCards.AddRange(cards);
                pageNumber++;
                cardQueryModel.PageNumber = pageNumber;
                cardCount = cards.Count;
            }
            await this.SaveImagesAsync(allCards);
        }

        private async Task SaveImagesAsync(List<Api.Models.Yugioh.CardModel> cards)
        {
            Dictionary<Guid, List<string>> cardIdUrlPairs = new Dictionary<Guid, List<string>>();
            foreach (Api.Models.Yugioh.CardModel card in cards)
            {
                cardIdUrlPairs.Add(card.CardId, new List<string>() 
                { 
                    _options.Value.YugiohImgDbUrl + card.Name.Replace("\"", "").Replace(" ", "_").Replace("!", "").Replace("?", "").Replace(":", "").Replace("'", "").Replace(".", "").Replace("&", "").Replace(",", "").Replace("@", "").Replace(@"/", "").Replace("#", "").Replace("(", "").Replace(")", "").Replace("<", "").Replace(">", ""), 
                    _options.Value.YugiohImgDbUrl2 + card.Name.Replace("\"", "").Replace(" ", "_").Replace("?", HttpUtility.UrlEncode("?")).Replace("'", HttpUtility.UrlEncode("'")).Replace("&", HttpUtility.UrlEncode("&")).Replace("<", "").Replace(">", ""),
                    _options.Value.YugiohImgDbUrl2 + card.Name.Replace(" ", "_").Replace("?", HttpUtility.UrlEncode("?")).Replace("'", HttpUtility.UrlEncode("'")).Replace("&", HttpUtility.UrlEncode("&")).Replace("<", "").Replace(">", ""),
                    _options.Value.YugiohImgDbUrl2 + card.Name.Replace("\"", "").Replace(" ", "_").Replace("?", HttpUtility.UrlEncode("?")).Replace("'", HttpUtility.UrlEncode("'")).Replace("&", HttpUtility.UrlEncode("&")).Replace("<", "").Replace(">", "") + "_(card)"
                });
            }
            ImageLineItem[] cardImageLineItems = await Task.WhenAll(cardIdUrlPairs.Select(x => this.GetCardImgSrcDataAsync(x)));
            string baseOutputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Cards.WebScraper\\Images\\Yugioh\\");
            Directory.CreateDirectory(baseOutputDirectory);
            List<string> imagePaths = new List<string>();
            using (var httpClient = new HttpClient())
            {
                List<Task> cardTasks = new List<Task>();
                foreach (ImageLineItem cardImageLineItem in cardImageLineItems)
                {
                    string cardDirectory = Path.Combine(baseOutputDirectory, cardImageLineItem.CardId.ToString());
                    Directory.CreateDirectory(cardDirectory);
                    cardTasks.AddRange(cardImageLineItem.CardImageSrcs.Select(x => this.DownloadCardImageAsync(cardDirectory, x, httpClient)));
                }
                await Task.WhenAll(cardTasks);
            };
        }

        private async Task DownloadCardImageAsync(string cardDirectory, string cardImageSrc, HttpClient httpClient)
        {
            try
            {
                string fileName = Path.GetFileName(new Uri(cardImageSrc).LocalPath);
                string savePath = Path.Combine(cardDirectory, fileName);
                if (File.Exists(savePath))
                {
                    return;
                }
                byte[] imageBytes = await httpClient.GetByteArrayAsync(cardImageSrc);
                await File.WriteAllBytesAsync(savePath, imageBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to download img from ({cardImageSrc}).");
            }
            
        }

        private async Task<ImageLineItem> GetCardImgSrcDataAsync(KeyValuePair<Guid, List<string>> cardIdUrlPair/*, HtmlWeb htmlWeb*/)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            List<string> cardImageSrcs = new List<string>();
            HtmlDocument cardHtmlDoc = htmlWeb.Load(cardIdUrlPair.Value[0]);
            HtmlNodeCollection altArtsNodes = cardHtmlDoc.DocumentNode.SelectNodes("//div[@id=\"card-images-container\"]//div[@id=\"alternative-arts\"]//img[@id=\"alternative-card-image\"]");
            if (altArtsNodes?.Any() != true)
            {
                HtmlNode cardImgNode = cardHtmlDoc.DocumentNode.SelectSingleNode("//div[@id=\"card-images-container\"]//img[@id=\"card-image\"]");
                int cardUrlIndex = 1;
                while (cardImgNode == null && cardUrlIndex < cardIdUrlPair.Value.Count)
                {
                    cardImgNode = await this.GetNodeByUrlAsync(cardIdUrlPair.Value[cardUrlIndex], "//div[contains(@class, \"cardtable-main_image-wrapper\")]//a//img", htmlWeb);
                    cardUrlIndex++;
                }
                cardImageSrcs.Add(cardImgNode.GetAttributeValue("src", String.Empty));
            }
            else
            {
                foreach (HtmlNode altArtsNode in altArtsNodes)
                {
                    cardImageSrcs.Add(altArtsNode.GetAttributeValue("src", String.Empty));
                }
            }
            return new ImageLineItem() { CardId = cardIdUrlPair.Key, CardImageSrcs = cardImageSrcs };
        }

        private async Task<HtmlNode> GetNodeByUrlAsync(string url, string xPath, HtmlWeb htmlWeb)
        {
            HtmlDocument htmlDoc = await htmlWeb.LoadFromWebAsync(url);
            return htmlDoc.DocumentNode.SelectSingleNode(xPath);
        }

        private void BuildCards(HtmlNodeCollection cardHtmlNodes, SetLineItem setLineItem, Dictionary<string, CardLineItem> container)
        {
            for (int i = 0; i < cardHtmlNodes.Count; i++)
            {
                var cardLineItem = this.BuildCardLineItem(cardHtmlNodes[i]);

                if (container.ContainsKey(cardLineItem.Name))
                {
                    if (!container[cardLineItem.Name].Sets.Any(x => x.Name.Equals(setLineItem.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        container[cardLineItem.Name].Sets.Add(setLineItem);
                    }
                }
                else
                {
                    cardLineItem.Sets.Add(setLineItem);

                    container.Add(cardLineItem.Name, cardLineItem);
                }
            }
        }

        private CardLineItem BuildCardLineItem(HtmlNode cardHtmlNode)
        {
            var cardName = cardHtmlNode.SelectSingleNode(".//span[contains(@class, \"card_name\")]").InnerText.RemoveWhitespaceCharacters();
            int updatedFromIndex = cardName.IndexOf("Updated from");
            cardName = updatedFromIndex < 0 ? cardName : cardName.Substring(0, updatedFromIndex - 2);
            var cardDescription = cardHtmlNode.SelectSingleNode(".//dd[contains(@class, \"box_card_text\")]").InnerHtml.RemoveWhitespaceCharacters().Replace("<br>", "\r\n");
            var cardSpecs = cardHtmlNode.SelectSingleNode(".//dd[contains(@class, \"box_card_spec\")]");
            var cardAttribute = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_attribute\")]//span")?.InnerText;
            var cardSpecies = cardSpecs.SelectSingleNode(".//span[contains(@class, \"card_info_species_and_other_item\")]//span")?.InnerText.ToYugiohCardSpecies();
            var cardEffect = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_effect\")]//span")?.InnerText.RemoveWhitespaceCharacters();
            var cardAtkPower = cardSpecs.SelectSingleNode(".//span[contains(@class, \"atk_power\")]//span")?.InnerText.ToYugiohCardNumericValue();
            var cardDefPower = cardSpecs.SelectSingleNode(".//span[contains(@class, \"def_power\")]//span")?.InnerText.ToYugiohCardNumericValue();
            var cardLevel = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_level_rank\") and contains(@class, \"level\")]//span")?.InnerText.ToYugiohCardNumericValue();
            var cardRank = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_level_rank\") and contains(@class, \"rank\")and contains(@class, \"rank\")]//span")?.InnerText.ToYugiohCardNumericValue();
            var cardLink = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_linkmarker\")]//span")?.InnerText.ToYugiohCardNumericValue();
            var cardPScale = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_pen_scale\")]//span")?.InnerText.ToYugiohCardNumericValue();

            return new CardLineItem
            {
                Name = cardName,
                Description = cardDescription,
                Attribute = cardAttribute,
                Level = cardLevel,
                Rank = cardRank,
                Link = cardLink,
                PScale = cardPScale,
                Species = cardSpecies,
                Effect = cardEffect,
                AtkPower = cardAtkPower,
                DefPower = cardDefPower,
            };
        }

        public class CardLineItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Attribute { get; set; }
            public int? Level { get; set; }
            public int? Rank { get; set; }
            public int? Link { get; set; }
            public int? PScale { get; set; }
            public List<string>? Species { get; set; }
            public string? Effect { get; set; }
            public int? AtkPower { get; set; }
            public int? DefPower { get; set; }
            public List<SetLineItem> Sets { get; set; } = new();
        }

        public class SetLineItem
        {
            public string Name { get; set; }
            public DateTime Date { get; set; }
        }

        public class ImageLineItem
        {
            public Guid CardId { get; set; }
            public List<string> CardImageSrcs { get; set; }
        }
    }
}