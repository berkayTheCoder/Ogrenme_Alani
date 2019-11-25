#define AfterInheritance // or BeforeInheritance

#if BeforeInheritance
#region snippet_BeforeInheritance
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Satici
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public ICollection<Satis> Satiss { get; set; }
        public Dukkan Dukkan { get; set; }
    }
}
#endregion
#elif AfterInheritance
#region snippet_AfterInheritance
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Satici : Kisi
    {
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Hire Date")]
        //public DateTime HireDate { get; set; }

        public ICollection<Satis> SatisLari { get; set; }
        //public int DukkanId { get; set; }
        public Dukkan Dukkan { get; set; }
    }
}
#endregion
#endif