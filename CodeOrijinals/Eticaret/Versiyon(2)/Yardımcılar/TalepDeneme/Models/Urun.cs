using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalepDeneme.Models
{
    public class Urun
    {
        public ICollection<UrunTalep> UrunTaleps { get; set; }
        public ICollection<UserUrun> UserUruns { get; set; }
    }
}
