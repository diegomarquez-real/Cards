using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class CardSpeciesAssociationRepository : GenericRepository<Models.Yugioh.CardSpeciesAssociation, Guid>, Abstractions.Repositories.Yugioh.ICardSpeciesAssociationRepository
    {
        public CardSpeciesAssociationRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }
    }
} 