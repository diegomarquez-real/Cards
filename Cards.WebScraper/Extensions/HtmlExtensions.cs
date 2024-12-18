using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cards.WebScraper
{
    public static class HtmlExtensions
    {
        public static string RemoveWhitespaceCharacters(this string input)
        {
            var htmlDecoded = System.Net.WebUtility.HtmlDecode(input);

            return Regex.Replace(htmlDecoded, @"[\r\n\t]", "").Trim();
        }
    }
}