using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class CardSetAssociationRepository : GenericRepository<Models.Yugioh.CardSetAssociation, Guid>, Abstractions.Repositories.Yugioh.ICardSetAssociationRepository
    {
        public CardSetAssociationRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}