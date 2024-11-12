using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.WebScraper
{
    public static class ScrapingExtensions
    {
        public static async Task NavigateNewTabAsync(this OpenQA.Selenium.IWebDriver driver, string url)
        {
            ((OpenQA.Selenium.IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            var newTabHandle = driver.WindowHandles.Last();
            await driver.SwitchTo().Window(newTabHandle).Navigate().GoToUrlAsync(url);
        }
    }
}