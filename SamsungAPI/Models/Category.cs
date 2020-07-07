using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungAPI.Models
{
    public class Category
    {
        public long CategoryId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
