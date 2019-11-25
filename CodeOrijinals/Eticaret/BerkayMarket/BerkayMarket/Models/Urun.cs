using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerkayMarket.Models
{
    public class Urun
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int UrunID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int KategoriID { get; set; }

        public Kategori Kategori { get; set; }
        public ICollection<Siparis> Sipariss { get; set; }
        public ICollection<Envanter> Envanters { get; set; }
    }
}