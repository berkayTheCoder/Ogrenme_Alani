using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerkayMarket.Models
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
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Urun Urun { get; set; }
        public Musteri Musteri { get; set; }
    }
}