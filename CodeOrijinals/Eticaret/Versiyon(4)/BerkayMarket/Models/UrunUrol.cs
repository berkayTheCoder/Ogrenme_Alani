using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class UrunUrol
    {
        //public UrunUrol(Urun urun, ClaimsPrincipal user)
        //{
        //    Urun = urun;
        //}

        public int UrunId { get; set; }
        public Urun Urun { get; set; }

        public int UrolId { get; set; }
        public Urol Urol { get; set; }

    }
}
