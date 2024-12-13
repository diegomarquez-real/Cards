using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Client.Abstractions.Yugioh
{
    public interface ICardSetAssociationClient
    {
        Task<Guid> CreateCardSetAssociationAsync(Models.Yugioh.Create.CreateCardSetAssociationModel createCardSetAssociationModel);
    }
}