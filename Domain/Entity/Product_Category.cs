using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Product_Category
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}
