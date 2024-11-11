using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Web;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Cards.WebScraper.Services
{
    public class YugiohService : Abstractions.IYugiohService
    {
        private readonly IOptions<Options.AppSettingsOptions> _options;
        private readonly IWebDriver _driver;

        public YugiohService(IOptions<Options.AppSettingsOptions> options)
        {
            _options = options;
            _driver = new ChromeDriver();   
        }

        public async Task AddCardsFullAsync()
        {

            Uri uri = new Uri(_options.Value.YugiohDbUrl);
            // Get the base part of the URL
            string baseUrl = uri.GetLeftPart(UriPartial.Authority);


            await _driver.Navigate().GoToUrlAsync(uri);
            // Look for the tab row consisting of ["Sort by Release Date", "Products", "Perks/Bundles"]
            var tabLink = _driver.FindElement(By.ClassName("tablink"));
            // Look for the actual link specific to the "Products" tab.
            var productsLink = tabLink.FindElements(By.TagName("li"))
                                      .FirstOrDefault(x => 
                                          x.FindElements(By.TagName("span"))
                                           .FirstOrDefault(x => x.Text.Equals("Products")) != null
                                      );

            if (productsLink == null)
                return;

            productsLink.Click();
            var currentPageHtml = new HtmlDocument();
            currentPageHtml.LoadHtml(_driver.PageSource);
            var packHtmlNodes = currentPageHtml.DocumentNode.SelectNodes("//div[@id=\"list_title_1\"]//div[contains(@class, \"list_body\")]//div[contains(@class, \"pack\")]//input[contains(@class, \"link_value\")]");
            // Create a list of pack URLs to navigate to.
            var packUrls = packHtmlNodes.Select(x => HttpUtility.HtmlDecode(baseUrl + x.Attributes["value"].Value)).ToList();
            var cards = new List<CardLineItem>();

            for (int i = 0; i < packUrls.Count; i++)
            {
                if(_driver.WindowHandles.Count > 1)
                {
                    // If there are multiple tabs open I'm assuming that I'm in the card list page.
                    // Navigate to the pack URL in the current tab.
                    await _driver.Navigate().GoToUrlAsync(packUrls[i]);
                }
                else
                {
                    // If there is only one tab open I'm assuming that I'm in the products page.
                    // Open a new tab and navigate to the pack URL.
                    await _driver.NavigateNewTabAsync(packUrls[i]);
                }
                currentPageHtml.LoadHtml(_driver.PageSource);
                var cardHtmlNodes = currentPageHtml.DocumentNode.SelectNodes("//div[@id=\"card_list\"]//div[contains(@class, \"t_row\")]");
                cards.AddRange(this.BuildCards(cardHtmlNodes));
            }

            _driver.Quit();
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