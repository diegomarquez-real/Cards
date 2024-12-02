using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client
{
    public static class EnumExtensions
    {
        public static string ToSchemaString(this Enums.SchemaType value)
        {
            switch(value)
            {
                case Enums.SchemaType.Dbo:
                    return String.Empty;
                case Enums.SchemaType.Yugioh:
                    return "Yugioh";
                default:
                    return String.Empty;
            }    
        }
    }
}