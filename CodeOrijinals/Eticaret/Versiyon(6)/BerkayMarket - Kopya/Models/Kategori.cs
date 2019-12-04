using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }

        public string Adi { get; set; }

        //---ilişkileri----
        public List<Urun> Urunleri { get; set; }
    }
}
