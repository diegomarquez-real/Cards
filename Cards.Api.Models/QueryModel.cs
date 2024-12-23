using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Api.Models
{
    public class QueryModel
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public SortByEnum? SortBy { get; set; }
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.Ascending;

        #region Sorting 

        public enum SortByEnum
        {
            Date = 1,
            Name = 2
        }

        public enum SortOrderEnum
        {
            Ascending,
            Descending
        }

        #endregion
    }
}