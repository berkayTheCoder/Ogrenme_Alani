#define AfterInheritance // or Intro or TableNames or BeforeInheritance

#if Intro
#region snippet_Intro
using KendiDukkanim.Models;
using Microsoft.EntityFrameworkCore;

namespace KendiDukkanim.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Siparis> Sipariss { get; set; }
        public DbSet<Musteri> Musteris { get; set; }
    }
}
#endregion

#elif TableNames
#region snippet_TableNames
using KendiDukkanim.Models;
using Microsoft.EntityFrameworkCore;

namespace KendiDukkanim.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Siparis> Sipariss { get; set; }
        public DbSet<Musteri> Musteris { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().ToTable("Urun");
            modelBuilder.Entity<Siparis>().ToTable("Siparis");
            modelBuilder.Entity<Musteri>().ToTable("Musteri");
        }
    }
}
#endregion

#elif BeforeInheritance
#region snippet_BeforeInheritance
using KendiDukkanim.Models;
using Microsoft.EntityFrameworkCore;

namespace KendiDukkanim.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Siparis> Sipariss { get; set; }
        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Satici> Saticis { get; set; }
        public DbSet<Dukkan> Dukkans { get; set; }
        public DbSet<Satis> Satiss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().ToTable("Urun");
            modelBuilder.Entity<Siparis>().ToTable("Siparis");
            modelBuilder.Entity<Musteri>().ToTable("Musteri");
            modelBuilder.Entity<Kategori>().ToTable("Kategori");
            modelBuilder.Entity<Satici>().ToTable("Satici");
            modelBuilder.Entity<Dukkan>().ToTable("Dukkan");
            modelBuilder.Entity<Satis>().ToTable("Satis");

            modelBuilder.Entity<Satis>()
                .HasKey(c => new { c.UrunID, c.SaticiID });
        }
    }
}
#endregion
#elif AfterInheritance
#region snippet_AfterInheritance
using KendiDukkanim.Models;
using Microsoft.EntityFrameworkCore;

namespace KendiDukkanim.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Siparis> Sipariss { get; set; }
        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Satici> Saticis { get; set; }
        public DbSet<Dukkan> Dukkans { get; set; }
        public DbSet<Satis> Satiss { get; set; }
        public DbSet<Kisi> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().ToTable("Urun");
            modelBuilder.Entity<Siparis>().ToTable("Siparis");
            modelBuilder.Entity<Musteri>().ToTable("Musteri");
            modelBuilder.Entity<Kategori>().ToTable("Kategori");
            modelBuilder.Entity<Satici>().ToTable("Satici");
            modelBuilder.Entity<Dukkan>().ToTable("Dukkan");
            modelBuilder.Entity<Satis>().ToTable("Satis");
            modelBuilder.Entity<Kisi>().ToTable("Kisi");

            modelBuilder.Entity<Satis>()
                .HasKey(c => new { c.UrunID, c.SaticiID });
        }
    }
}
#endregion
#endif