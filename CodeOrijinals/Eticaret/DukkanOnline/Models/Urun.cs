using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline.Models
{
    public class Urun
    {
        public int Id { get; set; }
        [Display(Name ="Ürün Başlığı")]
        public string Title { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Kısa Açıklama")]
        public string ShortDescription { get; set; }
        [Display(Name = "Uzun Açıklama")]
        public string LongDescription { get; set; }
        [Display(Name = "Fiyatı")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Stok { get; set; }
        public ICollection<Envanter> Envanterler { get; set; }
        public ICollection<Sepet> Sepets { get; set; }
        public ICollection<SatinAlma> SatinAlmas { get; set; }
        public ICollection<Yorum> Yorums { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public string ToptanciId { get; set; }
        public Toptanci Toptanci { get; set; }
        public string SaticiId { get; set; }
        public Satici Satici { get; set; }
        public string BakiciId { get; set; }
        public Bakici Bakici { get; set; }
        public string AliciId { get; set; }
        public Alici Alici { get; set; }
        public string YorumcuId { get; set; }
        public Yorumcu Yorumcu { get; set; }
    }
    public class Envanter
    {
        public int Id { get; set; }
        public ICollection<Sepet> Sepets { get; set; }
        public ICollection<SatinAlma> SatinAlmas { get; set; }
        public ICollection<Yorum> Yorums { get; set; }
        public string UrunId { get; set; }
        public Urun Urun { get; set; }
        public string SaticiId { get; set; }
        public Satici Satici { get; set; }
        public string BakiciId { get; set; }
        public Bakici Bakici { get; set; }
        public string AliciId { get; set; }
        public Alici Alici { get; set; }
        public string YorumcuId { get; set; }
        public Yorumcu Yorumcu { get; set; }
    }
    public class Sepet
    {
        public int Id { get; set; }
        public ICollection<SatinAlma> SatinAlmas { get; set; }
        public ICollection<Yorum> Yorums { get; set; }
        public string EnvanterId { get; set; }
        public Envanter Envanter { get; set; }
        public string UrunId { get; set; }
        public Urun Urun { get; set; }
        public string BakiciId { get; set; }
        public Bakici Bakici { get; set; }
        public string AliciId { get; set; }
        public Alici Alici { get; set; }
        public string YorumcuId { get; set; }
        public Yorumcu Yorumcu { get; set; }
    }

    public class SatinAlma
    {
        public int Id { get; set; }
        public ICollection<Yorum> Yorums { get; set; }
        public string SepetId { get; set; }
        public Sepet Sepet { get; set; }
        public string EnvanterId { get; set; }
        public Envanter Envanter { get; set; }
        public string UrunId { get; set; }
        public Urun Urun { get; set; }
        public string AliciId { get; set; }
        public Alici Alici { get; set; }
        public string YorumcuId { get; set; }
        public Yorumcu Yorumcu { get; set; }
    }

    public class Yorum
    {
        public int Id { get; set; }
        [StringLength(600,MinimumLength =3)]
        public string YorumMetni { get; set; }
        [Range(1,10)]
        public int Puan { get; set; }
        public string SatinAlmaId { get; set; }
        public SatinAlma SatinAlma { get; set; }
        public string SepetId { get; set; }
        public Sepet Sepet { get; set; }
        public string EnvanterId { get; set; }
        public Envanter Envanter { get; set; }
        public string UrunId { get; set; }
        public Urun Urun { get; set; }
        public string YorumcuId { get; set; }
        public Yorumcu Yorumcu { get; set; }
    }
}
