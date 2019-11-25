using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DukkanOnline2.Models
{
    public class DukkanOnline2Context : DbContext
    {
        public DukkanOnline2Context (DbContextOptions<DukkanOnline2Context> options)
            : base(options)
        {
        }

        public DbSet<DukkanOnline2.Models.Sepet> Sepet { get; set; }
    }
}
