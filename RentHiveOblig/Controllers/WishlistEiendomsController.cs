using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;

namespace RentHiveOblig.Controllers
{
    public class WishlistEiendomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistEiendomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WishlistEiendoms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WishlistEiendom.Include(w => w.Eiendom).Include(w => w.Wishlist);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WishlistEiendoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WishlistEiendom == null)
            {
                return NotFound();
            }

            var wishlistEiendom = await _context.WishlistEiendom
                .Include(w => w.Eiendom)
                .Include(w => w.Wishlist)
                .FirstOrDefaultAsync(m => m.WishListEiendomId == id);
            if (wishlistEiendom == null)
            {
                return NotFound();
            }

            return View(wishlistEiendom);
        }

        // GET: WishlistEiendoms/Create
        public IActionResult Create()
        {
            ViewData["EiendomId"] = new SelectList(_context.Eiendom, "EiendomID", "Tittel");
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "WishlistId", "WishlistId");
            return View();
        }

        // POST: WishlistEiendoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WishListEiendomId,WishlistId,EiendomId")] WishlistEiendom wishlistEiendom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishlistEiendom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EiendomId"] = new SelectList(_context.Eiendom, "EiendomID", "Tittel", wishlistEiendom.EiendomId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "WishlistId", "WishlistId", wishlistEiendom.WishlistId);
            return View(wishlistEiendom);
        }

        // GET: WishlistEiendoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WishlistEiendom == null)
            {
                return NotFound();
            }

            var wishlistEiendom = await _context.WishlistEiendom.FindAsync(id);
            if (wishlistEiendom == null)
            {
                return NotFound();
            }
            ViewData["EiendomId"] = new SelectList(_context.Eiendom, "EiendomID", "Tittel", wishlistEiendom.EiendomId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "WishlistId", "WishlistId", wishlistEiendom.WishlistId);
            return View(wishlistEiendom);
        }

        // POST: WishlistEiendoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WishListEiendomId,WishlistId,EiendomId")] WishlistEiendom wishlistEiendom)
        {
            if (id != wishlistEiendom.WishListEiendomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishlistEiendom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistEiendomExists(wishlistEiendom.WishListEiendomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EiendomId"] = new SelectList(_context.Eiendom, "EiendomID", "Tittel", wishlistEiendom.EiendomId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "WishlistId", "WishlistId", wishlistEiendom.WishlistId);
            return View(wishlistEiendom);
        }

        // GET: WishlistEiendoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WishlistEiendom == null)
            {
                return NotFound();
            }

            var wishlistEiendom = await _context.WishlistEiendom
                .Include(w => w.Eiendom)
                .Include(w => w.Wishlist)
                .FirstOrDefaultAsync(m => m.WishListEiendomId == id);
            if (wishlistEiendom == null)
            {
                return NotFound();
            }

            return View(wishlistEiendom);
        }

        // POST: WishlistEiendoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WishlistEiendom == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WishlistEiendom'  is null.");
            }
            var wishlistEiendom = await _context.WishlistEiendom.FindAsync(id);
            if (wishlistEiendom != null)
            {
                _context.WishlistEiendom.Remove(wishlistEiendom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistEiendomExists(int id)
        {
            return (_context.WishlistEiendom?.Any(e => e.WishListEiendomId == id)).GetValueOrDefault();
        }
    }
}
