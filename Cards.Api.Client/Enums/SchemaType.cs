using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Enums
{
    public enum SchemaType
    {
        [Description("Dbo")]
        Dbo = 1,
        [Description("Yugioh")]
        Yugioh = 2
    }
}