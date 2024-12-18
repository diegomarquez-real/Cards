using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface ICardEffectTypeAssociationClient
    {
        Task<Guid> CreateCardEffectTypeAssociationAsync(Models.Yugioh.Create.CreateCardEffectTypeAssociationModel createCardEffectTypeAssociationModel);
    }
}