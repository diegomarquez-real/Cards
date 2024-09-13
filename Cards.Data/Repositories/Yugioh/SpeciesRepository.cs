using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class SpeciesRepository : GenericRepository<Models.Yugioh.Species, Guid>, Abstractions.Repositories.Yugioh.ISpeciesRepository
    {
        public SpeciesRepository(Abstractions.IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}