using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples
{

    public static class Statics
    {

        public static AllTypesData SampleAllTypesData = new AllTypesData();

        public static Category SampleClass = new Category
        {
            CategoryId = 1,
            CategoryName = "Cars",
            CategoryDetails = "Best Cars",
            SpecialProduct = new Product { Id = 1, ProductName = "Mercedes 1", ProductDetails= "Special model." },
            Products = new List<Product>
            {
                new Product{ Id = 11, ProductName = "Mercedes 11", ProductDetails = "Old model." },
                new Product { Id = 12, ProductName = "Mercedes 12", ProductDetails = "New model." },
            }
        };

    }



    public class AllTypesData
    {
        public System.Boolean PropBoolean { get { return true; } } //bool	
        public System.Byte PropByte { get { return byte.MaxValue; } } //byte	
        public System.SByte PropSByte { get { return sbyte.MaxValue; } } //sbyte	
        public System.Char PropChar { get { return 'A'; } } //char	
        public System.Decimal PropDecimal { get { return decimal.MaxValue; } } //decimal	
        public System.Double PropDouble { get { return double.MaxValue; } } //double	
        public System.Single PropSingle { get { return Single.MaxValue; } } //float	
        public System.Int32 PropInt32 { get { return Int32.MaxValue; } } //int		
        public System.UInt32 PropUInt32 { get { return uint.MaxValue; } } //uint	
        public System.Int64 PropInt64 { get { return Int64.MaxValue; } } //long	
        public System.UInt64 PropUInt64 { get { return UInt64.MaxValue; } } //ulong	
        public System.Object PropObject { get { return new object(); } } //object	
        public System.Int16 PropInt16 { get { return Int16.MaxValue; } } //short	
        public System.UInt16 PropUInt16 { get { return UInt16.MaxValue; } } //ushort	
        public System.String PropString { get { return "ABC"; } } //string	

        // Class - User defined
        public Product PropProduct { get { return new Product { Id = 1, ProductName = "name...", ProductDetails = "details..." }; } }

        // System.Collections.Generic - include Built-in Type
        public List<int> PropListInt { get { return new List<int> { 1, 2, 3 }; } } //List<int>	
        public Dictionary<int, string> PropDictionaryIntStr { get { return new Dictionary<int, string> { { 1, "A" }, { 2, "B" } }; } } //Dictionary<int,string>	

        // System.Collections.Generic - include Class
        public List<Product> PropListProduct { get { return new List<Product> { new Product { Id = 1, ProductName = "aaa", ProductDetails = "ddd" } }; } } //List<class>	

        // bunun icin henuz dunya hazir degil
        // System.Collections.Generic - include System.Collections.Generic - include Class
        //public List<List<Urun>> PropListListUrun {
        //    get {
        //        return new List<List<Urun>> {
        //            new List<Urun> {
        //                new Urun { Id = 1, UrunAdi = "aa", UrunDetayi = "ddd" },
        //                new Urun { Id = 2, UrunAdi = "bb", UrunDetayi = "ddd" },
        //            },
        //            new List<Urun> {
        //                new Urun { Id = 3, UrunAdi = "cc", UrunDetayi = "ddd" },
        //                new Urun { Id = 4, UrunAdi = "dd", UrunDetayi = "ddd" },
        //            }
        //        };
        //    }
        //} //List<List<class>>	

    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDetails { get; set; }
        public Product SpecialProduct { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDetails { get; set; }
    }

}
