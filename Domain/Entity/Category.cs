using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Category
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public virtual IList<Product_Category> Product_Categories { get; set; }

    }
}
