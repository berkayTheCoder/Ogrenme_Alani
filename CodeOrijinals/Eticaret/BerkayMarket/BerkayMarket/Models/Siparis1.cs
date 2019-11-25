using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class Siparis1
    {
        public enum Renk
        {
            Mor, Mavi, Pembe, Pudra, MintYeşili
        }

        public class Siparis
        {
            public int SiparisID { get; set; }
            public int UrunID { get; set; }
            public int MusteriID { get; set; }
            [DisplayFormat(NullDisplayText = "No Renk")]
            public Renk? Renk { get; set; }
            public int Adet { get; set; }

            public Urun Urun { get; set; }
            public Musteri Musteri { get; set; }
        }
    }
}
