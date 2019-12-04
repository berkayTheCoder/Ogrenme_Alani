#define SortFilterPage //or ScaffoldedIndex or SortOnly or SortFilter or DynamicLinq
#define CreateAndAttach //ReadFirst or 
#define DeleteWithoutReadFirst //DeleteWithReadFirst or 

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KendiDukkanim.Data;
using KendiDukkanim.Models;
using System;
using Microsoft.Extensions.Logging;

#region snippet_Context
namespace KendiDukkanim.Controllers
{
    public class UrunsController : Controller
    {
        private readonly SchoolContext _context;

        public UrunsController(SchoolContext context)
        {
            _context = context;
        }
#endregion

        // GET: Uruns

#if (ScaffoldedIndex)
#region snippet_ScaffoldedIndex
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uruns.ToListAsync());
        }
#endregion
#elif (SortOnly)
#region snippet_SortOnly
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            var Uruns = from s in _context.Uruns
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    Uruns = Uruns.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    Uruns = Uruns.OrderBy(s => s.SiparisDate);
                    break;
                case "date_desc":
                    Uruns = Uruns.OrderByDescending(s => s.SiparisDate);
                    break;
                default:
                    Uruns = Uruns.OrderBy(s => s.LastName);
                    break;
            }
            return View(await Uruns.AsNoTracking().ToListAsync());
        }
#endregion
#elif (SortFilter)
#region snippet_SortFilter
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var Uruns = from s in _context.Uruns
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Uruns = Uruns.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Uruns = Uruns.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    Uruns = Uruns.OrderBy(s => s.SiparisDate);
                    break;
                case "date_desc":
                    Uruns = Uruns.OrderByDescending(s => s.SiparisDate);
                    break;
                default:
                    Uruns = Uruns.OrderBy(s => s.LastName);
                    break;
            }
            return View(await Uruns.AsNoTracking().ToListAsync());
        }
#endregion
#elif (SortFilterPage)
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
                                       || s.KisaAciklama.Contains(searchString));
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
#elif (DynamicLinq)
#region snippet_DynamicLinq
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = 
                String.IsNullOrEmpty(sortOrder) ? "LastName_desc" : "";
            ViewData["DateSortParm"] = 
                sortOrder == "SiparisDate" ? "SiparisDate_desc" : "SiparisDate";

            if (searchString != null)
            {
                page = 1;
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
                Uruns = Uruns.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "LastName";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                Uruns = Uruns.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                Uruns = Uruns.OrderBy(e => EF.Property<object>(e, sortOrder));
            }
       
            int pageSize = 3;
            return View(await PaginatedList<Urun>.CreateAsync(Uruns.AsNoTracking(), 
                page ?? 1, pageSize));
        }
#endregion
#endif

        // GET: Uruns/Details/5
#region snippet_Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Urun = await _context.Uruns
                .Include(s => s.UrunSaticilari)
                    .ThenInclude(e => e.Dukkan)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UrunID == id);

            if (Urun == null)
            {
                return NotFound();
            }

            return View(Urun);
        }
#endregion

        // GET: Uruns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uruns/Create
#region snippet_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Adi,Stok,KisaAciklama,UzunAciklama,SaticiId,")] Urun urun)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(urun);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(urun);
        }
#endregion

        // GET: Uruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Urun = await _context.Uruns
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (Urun == null)
            {
                return NotFound();
            }
            return View(Urun);
        }

        // POST: Uruns/Edit/5
#if (CreateAndAttach)
#region snippet_CreateAndAttach
        public async Task<IActionResult> Edit(int id, [Bind("UrunID,SiparisDate,FirstMidName,LastName")] Urun Urun)
        {
            if (id != Urun.UrunID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Urun);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(Urun);
        }
#endregion
#elif (ReadFirst)
#region snippet_ReadFirst
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var UrunToUpdate = await _context.Uruns.FirstOrDefaultAsync(s => s.UrunID == id);
            if (await TryUpdateModelAsync<Urun>(
                UrunToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.SiparisDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(UrunToUpdate);
        }
#endregion
#endif

        // GET: Uruns/Delete/5
#region snippet_DeleteGet
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Urun = await _context.Uruns
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (Urun == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(Urun);
        }
#endregion
        // POST: Uruns/Delete/5
#if (DeleteWithReadFirst)
#region snippet_DeleteWithReadFirst
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Urun = await _context.Uruns.FindAsync(id);
            if (Urun == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Uruns.Remove(Urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
#endregion
#elif (DeleteWithoutReadFirst)
#region snippet_DeleteWithoutReadFirst
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Urun UrunToDelete = new Urun() { UrunID = id };
                _context.Entry(UrunToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
#endregion
#endif
    }
}
