using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data
{
    public static class SqlBuilderExtensions
    {
        public static (int offset, int fetch) ApplyPaging(
            this SqlBuilder sqlBuilder, 
            string orderBySql, 
            int? pageNumber, 
            int? pageSize)
        {
            int fetchValue = pageSize.HasValue && pageSize.Value > 0 && pageSize.Value <= 100 ? pageSize.Value : 100;
            int offsetValue = pageNumber.HasValue && pageNumber > 0 ? (pageNumber.Value - 1) * fetchValue : 0;

            sqlBuilder.OrderBy($@"{orderBySql}
                                  OFFSET @OffsetValue ROWS
                                  FETCH NEXT @FetchValue ROWS ONLY");

            return (offsetValue, fetchValue);
        }
    }
}