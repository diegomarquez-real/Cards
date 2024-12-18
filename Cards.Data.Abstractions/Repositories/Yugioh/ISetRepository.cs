using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Yugioh
{
    public interface ISetRepository : IGenericRepository<Models.Yugioh.Set, Guid>
    {
        Task<Models.Yugioh.Set?> FindByNameAsync(string setName);
    }
}