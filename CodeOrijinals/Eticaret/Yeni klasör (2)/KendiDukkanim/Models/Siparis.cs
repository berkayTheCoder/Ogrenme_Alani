
#define Final // or Intro

#if Intro
#region snippet_Intro
namespace KendiDukkanim.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Siparis
    {
        public int SiparisID { get; set; }
        public int UrunID { get; set; }
        public int MusteriID { get; set; }
        public Grade? Grade { get; set; }

        public Urun Urun { get; set; }
        public Musteri Musteri { get; set; }
    }
}
#endregion

#elif Final
#region snippet_Final
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    //public enum Grade
    //{
    //    A, B, C, D, F
    //}

    public class Siparis
    {
        public int SiparisID { get; set; }

        public int UrunID { get; set; }
        public Urun Urun { get; set; }

        public int MusteriID { get; set; }
        public Musteri Musteri { get; set; }

        //[DisplayFormat(NullDisplayText = "No grade")]
        //public Grade? Grade { get; set; }
    }
}
#endregion
#endif