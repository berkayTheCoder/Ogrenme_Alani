using System;
using System.Collections.Generic;
using System.Text;
using DukkanOnline9.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DukkanOnline9.Data
{
    public class ApplicationDbContext : IdentityDbContext<Kullanici,Rol,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kullanici> Kullanicis { get; set; }
        public DbSet<Rol> Rols { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Kullanici>(b =>
            {
                b.HasMany(e => e.KullaniciRolleri)
                    .WithOne()
                    .HasForeignKey(kr => kr.UserId)
                    .IsRequired();
            });
            builder.Entity<Rol>(b =>
            {
                b.HasMany(e => e.RolKullanicilari)
                    .WithOne()
                    .HasForeignKey(kr => kr.RoleId)
                    .IsRequired();
            });
        }
        public DbSet<DukkanOnline9.Models.KullaniciRol> KullaniciRol { get; set; }
    }
}
