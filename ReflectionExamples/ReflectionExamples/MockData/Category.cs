using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples.MockData
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDetails { get; set; }
        public Product SpecialProduct { get; set; }
        public List<Product> Products { get; set; }
    }
}
