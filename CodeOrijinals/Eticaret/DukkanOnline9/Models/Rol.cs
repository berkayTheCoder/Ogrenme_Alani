using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline9.Models
{
    public class Rol : IdentityRole<Guid>
    {
        [Display(Name ="Tanılama")]
        public string Tanilama { get; set; }
        public virtual ICollection<KullaniciRol> RolKullanicilari { get; set; }
    }
}
