using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Yugioh
{
    public class CardSpeciesAssociationModel
    {
        public Guid CardSpeciesAssociationId { get; set; }
        public Guid CardId { get; set; }
        public Guid SpeciesId { get; set; }
    }
}