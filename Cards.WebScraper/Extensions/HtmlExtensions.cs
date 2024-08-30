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
            return Regex.Replace(input, @"[\r\n\t]", "").Trim();
        }
    }
}