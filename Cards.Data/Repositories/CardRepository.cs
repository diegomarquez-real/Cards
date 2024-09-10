using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data
{
    public class CardRepository : GenericRepository<Models.Card, Guid>, Abstractions.ICardRepository
    {
        public CardRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        } 
    }
}