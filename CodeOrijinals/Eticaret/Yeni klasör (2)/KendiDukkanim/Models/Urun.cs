#define Final // or Intro

#if Intro
#region snippet_Intro
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Urun
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UrunID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Siparis> Sipariss { get; set; }
    }
}
#endregion

#elif Final
#region snippet_Final
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Urun
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int UrunID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        #region Eklemeler1
        public string Adi { get; set; }
        public int Stok { get; set; }
        [Display(Name ="Kısa Açıklama")]
        public string KisaAciklama { get; set; }
        [Display(Name = "Uzun Açıklama")]
        public string UzunAciklama { get; set; }
        #endregion
        #region Satıcı İlişkisi
        //public int SaticiId { get; set; }
        public ICollection<Satici> UrunSaticilari { get; set; }
        #endregion
        [Range(0, 5)]
        public int Puani { get; set; }

        public int KategoriID { get; set; }
        public Kategori Kategorisi { get; set; }

        public ICollection<Siparis> Siparisleri { get; set; }
        public ICollection<Satis> Satislari { get; set; }
    }
}
#endregion
#endif