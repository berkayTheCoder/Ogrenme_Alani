using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KendiDukkanim.Models;

namespace KendiDukkanim.Models
{
    public class KendiDukkanimContext : DbContext
    {
        public KendiDukkanimContext (DbContextOptions<KendiDukkanimContext> options)
            : base(options)
        {
        }

        public DbSet<KendiDukkanim.Models.Satici> Satici { get; set; }
        public DbSet<KendiDukkanim.Models.Musteri> Musteri { get; set; }
        public DbSet<KendiDukkanim.Models.Urun> Urun { get; set; }
        public DbSet<KendiDukkanim.Models.Siparis> Siparis { get; set; }
        public DbSet<KendiDukkanim.Models.Satis> Satis { get; set; }
        public DbSet<KendiDukkanim.Models.Kategori> Kategori { get; set; }
        public DbSet<KendiDukkanim.Models.Dukkan> Dukkan { get; set; }
        public DbSet<KendiDukkanim.Models.Satici1> Satici1 { get; set; }
    }
}
