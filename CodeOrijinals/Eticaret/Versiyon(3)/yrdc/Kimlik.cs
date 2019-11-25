using Eticaret.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Models
{
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
