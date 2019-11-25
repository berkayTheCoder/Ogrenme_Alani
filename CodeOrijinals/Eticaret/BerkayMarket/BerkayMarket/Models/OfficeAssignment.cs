using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerkayMarket.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int SaticiID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Satici Satici { get; set; }
    }
}