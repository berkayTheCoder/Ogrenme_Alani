//using Bm.Areas.Identity.Data;

using System.Collections.Generic;

namespace BerkayMarket.Models
{
    public class Iyelik
    {
        //public int Miktar { get; set; }
        //public decimal Fiyati { get; set; }

#if true // Version 02 Urun İle Kullanıcı arasında iyelik şilişkisi
        public int UrunId { get; set; }
        public Urun Urun { get; set; }
        public string BmUserId { get; set; }
        public BmUser BmUser { get; set; }
#endif
    }
}