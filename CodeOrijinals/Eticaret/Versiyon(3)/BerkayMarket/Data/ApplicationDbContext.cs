using System;
using System.Collections.Generic;
using System.Text;
using BerkayMarket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BerkayMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext<BmUser,BmRole,string,BmUserClaim,BmUserRole,BmUserLogin,BmRoleClaim,BmUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }
}
