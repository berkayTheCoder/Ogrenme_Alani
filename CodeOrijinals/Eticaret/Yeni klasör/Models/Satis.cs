using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Satis //urunle satıcı arasındaki ilişki
    {
        [Key]
        public int SatisId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Satış Zamanı")]
        public DateTime SatisZamani { get; set; }

        public int SaticiID { get; set; }
        public Satici Satici { get; set; }
        public int UrunID { get; set; }
        public Urun Urun { get; set; }
    }
}