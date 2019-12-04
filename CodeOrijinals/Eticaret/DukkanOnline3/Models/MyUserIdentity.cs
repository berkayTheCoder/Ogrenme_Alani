using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline3.Models
{
    public class MyUser : IdentityUser
    {
        public MyUser() : base()
        {

        }
        public MyUser(string userName) : base(userName)
        {
        }

        public virtual ICollection<MyUserClaim> Claims { get; set; }
        public virtual ICollection<MyUserLogin> Logins { get; set; }
        public virtual ICollection<MyUserToken> Tokens { get; set; }
        public virtual ICollection<MyUserRole> UserRoles { get; set; }

        public ICollection<Iyelik> Iyelikleri { get; set; }

        public virtual ICollection<Yorum> Yorumlar { get; set; }
    }
    public class Uretici : MyUser
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


    public class MyRole : IdentityRole
    {
            public MyRole() : base()
            {

            }
            public MyRole(string roleName) : base(roleName)
            {
            }
        public virtual ICollection<MyUserRole> UserRoles { get; set; }
        public virtual ICollection<MyRoleClaim> RoleClaims { get; set; }
    }

    public class MyUserRole : IdentityUserRole<string>
    {
        public MyUserRole()
        {

        }
        public virtual MyUser User { get; set; }
        public virtual MyRole Role { get; set; }

    }

    public class MyUserClaim : IdentityUserClaim<string>
    {
        public virtual MyUser User { get; set; }
    }

    public class MyUserLogin : IdentityUserLogin<string>
    {
        public virtual MyUser User { get; set; }
    }

    public class MyRoleClaim : IdentityRoleClaim<string>
    {
        public virtual MyRole Role { get; set; }
    }

    public class MyUserToken : IdentityUserToken<string>
    {
        public virtual MyUser User { get; set; }
    }
}
