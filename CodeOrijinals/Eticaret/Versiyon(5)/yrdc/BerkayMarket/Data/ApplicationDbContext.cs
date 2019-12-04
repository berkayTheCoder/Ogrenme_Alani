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
            builder.Entity<BmUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                        .WithOne(e => e.User)
                        .HasForeignKey(uc => uc.UserId)
                        .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                        .WithOne(e => e.User)
                        .HasForeignKey(ul => ul.UserId)
                        .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                        .WithOne(e => e.User)
                        .HasForeignKey(ut => ut.UserId)
                        .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                        .WithOne(e => e.User)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

            builder.Entity<BmRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                        .WithOne(e => e.Role)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                        .WithOne(e => e.Role)
                        .HasForeignKey(rc => rc.RoleId)
                        .IsRequired();
            });
        }
    }
}
