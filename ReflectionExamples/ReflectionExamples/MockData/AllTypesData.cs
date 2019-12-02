using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples.MockData
{
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

    }
}
