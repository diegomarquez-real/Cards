using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Abstractions.Repositories.Yugioh
{
    public interface ISpeciesRepository : IGenericRepository<Models.Yugioh.Species, Guid>
    {
        Task<Models.Yugioh.Species?> FindByNameAsync(string speciesName);
    }
}