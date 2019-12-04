using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerkayMarket.Models
{
    public class Envanter
    {
        public int SaticiID { get; set; }
        public int UrunID { get; set; }
        public Satici Satici { get; set; }
        public Urun Urun { get; set; }
    }
}