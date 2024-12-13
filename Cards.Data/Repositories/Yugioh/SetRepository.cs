using Dapper;
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

        public async Task<Models.Yugioh.Set?> FindByNameAsync(string setName)
        {
            try
            {
                var sql = @"SELECT s.*
                            FROM [yugioh].[Set] AS s
                            WHERE a.Name = @Name";

                return await base._dbConnection.QuerySingleOrDefaultAsync<Models.Yugioh.Set>(sql, new { Name = setName });
            }
            catch (Exception) { throw; }
        }
    }
}