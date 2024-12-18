using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cards.WebScraper
{
    public static class YugiohExtensions
    {
        public static int? ToYugiohCardNumericValue(this string input)
        {
            if(int.TryParse(Regex.Match(input.RemoveWhitespaceCharacters(), @"\d+").Value, out int level))
                return level;

            return null;
        }

        public static List<string> ToYugiohCardSpecies(this string input)
        {
            return Regex.Replace(input.RemoveWhitespaceCharacters(), @"[\[\]]", "").Trim().Split("/").ToList();
        }
    }
}