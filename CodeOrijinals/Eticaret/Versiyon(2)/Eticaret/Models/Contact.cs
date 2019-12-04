//using Eticaret.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eticaret.Models
{
    #region snippet1
    public class Contact : IdentityUser<string>
    {
        public int ContactId { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        [Display(Name ="Adı")]
        public string Name { get; set; }
        [Display(Name = "Adresi")]
        public string Address { get; set; }
        [Display(Name = "İlçe")]
        public string Town { get; set; }
        [Display(Name = "İl")]
        public string City { get; set; }
        [Display(Name = "Bölge")]
        public string State { get; set; }
        [Display(Name = "PostaKod")]
        public string Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email2 { get; set; }
        public string Resim { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }

        [Display(Name = "Statü")]
        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
    #endregion
}