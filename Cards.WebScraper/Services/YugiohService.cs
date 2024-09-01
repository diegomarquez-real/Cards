using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public async Task GetRecentCardsAsync()
        {
            await _driver.Navigate().GoToUrlAsync(_options.Value.YugiohDbUrl);
            var cardUpdateList = _driver.FindElement(By.Id("update_list"));
            var mostRecentCardsLink = cardUpdateList.FindElement(By.ClassName("t_body")).FindElement(By.ClassName("t_row"));
            var cardListReleaseDate = mostRecentCardsLink.FindElement(By.ClassName("time")).Text;
            var cardSetName = mostRecentCardsLink.FindElement(By.ClassName("pack")).FindElement(By.TagName("p")).Text;
            mostRecentCardsLink.Click();
            var currentPageHtml = new HtmlDocument();
            currentPageHtml.LoadHtml(_driver.PageSource);
            var cardList = currentPageHtml.DocumentNode.SelectSingleNode("//*[@id=\"card_list\"]");
            var cards = cardList.SelectNodes(".//*[contains(@class, \"t_row\")]");
            foreach (var card in cards)
            {
                var img = card.SelectSingleNode(".//div[contains(@class, \"box_card_img\")]//img");
                var cardName = card.SelectSingleNode(".//span[contains(@class, \"card_name\")]").InnerText.RemoveWhitespaceCharacters();
                var cardDescription = card.SelectSingleNode(".//dd[contains(@class, \"box_card_text\")]").InnerText.RemoveWhitespaceCharacters();
                var cardSpecs = card.SelectSingleNode(".//dd[contains(@class, \"box_card_spec\")]");
                var cardAttributes = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_attribute\")]//span")?.InnerText;
                var cardLevel = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_level_rank\")]//span")?.InnerText.ToYugiohCardLevel();
                var cardSpecies = cardSpecs.SelectSingleNode(".//span[contains(@class, \"card_info_species_and_other_item\")]//span")?.InnerText.ToYugiohCardSpecies();
                var cardEffect = cardSpecs.SelectSingleNode(".//span[contains(@class, \"box_card_effect\")]//span")?.InnerText.RemoveWhitespaceCharacters();
                var cardAtkPower = cardSpecs.SelectSingleNode(".//span[contains(@class, \"atk_power\")]//span")?.InnerText.ToYugiohCardAtkPower();
                var cardDefPower = cardSpecs.SelectSingleNode(".//span[contains(@class, \"def_power\")]//span")?.InnerText.ToYugiohCardDefPower();
            }
            _driver.Quit();
        }
    }
}