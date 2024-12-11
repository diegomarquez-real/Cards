using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Repositories.Yugioh
{
    public class CardRepository : GenericRepository<Models.Yugioh.Card, Guid>, Abstractions.Repositories.Yugioh.ICardRepository
    {
        public CardRepository(Abstractions.IDataContext dataContext,
            Abstractions.IUserContext userContext)
            : base(dataContext, userContext)
        {
        }

        public async Task<Models.Yugioh.Card?> FindByNameAsync(string cardName)
        {
            try
            {
                var sql = @"SELECT c.*
                            FROM [yugioh].[Card] AS c
                            WHERE c.Name = @Name";

                return await base._dbConnection.QuerySingleOrDefaultAsync<Models.Yugioh.Card>(sql, new { Name = cardName });
            }
            catch (Exception) { throw; }
        }
    }
}