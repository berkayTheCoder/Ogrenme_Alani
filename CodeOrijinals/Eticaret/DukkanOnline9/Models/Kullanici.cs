using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline9.Models
{
    public class Kullanici : IdentityUser<Guid>
    {
        public virtual ICollection<KullaniciRol> KullaniciRolleri { get; set; }
    }
}
