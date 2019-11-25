using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BerkayMarket.Models;

namespace BerkayMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BerkayMarket.Models.Tanislik> Tanislik { get; set; }
        public DbSet<BerkayMarket.Models.Urun> Uruns { get; set; }

        public DbSet<BerkayMarket.Models.Kategori> Kategoris { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Tanislik>(b =>
            {
                //b.HasKey(t => new { t.IyeId, t.TanisId });
            });
        }
    }
}
