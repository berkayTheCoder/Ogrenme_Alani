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
    public class VeritabaniContext: DbContext//ismi uygun değil sonraki verisyonda değiştirelim
    {
        public VeritabaniContext(DbContextOptions<VeritabaniContext> options):base(options)
        {

        }
        public DbSet<Urun> Urunler { get; set; } //Add,Remove,ToList,where(x=>x.),single,first
        //saveChanges(); --> Gerçek veritabanına yazılır  // vtys: SqlServer, Oracle,Sqlite,MySql

        public DbSet<Kategori> Kategoriler { get; set; }

        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}
