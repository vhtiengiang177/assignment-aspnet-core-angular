using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public String Name { get; set; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        public Int16 Rating { get; set; }
        public Double Price { get; set; }
    }
}
