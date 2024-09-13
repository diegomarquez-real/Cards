using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Yugioh
{
    public class CardEffectTypeAssociationModel
    {
        public Guid CardEffectTypeAssociationId { get; set; }
        public Guid CardId { get; set; }
        public Guid EffectTypeId { get; set; }
    }
}