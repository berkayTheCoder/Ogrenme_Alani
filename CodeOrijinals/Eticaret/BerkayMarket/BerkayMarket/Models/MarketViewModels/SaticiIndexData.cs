using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Models.MarketViewModels
{
    public class SaticiIndexData
    {
        public IEnumerable<Satici> Saticis { get; set; }
        public IEnumerable<Urun> Uruns { get; set; }
        public IEnumerable<Siparis> Sipariss { get; set; }
    }
}