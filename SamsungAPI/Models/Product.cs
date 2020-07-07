using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungAPI.Models
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }

        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public List<Rating> Ratings { get; set; }


    }
}
