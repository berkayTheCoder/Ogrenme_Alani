using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendiDukkanim.Models
{
    public class Dukkan
    {
        [Key]
        public int Id { get; set; }
        public string Adi { get; set; }
        [StringLength(50)]
        [Display(Name = "Dükkanın Yeri")]
        //[Display(Name = "Office Location")]
        public string Yerleske { get; set; }
        //public string Location { get; set; }

        public int SaticiId { get; set; }
        public Satici Satici { get; set; }
    }
}