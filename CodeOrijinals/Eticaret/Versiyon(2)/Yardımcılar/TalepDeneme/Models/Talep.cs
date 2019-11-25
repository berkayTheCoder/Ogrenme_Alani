using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalepDeneme.Models
{
    public class Talep
    {
    }

    public class UserTalep
    {
        public int MyProperty { get; set; }
        public Talep Talep { get; set; }
    }

    public class UrunTalep
    {
        public Urun Urun { get; set; }
        public Talep Talep { get; set; }
    }
}
