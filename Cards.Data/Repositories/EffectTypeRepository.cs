using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data
{
    public class EffectTypeRepository : GenericRepository<Models.EffectType, Guid>, Abstractions.IEffectTypeRepository
    {
        public EffectTypeRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}