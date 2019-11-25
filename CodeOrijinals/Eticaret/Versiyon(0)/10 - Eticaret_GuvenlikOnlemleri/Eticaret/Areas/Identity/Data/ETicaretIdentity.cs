using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Areas.Identity.Data
{
    public class EticaretUser : IdentityUser
    {
        public EticaretUser() : base()
        {

        }
        public EticaretUser(string userName) : base(userName)
        {
        }

        public virtual ICollection<EticaretUserClaim> Claims { get; set; }
        public virtual ICollection<EticaretUserLogin> Logins { get; set; }
        public virtual ICollection<EticaretUserToken> Tokens { get; set; }
        public virtual ICollection<EticaretUserRole> UserRoles { get; set; }

#if false // yakında iyelik ve yorum özellikleri eklenecek
        public ICollection<Iyelik> Iyelikleri { get; set; }

        public virtual ICollection<Yorum> Yorumlar { get; set; } 
#endif
    }
#if false //hiyeraarşi sonraki versiyonda
    public class Uretici : EticaretUser
    {
        public ICollection<Mamul> Mamulleri { get; set; }
    }
    public class Toptanci : Uretici
    {
        public ICollection<Toptan> Toptanlari { get; set; }
    }
    public class Perakendeci : Toptanci
    {
        public ICollection<Perakende> Perakendeleri { get; set; }
    }
    public class Musteri : Perakendeci
    {
        public ICollection<Siparis> Siparisleri { get; set; }
    }
    public class Alici : Musteri
    {
        public ICollection<Alim> Alimlari { get; set; }
    }
    public class Satici : Perakendeci
    {
        public ICollection<Satim> Satimlari { get; set; }
    }

#endif

    public class EticaretRole : IdentityRole
    {
            public EticaretRole() : base()
            {

            }
            public EticaretRole(string roleName) : base(roleName)
            {
            }
        public virtual ICollection<EticaretUserRole> UserRoles { get; set; }
        public virtual ICollection<EticaretRoleClaim> RoleClaims { get; set; }
    }

    public class EticaretUserRole : IdentityUserRole<string>
    {
        public EticaretUserRole()
        {

        }
        public virtual EticaretUser User { get; set; }
        public virtual EticaretRole Role { get; set; }

    }

    public class EticaretUserClaim : IdentityUserClaim<string>
    {
        public virtual EticaretUser User { get; set; }
    }

    public class EticaretUserLogin : IdentityUserLogin<string>
    {
        public virtual EticaretUser User { get; set; }
    }

    public class EticaretRoleClaim : IdentityRoleClaim<string>
    {
        public virtual EticaretRole Role { get; set; }
    }

    public class EticaretUserToken : IdentityUserToken<string>
    {
        public virtual EticaretUser User { get; set; }
    }
}
