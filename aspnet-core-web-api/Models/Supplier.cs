using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
