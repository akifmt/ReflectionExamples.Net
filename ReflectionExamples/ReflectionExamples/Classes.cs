using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples
{

    public static class Statics
    {

        public static AllTypesData Data = new AllTypesData();

        public static Kategori OrnekVeri = new Kategori
        {
            KategoriId = 1,
            KategoriAdi = "Arabalar",
            KategoriDetayi = "Super Arabalar",
            OnemliUrun = new Urun { Id = 1, UrunAdi = "Mercedes 1", UrunDetayi = "Bu model tekerleklidir." },
            Urunler = new List<Urun>
            {
                new Urun { Id = 11, UrunAdi = "Mercedes 11", UrunDetayi = "Bu modelde cam vardır." },
                new Urun { Id = 12, UrunAdi = "Mercedes 12", UrunDetayi = "Bu modele direksiyon ekledik." },
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
        public Urun PropUrun { get { return new Urun { Id = 1, UrunAdi = "ad", UrunDetayi = "detay" }; } }

        // System.Collections.Generic - include Built-in Type
        public List<int> PropListInt { get { return new List<int> { 1, 2, 3 }; } } //List<int>	
        public Dictionary<int, string> PropDictionaryIntStr { get { return new Dictionary<int, string> { { 1, "A" }, { 2, "B" } }; } } //Dictionary<int,string>	

        // System.Collections.Generic - include Class
        public List<Urun> PropListUrun { get { return new List<Urun> { new Urun { Id = 1, UrunAdi = "aaa", UrunDetayi = "ddd" } }; } } //List<class>	

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

    public class Kategori
    {
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public string KategoriDetayi { get; set; }
        public Urun OnemliUrun { get; set; }
        public List<Urun> Urunler { get; set; }
    }

    public class Urun
    {
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public string UrunDetayi { get; set; }
    }

}
