using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
#if true 
    public class BmUretici : BmToptanci
    {
        public ICollection<Urun> Mamuls { get; set; }
    }
    public class BmToptanci : BmSatici
    {

        public ICollection<Urun> Toptans { get; set; }
    }
    public class BmSatici : BmUser
    {
        public ICollection<Urun> Perakendes { get; set; }
    }
#endif
}
