using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Web;

namespace Cards.WebScraper.Services
{
    public class YugiohService : Abstractions.IYugiohService
    {
        private readonly IOptions<Options.AppSettingsOptions> _options;

        public YugiohService(IOptions<Options.AppSettingsOptions> options)
        {
            _options = options; 
        }

        public async Task AddCardsFullAsync()
        {
            Uri uri = new Uri(_options.Value.YugiohDbUrl);
            string baseUrl = uri.GetLeftPart(UriPartial.Authority);
            var currentWebHtml = new HtmlWeb();
            var currentPageHtml = currentWebHtml.Load(uri.OriginalString);
            var packHtmlNodes = currentPageHtml.DocumentNode.SelectNodes("//div[@id=\"card_list_1\"]//div[@id=\"list_title_1\"]//div[contains(@class, \"list_body\")]//div[contains(@class, \"pack\")]//input[contains(@class, \"link_value\")]");
            var packUrls = packHtmlNodes.Select(x => HttpUtility.HtmlDecode(baseUrl + x.Attributes["value"].Value)).ToList();
            var cards = new List<CardLineItem>();

            for (int i = 0; i < packUrls.Count; i++)
            {
                currentWebHtml.Load(packUrls[i]);
                currentPageHtml = currentWebHtml.Load(packUrls[i]);
                var cardHtmlNodes = currentPageHtml.DocumentNode.SelectNodes("//div[@id=\"card_list\"]//div[contains(@class, \"t_row\")]");
                cards.AddRange(this.BuildCards(cardHtmlNodes));
            }
        }

        private List<CardLineItem> BuildCards(HtmlNodeCollection cardHtmlNodes)
        {
            var cardLineItems = new List<CardLineItem>();

            for (int i = 0; i < cardHtmlNodes.Count; i++)
            {
                cardLineItems.Add(this.BuildCardLineItem(cardHtmlNodes[i]));
            }

            return cardLineItems;
        }

        private CardLineItem BuildCardLineItem(HtmlNode cardHtmlNode)
        {
            var cardName = cardHtmlNode.SelectSingleNode(".//span[contains(@class, \"card_name\")]").InnerText.RemoveWhitespaceCharacters();
            var cardDescription = cardHtmlNode.SelectSingleNode(".//dd[contains(@class, \"box_card_text\")]").InnerText.RemoveWhitespaceCharacters();
            var cardSpecs = cardHtmlNode.SelectSingleNode(".//dd[contains(@class, \"box_card_spec\")]");
            var cardAttribute = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_attribute\")]//span")?.InnerText;
            var cardLevel = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_level_rank\")]//span")?.InnerText.ToYugiohCardLevel();
            var cardSpecies = cardSpecs.SelectSingleNode(".//span[contains(@class, \"card_info_species_and_other_item\")]//span")?.InnerText.ToYugiohCardSpecies();
            var cardEffect = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_effect\")]//span")?.InnerText.RemoveWhitespaceCharacters();
            var cardAtkPower = cardSpecs.SelectSingleNode(".//span[contains(@class, \"atk_power\")]//span")?.InnerText.ToYugiohCardAtkPower();
            var cardDefPower = cardSpecs.SelectSingleNode(".//span[contains(@class, \"def_power\")]//span")?.InnerText.ToYugiohCardDefPower();

            return new CardLineItem
            {
                Name = cardName,
                Description = cardDescription,
                Attribute = cardAttribute,
                Level = cardLevel,
                Species = cardSpecies,
                Effect = cardEffect,
                AtkPower = cardAtkPower,
                DefPower = cardDefPower
            };
        }

        public class CardLineItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Attribute { get; set; }
            public int? Level { get; set; }
            public string[]? Species { get; set; }
            public string Effect { get; set; }
            public int? AtkPower { get; set; }
            public int? DefPower { get; set; }
        }
    }
}