//using Eticaret.Areas.Identity.Data;

using System.Collections.Generic;

namespace Eticaret.Models
{
    public class Iyelik
    {
        public int Id { get; set; }
        public int Miktar { get; set; }
        public decimal Fiyati { get; set; }

#if true // Version 02 Urun İle Kullanıcı arasında iyelik şilişkisi
        public int UrunId { get; set; }
        public Urun Urun { get; set; }
        public string EticaretUserId { get; set; }
        public EticaretUser EticaretUser { get; set; }

#elif true // Sonraki Versiyona Planlanan
        public Urun Urun { get; set; }
        public EticaretUser EticaretUser { get; set; }
#endif
    }
}