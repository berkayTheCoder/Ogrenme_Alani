using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class Siparis
    {
        public int Id { get; set; }
        public string SiparisVerenUserId { get; set; }
        public BmUser SiparisVerenUser { get; set; }

        public int UrunId { get; set; }
        public Urun Urun { get; set; }

        public string SiparisAlanUserId { get; set; }
        public BmUser SiparisAlanUser { get; set; }

        public SiparisDurumu SiparisDurumu { get; set; }


        public int Miktari { get; set; }
        public string SiparisMetni { get; set; }
    }
    public enum SiparisDurumu
    {
        SiparisVer,
        Onayla,
        Reddet

    }
}
