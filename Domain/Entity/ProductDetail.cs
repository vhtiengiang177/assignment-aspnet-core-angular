using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ProductDetail
    {
        public int ProductID { get; set; }
        public string Details { get; set; }
        public virtual Product Product { get; set; }
    }
}
