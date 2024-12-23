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
        public static object ApplyPaging(this StringBuilder sql, int? pageNumber, int? pageSize)
        {
            int fetchValue = pageSize.HasValue && pageSize.Value > 0 && pageSize.Value <= 100 ? pageSize.Value : 100;
            int offsetValue = pageNumber.HasValue && pageNumber > 0 ? (pageNumber.Value - 1) * fetchValue : 0;

            sql.AppendLine($@"OFFSET @OffsetValue ROWS
                              FETCH NEXT @FetchValue ROWS ONLY");

            return new { OffsetValue = offsetValue, FetchValue = fetchValue };
        }
    }
}