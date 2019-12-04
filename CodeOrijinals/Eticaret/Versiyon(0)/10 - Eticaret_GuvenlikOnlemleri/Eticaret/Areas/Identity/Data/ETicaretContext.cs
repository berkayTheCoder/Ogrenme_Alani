using System;
using System.Collections.Generic;
using System.Text;
//using DukkanOnline3.Models;
using Eticaret.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Areas.Identity.Data
{
    /// <summary>
    /// bu bağlamın da ismi uygun değil. KimlikVTContext olarak değiştireyim sonraki versiyonunda
    /// </summary>
    public class EticaretContext : IdentityDbContext<EticaretUser, EticaretRole, string, EticaretUserClaim, EticaretUserRole, EticaretUserLogin, EticaretRoleClaim, EticaretUserToken>
    {
        public EticaretContext(DbContextOptions<EticaretContext> options)
            : base(options)
        {
        }
        #region Planlananlar

#if false // bu versiyonda Uygulama ilişkileri bu bağlamda tutulmayacak
        public DbSet<Kategori> Kategoris { get; set; }
        //public DbSet<AltKategori> AltKategoris { get; set; }
        public DbSet<Urun> Uruns { get; set; } 
#endif

#if false // Yakında UrunHiyerarşisi İyelik ve UserHiyerarşisi
        public DbSet<Mamul> Mamuls { get; set; }
        public DbSet<Toptan> Toptans { get; set; }
        public DbSet<Perakende> Perakendes { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<Alim> Alims { get; set; }
        public DbSet<Satim> Satims { get; set; }

        public DbSet<Iyelik> Iyeliks { get; set; } 

        public DbSet<Uretici> Ureticis { get; set; }
        public DbSet<Toptanci> Toptancis { get; set; }
        public DbSet<Perakendeci> Perakendecis { get; set; }
        public DbSet<Alici> Alicis { get; set; }
        public DbSet<Satici> Saticis { get; set; }
#endif

#if false //Sonrki versiyonda Yorumlr ve arkadaşlar
        public DbSet<Yorum> Yorums { get; set; }
        public DbSet<Contact> Contacts { get; set; } 
#endif 
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EticaretUser>(b =>
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

            modelBuilder.Entity<EticaretRole>(b =>
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
