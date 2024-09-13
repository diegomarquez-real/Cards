using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class CardEffectTypeAssociationRepository : GenericRepository<Models.Yugioh.CardEffectTypeAssociation, Guid>, Abstractions.Repositories.Yugioh.ICardEffectTypeAssociationRepository
    {
        public CardEffectTypeAssociationRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}