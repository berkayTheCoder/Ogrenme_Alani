using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline9.Models.DukkanViewModels
{
    public class KullaniciRolCreateData
    {
        public ICollection<Kullanici> Kullanicis { get; set; }
        public ICollection<Rol> Rols { get; set; }

        public ICollection<KullaniciRol> KullaniciRolIliskileri { get; set; }
    }
}
