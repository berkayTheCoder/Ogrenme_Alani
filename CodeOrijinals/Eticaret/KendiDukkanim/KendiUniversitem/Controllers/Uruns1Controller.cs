#define Index1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KendiDukkanim.Data;
using KendiDukkanim.Models;

namespace KendiDukkanim.Controllers
{
    public class Uruns1Controller : Controller
    {
        private readonly SchoolContext _context;

        public Uruns1Controller(SchoolContext context)
        {
            _context = context;
        }
#if Index0
        // GET: Uruns1
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Uruns.Include(u => u.Kategori);
            return View(await schoolContext.ToListAsync());
        }
#elif Index1
        #region snippet_SortFilterPage
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["AdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ad_desc" : "";
            ViewData["StokSortParm"] = sortOrder == "Stok" ? "stok_desc" : "Stok";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Uruns = from s in _context.Uruns
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Uruns = Uruns.Where(s => s.Adi.Contains(searchString)
                                       || s.KisaAciklama.Contains(searchString)
                                       || s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Uruns = Uruns.OrderByDescending(s => s.Adi);
                    break;
                case "Date":
                    Uruns = Uruns.OrderBy(s => s.Stok);
                    break;
                case "date_desc":
                    Uruns = Uruns.OrderByDescending(s => s.Stok);
                    break;
                default:
                    Uruns = Uruns.OrderBy(s => s.Adi);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Urun>.CreateAsync(Uruns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion
#endif


    }
}
