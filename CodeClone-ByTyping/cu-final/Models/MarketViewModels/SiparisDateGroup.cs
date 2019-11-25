using System;
using System.ComponentModel.DataAnnotations;

namespace BerkayMarket.Models.SchoolViewModels
{
    public class SiparisDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? SiparisDate { get; set; }

        public int MusteriCount { get; set; }
    }
}