using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{

    public class BmUser : IdentityUser<string>
    {
        public BmUser() : base()
        {

        }
        public BmUser(string userName) : base(userName)
        {
        }

        public virtual ICollection<BmUserClaim> Claims { get; set; }
        public virtual ICollection<BmUserLogin> Logins { get; set; }
        public virtual ICollection<BmUserToken> Tokens { get; set; }
        public virtual ICollection<BmUserRole> UserRoles { get; set; }

#if true // Version02

        public ICollection<Iyelik> Iyeliks { get; set; }
        public List<Urun> Uruns { get; set; }


#elif false //Bidahaki versiyonda yapacağım   
        //public ICollection<BmUserTalep> Taleps { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; } 
#endif
    }

}
