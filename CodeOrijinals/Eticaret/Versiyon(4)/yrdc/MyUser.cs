using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Models
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

#if true // Version02

        public ICollection<Iyelik> Iyeliks { get; set; }

#elif false //Bidahaki versiyonda yapacağım   
        //public ICollection<EticaretUserTalep> Taleps { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; } 
#endif
    }

}
