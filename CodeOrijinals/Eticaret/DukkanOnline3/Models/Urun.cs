using DukkanOnline3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline3.Models
{
    public class Urun
    {
        public Urun()
        {
        }

        public Urun(string urunName)
        {
        }

        public int Id { get; set; }

        [Display(Name ="Ürün Başlığı")]
        public string Title { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Kısa Açıklama")]
        public string ShortDescription { get; set; }
        [Display(Name = "Uzun Açıklama")]
        public string LongDescription { get; set; }

        [Display(Name = "Maliyeti")]
        [DataType(DataType.Currency)]
        public decimal Maliyeti { get; set; }
        public int Stok { get; set; }
        public string Resim { get; set; }
        [NotMapped]
        public virtual IFormFile FormFile { get; set; }


        public virtual int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        //public virtual int AltKategoriId { get; set; }
        //public AltKategori AltKategori { get; set; }
        public ICollection<Yorum> Yorumlari { get; set; }
        public ICollection<Iyelik> Iyelikleri { get; set; }
    }

    public class Yorum
    {
        public int Id { get; set; }
        [StringLength(600,MinimumLength =3)]
        public string YorumMetni { get; set; }
        [Range(1,10)]
        public int Puan { get; set; }

        public int UrunId { get; set; }
        public Urun MyUrun { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

        public string YorumcuId { get; set; }
        public MyUser Yorumcu { get; set; }

    }
}
