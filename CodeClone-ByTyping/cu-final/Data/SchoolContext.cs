using BerkayMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace BerkayMarket.Data
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }

        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Siparis> Sipariss { get; set; }
        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Satici> Saticis { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Envanter> Envanters { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().ToTable("Urun");
            modelBuilder.Entity<Siparis>().ToTable("Siparis");
            modelBuilder.Entity<Musteri>().ToTable("Musteri");
            modelBuilder.Entity<Kategori>().ToTable("Kategori");
            modelBuilder.Entity<Satici>().ToTable("Satici");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<Envanter>().ToTable("Envanter");
            modelBuilder.Entity<Person>().ToTable("Person");

            modelBuilder.Entity<Envanter>()
                .HasKey(c => new { c.UrunID, c.SaticiID });
        }
    }
}