using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Models
{
    public class Mamul : Iyelik { }
    public class Toptan : Mamul { }
    public class Perakende : Toptan { }
    public class Alma : Perakende { }

}
