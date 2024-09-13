using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Yugioh
{
    public interface ICardSpeciesAssociationRepository : IGenericRepository<Models.Yugioh.CardSpeciesAssociation, Guid>
    {
    }
}