using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class Urun
    {
        [Key]
        public int Id { get; set; }

        public string Adi { get; set; }

        public int Stok { get; set; }

        //---ilişkileri----
        [Display(Name ="Kategorisi")]
        public int KategoriId { get; set; }    //scaler
        public Kategori Kategori { get; set; }  //referans

        public string Resim { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }

        [Display(Name ="Açıklama")]
        public string Aciklama { get; set; }

#if true // Version4 USR ve diğer userlar
        public int USRId { get; set; }
        public USR USR { get; set; }
        public string BmUserId { get; set; }
        public BmUser BmUser { get; set; }
        //public string BmSaticiId { get; set; }
        //public BmUser BmSatici { get; set; }
        //public string BmToptanciId { get; set; }
        //public BmUser BmToptanci { get; set; }
        //public string BmUreticiId { get; set; }
        //public BmUser BmUretici { get; set; }
#endif


    } 

}
