using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Values
{
    public class FilterParamsProduct
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string Sort { get; set; }
        public int[] IdCategories { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int[] Rating { get; set; }
        public string Content { get; set; }

    }
}
