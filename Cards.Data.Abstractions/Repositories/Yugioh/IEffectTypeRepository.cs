using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Yugioh
{
    public interface IEffectTypeRepository : IGenericRepository<Models.Yugioh.EffectType, Guid>
    {
    }
}