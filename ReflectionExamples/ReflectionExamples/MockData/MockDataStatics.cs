using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples.MockData
{
    public static class MockDataStatics
    {

        public static AllTypesData SampleAllTypesData = new AllTypesData();

        public static Category SampleClass = new Category
        {
            CategoryId = 1,
            CategoryName = "Cars",
            CategoryDetails = "Best Cars",
            SpecialProduct = new Product { Id = 1, ProductName = "Mercedes 1", ProductDetails = "Special model." },
            Products = new List<Product>
            {
                new Product{ Id = 11, ProductName = "Mercedes 11", ProductDetails = "Old model." },
                new Product { Id = 12, ProductName = "Mercedes 12", ProductDetails = "New model." },
            }
        };

    }
}
