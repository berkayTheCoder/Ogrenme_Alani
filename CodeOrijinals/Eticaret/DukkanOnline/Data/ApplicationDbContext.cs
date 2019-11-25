using System;
using System.Collections.Generic;
using System.Text;
using DukkanOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DukkanOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Toptanci> Toptancis { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DukkanOnline.Models.Urun> Uruns { get; set; }
        public DbSet<DukkanOnline.Models.Envanter> Envanter { get; set; }
    }
}
