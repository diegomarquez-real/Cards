using Dapper;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

        public async Task<IEnumerable<Models.Yugioh.Card>> FindAllOrQueryAsync(Models.Yugioh.CardQuery cardQuery)
        {
            try
            {
                SqlBuilder sqlBuilder = new ();
                DynamicParameters parameters = new();

                SqlBuilder.Template sqlTemplate = sqlBuilder
                    .AddTemplate(@"SELECT c.*
                                   FROM [yugioh].[Card] AS c
                                   /**innerjoin**/
                                   /**where**/ 
                                   /**orderby**/");

                // JOIN
                if (cardQuery.SortBy.HasValue && 
                    cardQuery.SortBy.Value == Models.Query.SortByEnum.Date)
                {
                    sqlBuilder.InnerJoin(@"
                    (
	                    SELECT csa.CardId, 
		                    s.ReleaseDate, 
	                        ROW_NUMBER() OVER (
			                    PARTITION BY csa.CardId 
			                    ORDER BY s.ReleaseDate DESC
		                    ) AS IdCount
	                    FROM [yugioh].[CardSetAssociation] AS csa
	                    INNER JOIN [yugioh].[Set] AS s
		                    ON s.SetId = csa.SetId
                    ) AS gbic ON gbic.CardId = c.CardId");
                    sqlBuilder.Where("gbic.IdCount = 1");
                }

                // WHERE
                if (!String.IsNullOrWhiteSpace(cardQuery.NameSearchText))
                {
                    string nameSearchText = $"%{cardQuery.NameSearchText.Trim()}%";
                    sqlBuilder.Where("c.Name LIKE @NameSearchText");
                    parameters.Add("NameSearchText", nameSearchText);
                }

                // ORDER BY
                this.ApplySortingAndPaging(sqlBuilder, cardQuery, parameters);

                return await base._dbConnection.QueryAsync<Models.Yugioh.Card>(sqlTemplate.RawSql, parameters);
            }
            catch (Exception) { throw; }
        }

        private void ApplySortingAndPaging(
            SqlBuilder sqlBuilder, 
            Models.Yugioh.CardQuery cardQuery,
            DynamicParameters parameters)
        {
            string orderBy = "c.Name";

            if (cardQuery.SortBy.HasValue)
            {
                switch (cardQuery.SortBy)
                {
                    case Models.Query.SortByEnum.Date:
                        orderBy = "gbic.ReleaseDate";
                        break;
                    case Models.Query.SortByEnum.Name:
                        orderBy = "c.Name";
                        break;
                }
            }

            switch (cardQuery.SortOrder)
            {
                case Models.Query.SortOrderEnum.Ascending:
                    orderBy += " ASC";
                    break;
                case Models.Query.SortOrderEnum.Descending:
                    orderBy += " DESC";
                    break;
            }

            (int offset, int fetch) offsetFetchTuple = sqlBuilder.ApplyPaging(orderBy, cardQuery.PageNumber, cardQuery.PageSize);

            parameters.AddDynamicParams(new 
            { 
                OffsetValue = offsetFetchTuple.offset, 
                FetchValue = offsetFetchTuple.fetch 
            });
        }
    }
}