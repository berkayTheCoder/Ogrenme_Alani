using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalepDeneme.Models
{
    public class UygUser : IdentityUser
    {
        public ICollection<UserTalep> UserTaleps { get; set; }
        public ICollection<UserUrun> UserUruns { get; set; }
    }
}
