using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DukkanOnline4.Models
{
    public class DukkanOnline4Context : DbContext
    {
        public DukkanOnline4Context (DbContextOptions<DukkanOnline4Context> options)
            : base(options)
        {
        }

        public DbSet<DukkanOnline4.Models.Contact> Contact { get; set; }
    }
}
