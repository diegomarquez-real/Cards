using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class EffectTypeRepository : GenericRepository<Models.Yugioh.EffectType, Guid>, Abstractions.Repositories.Yugioh.IEffectTypeRepository
    {
        public EffectTypeRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}