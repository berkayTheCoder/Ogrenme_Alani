using Eticaret.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Data
{
    /// <summary>
    /// Sonraki versiyonda UygulamVTContext olarak değiştireyim.
    /// </summary>
    public class UygVTContext: DbContext//ismi uygun değil sonraki verisyonda değiştirelim
    {
        public UygVTContext(DbContextOptions<UygVTContext> options):base(options)
        {

        }
        public DbSet<Urun> Urunler { get; set; } 
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Eticaret.Models.Iyelik> Iyelik { get; set; }
        public DbSet<Eticaret.Models.Yorum> Yorum { get; set; }
        public DbSet<Eticaret.Models.Contact> Contact { get; set; }

    }
}
