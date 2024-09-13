using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models.Yugioh.Create
{
    public class CreateCardEffectTypeAssociationModel
    {
        public Guid CardId { get; set; }
        public Guid EffectTypeId { get; set; }
    }
}