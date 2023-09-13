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
    public class BrukersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrukersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brukers
        public async Task<IActionResult> Index()
        {
              return _context.Bruker != null ? 
                          View(await _context.Bruker.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bruker'  is null.");
        }

        // GET: Brukers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bruker == null)
            {
                return NotFound();
            }

            var bruker = await _context.Bruker
                .FirstOrDefaultAsync(m => m.BrukerID == id);
            if (bruker == null)
            {
                return NotFound();
            }

            return View(bruker);
        }

        // GET: Brukers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brukers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrukerNavn,BrukerEpost")] Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bruker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bruker);
        }

        // GET: Brukers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bruker == null)
            {
                return NotFound();
            }

            var bruker = await _context.Bruker.FindAsync(id);
            if (bruker == null)
            {
                return NotFound();
            }
            return View(bruker);
        }

        // POST: Brukers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrukerNavn,BrukerEpost")] Bruker bruker)
        {
            if (id != bruker.BrukerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bruker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrukerExists(bruker.BrukerID))
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
            return View(bruker);
        }

        // GET: Brukers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bruker == null)
            {
                return NotFound();
            }

            var bruker = await _context.Bruker
                .FirstOrDefaultAsync(m => m.BrukerID == id);
            if (bruker == null)
            {
                return NotFound();
            }

            return View(bruker);
        }

        // POST: Brukers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bruker == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bruker'  is null.");
            }
            var bruker = await _context.Bruker.FindAsync(id);
            if (bruker != null)
            {
                _context.Bruker.Remove(bruker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrukerExists(int id)
        {
          return (_context.Bruker?.Any(e => e.BrukerID == id)).GetValueOrDefault();
        }
    }
}
