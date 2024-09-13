using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class PowerRepository : GenericRepository<Models.Power, Guid>, Abstractions.Repositories.Yugioh.IPowerRepository
    {
        public PowerRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}