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

            builder.Entity<Iyelik>(b =>
            {
                b.HasKey(t => new { t.UrunId, t.BmUserId });
            });
            builder.Entity<UrunUrol>(b =>
            {
                b.HasKey(uu =>new { uu.UrunId, uu.UrolId});
            });
        }

        public DbSet<BerkayMarket.Models.Urol> Urol { get; set; }

        public DbSet<BerkayMarket.Models.UrunUrol> UrunUrol { get; set; }

        public DbSet<BerkayMarket.Models.Iyelik> Iyelik { get; set; }
    }
}
