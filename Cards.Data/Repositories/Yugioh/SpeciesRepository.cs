using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class SpeciesRepository : GenericRepository<Models.Yugioh.Species, Guid>, Abstractions.Repositories.Yugioh.ISpeciesRepository
    {
        public SpeciesRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }

        public async Task<Models.Yugioh.Species?> FindByNameAsync(string speciesName)
        {
            try
            {
                var sql = @"SELECT s.*
                            FROM [yugioh].[Species] AS s
                            WHERE s.Name = @Name";

                return await base._dbConnection.QuerySingleOrDefaultAsync<Models.Yugioh.Species>(sql, new { Name = speciesName });
            }
            catch (Exception) { throw; }
        }
    }
}