using System.Collections;
using System.Collections.Generic;

namespace DukkanOnline3.Models
{
    public class Iyelik
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int UrunID { get; set; }
        public Urun Urun { get; set; }
        public string MyUserID { get; set; }
        public MyUser MyUsers { get; set; }

    }
}