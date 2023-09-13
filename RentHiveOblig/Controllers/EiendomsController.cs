using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;

namespace RentHiveOblig.Controllers
{
    public class EiendomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EiendomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eiendoms
        public async Task<IActionResult> Index()
        {
              return _context.Eiendom != null ? 
                          View(await _context.Eiendom.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Eiendom'  is null.");
        }

        // GET: Eiendoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Eiendom == null)
            {
                return NotFound();
            }

            var eiendom = await _context.Eiendom
                .FirstOrDefaultAsync(m => m.EiendomID == id);
            if (eiendom == null)
            {
                return NotFound();
            }

            return View(eiendom);
        }

        // GET: Eiendoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eiendoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EiendomName,EiendomDescription")] Eiendom eiendom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eiendom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eiendom);
        }

        // GET: Eiendoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Eiendom == null)
            {
                return NotFound();
            }

            var eiendom = await _context.Eiendom.FindAsync(id);
            if (eiendom == null)
            {
                return NotFound();
            }
            return View(eiendom);
        }

        // POST: Eiendoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EiendomName,EiendomDescription")] Eiendom eiendom)
        {
            if (id != eiendom.EiendomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eiendom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EiendomExists(eiendom.EiendomID))
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
            return View(eiendom);
        }

        // GET: Eiendoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eiendom == null)
            {
                return NotFound();
            }

            var eiendom = await _context.Eiendom
                .FirstOrDefaultAsync(m => m.EiendomID == id);
            if (eiendom == null)
            {
                return NotFound();
            }

            return View(eiendom);
        }

        // POST: Eiendoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Eiendom == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Eiendom'  is null.");
            }
            var eiendom = await _context.Eiendom.FindAsync(id);
            if (eiendom != null)
            {
                _context.Eiendom.Remove(eiendom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EiendomExists(int id)
        {
          return (_context.Eiendom?.Any(e => e.EiendomID == id)).GetValueOrDefault();
        }
    }
}
