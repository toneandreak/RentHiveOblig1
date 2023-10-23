using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;
using RentHiveOblig.ViewModels;

namespace RentHiveOblig.Controllers
{
    public class EiendomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EiendomsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;


        public EiendomsController(ApplicationDbContext context, ILogger<EiendomsController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
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

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eiendoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EiendomViewModel model)
       {

            //Check if it finds userId. 

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("The userId is null or empty.");

                return Forbid();

            }

            //Check if the modelstate is valid.

            if (!ModelState.IsValid)
            {
                _logger.LogError("The ModelState is not valid.");
                return View(model);
            }


                //Try to create the model.
            
            try{

                Eiendom eiendom = new Eiendom
                    {
                        ApplicationUserId = userId,
                        PrisPerNatt = model.PrisPerNatt,
                        Tittel = model.Tittel,
                        Beskrivelse = model.Beskrivelse,
                        Street = model.Street,
                        City = model.City,
                        Country = model.Country,
                        ZipCode = model.ZipCode,
                        State = model.State,
                        Soverom = model.Soverom,
                        Bad = model.Bad,
                        CreatedDateTime = DateTime.Now
                    };


                    _context.Eiendom.Add(eiendom);

                        await _context.SaveChangesAsync();

                 } catch(Exception ex)
            {

                    _logger.LogError(ex, "An error occured while creating the property."); 
                //Need to return something here too. 
            }

                return RedirectToAction("Index", "Hosting");

        }





        /* THE OLD CREATE: 
         
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
        */




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



            //EXTRA CONTROL TO PREVENT OTHER USERS BEING ABLE TO EDIT/SEE OTHER'S LISTINGS.

            var userId = _userManager.GetUserId(User);

            if(userId == null || userId != eiendom.ApplicationUserId) {

                return Forbid(); 
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
