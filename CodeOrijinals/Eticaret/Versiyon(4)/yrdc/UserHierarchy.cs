using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Models
{
#if true //Sonraki Versiyonda Urun İlişkileri
    /// <summary>
    /// Iyeliğine göre User hiyerarşisi
    /// </summary>
    public class Alici : EticaretUser { }
    public class Perakendeci : Alici { }
    public class Toptanci : Perakendeci { }
    public class Uretici : Toptanci { }
#endif
}
