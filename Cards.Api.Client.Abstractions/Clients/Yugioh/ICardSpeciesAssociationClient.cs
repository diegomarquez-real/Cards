using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface ICardSpeciesAssociationClient
    {
        Task<Guid> CreateCardSpeciesAssociationAsync(Models.Yugioh.Create.CreateCardSpeciesAssociationModel createCardSpeciesAssociationModel);
    }
}