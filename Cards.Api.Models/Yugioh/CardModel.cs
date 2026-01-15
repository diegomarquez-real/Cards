using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Yugioh
{
    public class CardModel
    {
        public enum Expansions 
        { 
            Images
        }

        public Guid CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AttributeId { get; set; }
        public IEnumerable<byte[]> ImageBytes { get; set; }
    }
}