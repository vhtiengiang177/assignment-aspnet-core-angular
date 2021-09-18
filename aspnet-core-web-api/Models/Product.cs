using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required (ErrorMessage = "Please enter a product name")]
        public String Name { get; set; }
        public String? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        [Range(0,5)]
        [Required(AllowEmptyStrings = true)]
        public Int16 Rating { get; set; }
        public Double Price { get; set; }
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public IList<Product_Category> Product_Categories { get; set; }
    }
}
