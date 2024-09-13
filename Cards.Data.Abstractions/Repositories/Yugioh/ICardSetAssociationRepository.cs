using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Yugioh
{
    public interface ICardSetAssociationRepository : IGenericRepository<Models.CardSetAssociation, Guid>
    {
    }
}