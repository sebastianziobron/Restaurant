using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Models
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
