using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Models
{
    public class ProductDetail
    {
        public int ProductID { get; set; }
        public String Details { get; set; }
        public Product Product { get; set; }
    }
}
