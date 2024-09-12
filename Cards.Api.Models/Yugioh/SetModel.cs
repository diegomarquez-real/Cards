using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Yugioh
{
    public class SetModel
    {
        public Guid SetId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}