using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class CardRepository : GenericRepository<Models.Card, Guid>, Abstractions.Repositories.Yugioh.ICardRepository
    {
        public CardRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}