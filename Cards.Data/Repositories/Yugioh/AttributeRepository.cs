using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class AttributeRepository : GenericRepository<Models.Yugioh.Attribute, Guid>, Abstractions.Repositories.Yugioh.IAttributeRepository
    {
        public AttributeRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }

        public async Task<Models.Yugioh.Attribute?> FindByNameAsync(string attributeName)
        {
            try
            {
                var sql = @"SELECT a.*
                            FROM [yugioh].[Attribute] AS a
                            WHERE a.Name = @Name";

                return await base._dbConnection.QuerySingleOrDefaultAsync<Models.Yugioh.Attribute>(sql, new { Name = attributeName });
            }
            catch (Exception) { throw; }
        }
    }
}