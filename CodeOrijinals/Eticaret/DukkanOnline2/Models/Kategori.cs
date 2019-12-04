using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DukkanOnline2.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        [Display(Name ="Adı")]
        public string Name { get; set; }
        public int? AltKategoriId { get; set; }
        public AltKategori AltKategori { get; set; }
        public ICollection<Urun> Uruns { get; set; }
    }

    public class AltKategori
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
    }
}