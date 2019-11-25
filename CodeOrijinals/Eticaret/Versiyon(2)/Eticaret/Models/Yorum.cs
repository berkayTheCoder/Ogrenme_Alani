using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Models
{
    public class Yorum
    {
        public int Id { get; set; }
        [StringLength(600, MinimumLength = 3)]
        public string YorumMetni { get; set; }
        [Range(1, 10)]
        public int Puan { get; set; }
#if false // Sonraki versiyonda yorumu urun ve kullanıcı ile ilişkilendireceğim.
        public string UrunId { get; set; }
        public Urun Urun { get; set; }
        public string YorumcuId { get; set; } 
#endif
    }
}
