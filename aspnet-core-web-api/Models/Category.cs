using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Models
{
    public class Category
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public IList<Product_Category> Product_Categories { get; set; }

    }
}
