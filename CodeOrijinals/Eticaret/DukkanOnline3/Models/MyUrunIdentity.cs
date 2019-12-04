using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline3.Models
{
    public class Mamul : Urun { }
    public class Toptan : Mamul { }
    public class Perakende : Toptan { }
    public class Siparis : Perakende { }
    public class Alim : Siparis { }
    public class Satim : Siparis { }


}
