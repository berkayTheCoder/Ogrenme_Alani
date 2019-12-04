using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerkayMarket.Models
{
    public class Kategori
    {
        public int KategoriID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? SaticiID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Satici Administrator { get; set; }
        public ICollection<Urun> Uruns { get; set; }
    }
}