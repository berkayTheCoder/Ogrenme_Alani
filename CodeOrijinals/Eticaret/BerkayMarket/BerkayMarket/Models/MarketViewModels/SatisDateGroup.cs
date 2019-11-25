using System;
using System.ComponentModel.DataAnnotations;

namespace BerkayMarket.Models.MarketViewModels
{
    public class SiparisDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? SiparisDate { get; set; }

        public int MusteriCount { get; set; }
    }
}