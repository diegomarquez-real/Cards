using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class SetRepository : GenericRepository<Models.Yugioh.Set, Guid>, Abstractions.Repositories.Yugioh.ISetRepository
    {
        public SetRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }
    }
}