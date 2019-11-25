using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline2.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        [Display(Name ="Adı")]
        public string Name { get; set; }
        [Display(Name = "Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [Display(Name = "Adresi")]
        public string Address { get; set; }
        [Display(Name = "İlçe")]
        public string Town { get; set; }
        [Display(Name = "İl")]
        public string City { get; set; }
        [Display(Name = "Bolge")]
        public string State { get; set; }
        [Display(Name = "Posta Kodu")]
        public string Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Bağlantı Durumu")]
        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Başvuru,
        Onaylı,
        Red
    }
}
