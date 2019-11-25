#define AfterInheritance // or Intro or StringLength or DataType or BeforeInheritance

#if Intro
#region snippet_Intro
using System;
using System.Collections.Generic;

namespace KendiDukkanim.Models
{
    public class Musteri
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime SiparisDate { get; set; }

        public ICollection<Siparis> Sipariss { get; set; }
    }
}
#endregion

#elif DataType
#region snippet_DataType
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KendiDukkanim.Models
{
    public class Musteri
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SiparisDate { get; set; }

        public ICollection<Siparis> Sipariss { get; set; }
    }
}
#endregion

#elif StringLength
#region snippet_StringLength
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KendiDukkanim.Models
{
    public class Musteri
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SiparisDate { get; set; }

        public ICollection<Siparis> Sipariss { get; set; }
    }
}
#endregion

#elif Column
#region snippet_Column
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Musteri
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        [Column("FirstName")]
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SiparisDate { get; set; }

        public ICollection<Siparis> Sipariss { get; set; }
    }
}
#endregion


#elif BeforeInheritance
#region snippet_BeforeInheritance
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Musteri
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Siparis Date")]
        public DateTime SiparisDate { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public ICollection<Siparis> Sipariss { get; set; }
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
    public class Musteri : Kisi
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Siparis Date")]
        public DateTime SiparisDate { get; set; }


        public ICollection<Siparis> Sipariss { get; set; }
    }
}
#endregion
#endif