using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Product
    {
        public int ID { get; set; }
        [Required (ErrorMessage = "Please enter a product name")]
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        [Range(0,5)]
        [Required]
        public Int16 Rating { get; set; }
        [Required]
        public Double Price { get; set; }
        public int? SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
        public virtual IList<Product_Category> Product_Categories { get; set; }
    }
}
