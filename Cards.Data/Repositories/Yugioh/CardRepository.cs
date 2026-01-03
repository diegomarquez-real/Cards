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

        public async Task<IEnumerable<Models.Yugioh.Card>> FindAllOrQueryAsync(Models.Yugioh.CardQuery cardQuery)
        {
            try
            {
                StringBuilder sql = new();
                SqlBuilder sqlBuilder = new ();
                DynamicParameters parameters = new();

                // WITH
                if(cardQuery.SortBy.HasValue && cardQuery.SortBy.Value == Models.Query.SortByEnum.Date)
                {
                    sql.AppendLine(@"WITH GroupByIdCount AS (
                                        SELECT csa.CardId, s.Name, s.ReleaseDate, 
	                                       ROW_NUMBER() OVER (
				                                PARTITION BY csa.CardId ORDER BY s.ReleaseDate DESC
		                                   ) AS IdCount
	                                    FROM yugioh.CardSetAssociation AS csa
	                                    INNER JOIN yugioh.[Set] AS s
		                                    ON s.SetId = csa.SetId
                                    )");
                }
                
                sql.AppendLine(@"SELECT c.*
                                 FROM [yugioh].[Card] AS c");

                SqlBuilder.Template sqlTemplate = sqlBuilder
                    .AddTemplate(@"/**innerjoin**/
                                   /**where**/ 
                                   /**orderby**/");

                // JOINS
                if (cardQuery.SortBy.HasValue && cardQuery.SortBy.Value == Models.Query.SortByEnum.Date)
                {
                    sqlBuilder.InnerJoin(@"GroupByIdCount AS gbic
	                                       ON gbic.CardId = c.CardId");
                    sqlBuilder.Where("IdCount = 1");
                }

                // WHERE
                if (!String.IsNullOrWhiteSpace(cardQuery.NameSearchText))
                {
                    string nameSearchText = $"%{cardQuery.NameSearchText}%";
                    sqlBuilder.Where("c.Name LIKE @NameSearchText");
                    parameters.Add("NameSearchText", nameSearchText);
                }

                sql.AppendLine(sqlTemplate.RawSql);

                // ORDER BY
                this.ApplySortingAndPaging(sql, cardQuery);

                // OFFSET FETCH    
                parameters.AddDynamicParams(sql.ApplyPaging(cardQuery.PageNumber, cardQuery.PageSize));

                return await base._dbConnection.QueryAsync<Models.Yugioh.Card>(sql.ToString(), parameters);
            }
            catch (Exception) { throw; }
        }

        public StringBuilder ApplySortingAndPaging(StringBuilder sql, Models.Yugioh.CardQuery cardQuery)
        {
            string orderBy = "ORDER BY c.Name";

            if (cardQuery.SortBy.HasValue)
            {
                switch (cardQuery.SortBy)
                {
                    case Models.Query.SortByEnum.Date:
                        orderBy = "ORDER BY gbic.ReleaseDate";
                        break;
                    case Models.Query.SortByEnum.Name:
                        orderBy = "ORDER BY c.Name";
                        break;
                    default:
                        orderBy = "ORDER BY c.Name";
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

            sql.AppendLine(orderBy);

            return sql;
        }
    }
}