using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DukkanOnline3.Models
{
    public class Kategori
    {
        public virtual int Id { get; set; }
        [Display(Name ="Adı")]
        public virtual string Name { get; set; }
        //public virtual ICollection<AltKategori> AltKategorileri { get; set; }
        public virtual ICollection<Urun> Uruns { get; set; }
    }

    //public class AltKategori
    //{
    //    public virtual int Id { get; set; }
    //    [Display(Name = "Adı")]
    //    public virtual string Name { get; set; }
    //    public virtual ICollection<Urun> Uruns { get; set; }
    //}
}