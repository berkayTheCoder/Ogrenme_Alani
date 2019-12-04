using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models
{
    public class Mamul : Urun
    {
    }//uretici
    public class Toptan : Mamul
    {
    }//toptanci
    public class Perakende : Toptan
    {
    }//satici
    public class Alma : Perakende
    {
    }//Alici

}
