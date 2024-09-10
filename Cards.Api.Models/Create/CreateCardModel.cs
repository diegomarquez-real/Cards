using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Create
{
    public class CreateCardModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AttributeId { get; set; }
    }
}