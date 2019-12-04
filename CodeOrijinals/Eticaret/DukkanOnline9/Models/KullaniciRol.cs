using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline9.Models
{
    public class KullaniciRol : IdentityUserRole<Guid>
    {
        public virtual Kullanici RolKullanicisi { get; set; }
        public virtual Rol KullaniciRolu { get; set; }
    }
}
