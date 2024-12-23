using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Models
{
    public class Query
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public SortByEnum? SortBy { get; set; }
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.Ascending;

        #region Sorting 

        public enum SortByEnum
        {
            Name = 1,
            Date = 2
        }

        public enum SortOrderEnum
        {
            Ascending,
            Descending
        }

        #endregion
    }
}