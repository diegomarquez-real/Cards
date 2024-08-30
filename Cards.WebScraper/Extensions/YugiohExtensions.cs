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
        public static int? ToYugiohCardLevel(this string input)
        {
            if(int.TryParse(input.RemoveWhitespaceCharacters().Replace("Level ", "", StringComparison.OrdinalIgnoreCase), out int level))
                return level;

            return null;
        }

        public static string[] ToYugiohCardSpecies(this string input)
        {
            return Regex.Replace(input.RemoveWhitespaceCharacters(), @"[\[\]]", "").Trim().Split("/");
        }

        public static int? ToYugiohCardAtkPower(this string input)
        {
            if (int.TryParse(input.RemoveWhitespaceCharacters().Replace("ATK ", "", StringComparison.OrdinalIgnoreCase), out int level))
                return level;

            return null;
        }

        public static int? ToYugiohCardDefPower(this string input)
        {
            if (int.TryParse(input.RemoveWhitespaceCharacters().Replace("DEF ", "", StringComparison.OrdinalIgnoreCase), out int level))
                return level;

            return null;
        }
    }
}