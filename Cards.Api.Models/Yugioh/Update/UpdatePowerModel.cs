using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Yugioh.Update
{
    public class UpdatePowerModel
    {
        public int? Level { get; set; }
        public int? Rank { get; set; }
        public int? Link { get; set; }
        public int? PScale { get; set; }
        public int? Attack { get; set; }
        public int? Defense { get; set; }
    }
}