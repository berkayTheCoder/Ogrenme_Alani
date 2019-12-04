using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerkayMarket.Models
{
    #region snippet1
    public class Tanislik 
    {
        public int Id { get; set; }
        // BmUserdan User için seçilecek
        public string IyeId { get; set; }

        public string TanisId { get; set; }
        // user ID from AspNetUser table.
        public BmUser Tanis { get; set; }
 // BmUser Listesindeki yönlendirmeden Tanışlarının bilgisine ulaşacak

        [Display(Name = "Durumu")]
        public TanislikDurumlari Durumlar { get; set; }
    }

    public enum TanislikDurumlari
    {
        İstek,
        Onaylı,
        Red
    }
    #endregion
}