using BerkayMarket.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class BmRole : IdentityRole
    {
            public BmRole() : base()
            {

            }
            public BmRole(string roleName) : base(roleName)
            {
            }
        public virtual ICollection<BmUserRole> UserRoles { get; set; }
        public virtual ICollection<BmRoleClaim> RoleClaims { get; set; }
    }



    public class BmUserRole : IdentityUserRole<string>
    {
        public BmUserRole()
        {

        }
        public virtual BmUser User { get; set; }
        public virtual BmRole Role { get; set; }

    }

    public class BmUserClaim : IdentityUserClaim<string>
    {
        public virtual BmUser User { get; set; }
    }

    public class BmUserLogin : IdentityUserLogin<string>
    {
        public virtual BmUser User { get; set; }
    }

    public class BmRoleClaim : IdentityRoleClaim<string>
    {
        public virtual BmRole Role { get; set; }
    }

    public class BmUserToken : IdentityUserToken<string>
    {
        public virtual BmUser User { get; set; }
    }
}
