using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class EffectTypeRepository : GenericRepository<Models.Yugioh.EffectType, Guid>, Abstractions.Repositories.Yugioh.IEffectTypeRepository
    {
        public EffectTypeRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }

        public async Task<Models.Yugioh.EffectType?> FindByNameAsync(string effectTypeName)
        {
            try
            {
                var sql = @"SELECT et.*
                            FROM [yugioh].[EffectType] AS et
                            WHERE et.Name = @Name";

                return await base._dbConnection.QuerySingleOrDefaultAsync<Models.Yugioh.EffectType>(sql, new { Name = effectTypeName });
            }
            catch (Exception) { throw; }
        }
    }
}