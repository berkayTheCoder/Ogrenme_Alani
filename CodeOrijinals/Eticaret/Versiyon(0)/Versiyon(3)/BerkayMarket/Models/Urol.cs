using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class Urol
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public ICollection<UrunUrol> UrunUrols { get; set; }
    }
}
