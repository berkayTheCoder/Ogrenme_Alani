using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline2.Models
{
    public class ApplicationUser : IdentityUser
    {
    }
    public class Yorumcu : ApplicationUser
    {
        public ICollection<Yorum> Yorums { get; set; }
    }
    public class Alici : Yorumcu
    {
        public ICollection<SatinAlma> SatinAlmas { get; set; }
    }

    public class Bakici : Alici
    {
        public ICollection<Sepet> Sepets { get; set; }
    }
    public class Satici : Bakici
    {
        public ICollection<Envanter> Envanters { get; set; }
    }
    public class Toptanci : Satici {
        public int Sicili { get; set; }
        public ICollection<Urun> Uruns { get; set; }
    }
}
