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
    public class MusterisController : Controller
    {
        private readonly SchoolContext _context;

        public MusterisController(SchoolContext context)
        {
            _context = context;
        }
#endregion

        // GET: Musteris

#if (ScaffoldedIndex)
#region snippet_ScaffoldedIndex
        public async Task<IActionResult> Index()
        {
            return View(await _context.Musteris.ToListAsync());
        }
#endregion
#elif (SortOnly)
#region snippet_SortOnly
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            var Musteris = from s in _context.Musteris
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    Musteris = Musteris.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    Musteris = Musteris.OrderBy(s => s.SiparisDate);
                    break;
                case "date_desc":
                    Musteris = Musteris.OrderByDescending(s => s.SiparisDate);
                    break;
                default:
                    Musteris = Musteris.OrderBy(s => s.LastName);
                    break;
            }
            return View(await Musteris.AsNoTracking().ToListAsync());
        }
#endregion
#elif (SortFilter)
#region snippet_SortFilter
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var Musteris = from s in _context.Musteris
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Musteris = Musteris.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Musteris = Musteris.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    Musteris = Musteris.OrderBy(s => s.SiparisDate);
                    break;
                case "date_desc":
                    Musteris = Musteris.OrderByDescending(s => s.SiparisDate);
                    break;
                default:
                    Musteris = Musteris.OrderBy(s => s.LastName);
                    break;
            }
            return View(await Musteris.AsNoTracking().ToListAsync());
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
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Musteris = from s in _context.Musteris
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Musteris = Musteris.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Musteris = Musteris.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    Musteris = Musteris.OrderBy(s => s.SiparisDate);
                    break;
                case "date_desc":
                    Musteris = Musteris.OrderByDescending(s => s.SiparisDate);
                    break;
                default:
                    Musteris = Musteris.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Musteri>.CreateAsync(Musteris.AsNoTracking(), pageNumber ?? 1, pageSize));
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

            var Musteris = from s in _context.Musteris
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                Musteris = Musteris.Where(s => s.LastName.Contains(searchString)
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
                Musteris = Musteris.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                Musteris = Musteris.OrderBy(e => EF.Property<object>(e, sortOrder));
            }
       
            int pageSize = 3;
            return View(await PaginatedList<Musteri>.CreateAsync(Musteris.AsNoTracking(), 
                page ?? 1, pageSize));
        }
#endregion
#endif

        // GET: Musteris/Details/5
#region snippet_Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Musteri = await _context.Musteris
                .Include(s => s.Sipariss)
                    .ThenInclude(e => e.Urun)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Musteri == null)
            {
                return NotFound();
            }

            return View(Musteri);
        }
#endregion

        // GET: Musteris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musteris/Create
#region snippet_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("SiparisDate,FirstMidName,LastName")] Musteri Musteri)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Musteri);
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
            return View(Musteri);
        }
#endregion

        // GET: Musteris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Musteri = await _context.Musteris
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Musteri == null)
            {
                return NotFound();
            }
            return View(Musteri);
        }

        // POST: Musteris/Edit/5
#if (CreateAndAttach)
#region snippet_CreateAndAttach
        public async Task<IActionResult> Edit(int id, [Bind("ID,SiparisDate,FirstMidName,LastName")] Musteri Musteri)
        {
            if (id != Musteri.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Musteri);
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
            return View(Musteri);
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
            var MusteriToUpdate = await _context.Musteris.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Musteri>(
                MusteriToUpdate,
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
            return View(MusteriToUpdate);
        }
#endregion
#endif

        // GET: Musteris/Delete/5
#region snippet_DeleteGet
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Musteri = await _context.Musteris
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Musteri == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(Musteri);
        }
#endregion
        // POST: Musteris/Delete/5
#if (DeleteWithReadFirst)
#region snippet_DeleteWithReadFirst
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Musteri = await _context.Musteris.FindAsync(id);
            if (Musteri == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Musteris.Remove(Musteri);
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
                Musteri MusteriToDelete = new Musteri() { ID = id };
                _context.Entry(MusteriToDelete).State = EntityState.Deleted;
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
