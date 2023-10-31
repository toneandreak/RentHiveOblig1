using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.DAL;
using RentHiveOblig.Models;
using RentHiveOblig.ViewModels;
using System.Security.Claims;

namespace RentHiveOblig.Controllers
{
    public class EiendomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EiendomsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;


        public EiendomsController(ApplicationDbContext context, ILogger<EiendomsController> logger, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }


        // POST: Search


        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Prispernatt" ? "price_desc" : "Price";
            var eiendom = from s in _context.Eiendom
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                eiendom = eiendom.Where(s => s.Tittel.Contains(searchString)
                                       || s.Beskrivelse.Contains(searchString)
                                       || s.Country.Contains(searchString)
                                         || s.City.Contains(searchString));
            }
            switch (sortOrder)
            {

                case "Price":
                    eiendom = eiendom.OrderBy(s => s.PrisPerNatt);
                    break;
                
            }

            return View(eiendom.ToList());
        }





        //GET: Eiendoms/ListingDetails/X

        public async Task<IActionResult> ListingDetails(int? id)
        {

            _logger.LogInformation($"Trying to access ListingDetails for 'eiendom' IO {id}. ");
            if (id == null)
            {
                _logger.LogError($"ListingDetails for 'eiendom' IO {id} not found. ");
                return NotFound();

            }

            var eiendom = await _context.Eiendom.Include(e => e.ApplicationUser)
                                                .FirstOrDefaultAsync(m => m.EiendomID == id);

            if (eiendom == null)
            {
                _logger.LogError($"Eiendom with ID {id} not found.");
                return NotFound();
            }


            //Showing errormessages
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }


            _logger.LogInformation($"Access to ListingDetails with {id} is successfull.");
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

            try
            {

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

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occured while creating the property.");
                //Need to return something here too. 
            }

            return RedirectToAction("Index", "Hosting");

        }



        //IMAGE UPLOAD

        // CODE (IMAGE UPLOAD) INSPIRED FROM https://www.codaffection.com/asp-net-core-article/asp-net-core-mvc-image-upload-and-retrieve/

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(IFormFile? file1, IFormFile? file2, IFormFile? file3, int EiendomID)
        {


            //if it does not retrieve eiendomID then return NotFound. 

            if (EiendomID == 0)
            {
                return NotFound("The Property ID does not exist.");
            }


            var eiendom = await _context.Eiendom.FindAsync(EiendomID);
            if (eiendom == null)
            {
                return NotFound("The property cannot be found.");

            }



            //We set the path where we want to store the file to wwwroot/Images

            //For some reason it only works with forwardSlash so we cannot use the path combine to set the path in the model.
            //We therefore use a pathFowardSlash which will be the input for the model.

            //We check if the input is null for each file with a if-statment
            //There are probably ways to do this better, but it works - and might not be so bad since it is only three images at max. 


            if (file1 != null)
            {
                var pathForwardSlash1 = "/Images/" + file1.FileName;

                var webPath1 = "Images" + Path.DirectorySeparatorChar + file1.FileName;
                var fullPath1 = Path.Combine(_hostEnvironment.WebRootPath, webPath1);

                //Some logging used for debugging on earlier issues
                _logger.LogInformation("webpath is: " + webPath1);
                _logger.LogInformation("fullpath is: " + fullPath1);
                _logger.LogInformation("hostEnvironment webrootpath: " + _hostEnvironment.WebRootPath);

                using (var stream = new FileStream(fullPath1, FileMode.Create))
                {
                    await file1.CopyToAsync(stream);
                }

                eiendom.Image1 = pathForwardSlash1;
                _context.Update(eiendom);

            }
            if (file2 != null)
            {
                var pathForwardSlash2 = "/Images/" + file2.FileName;

                var webPath2 = "Images" + Path.DirectorySeparatorChar + file2.FileName;
                var fullPath2 = Path.Combine(_hostEnvironment.WebRootPath, webPath2);

                //Some logging used for debugging on earlier issues
                _logger.LogInformation("webpath is: " + webPath2);
                _logger.LogInformation("fullpath is: " + fullPath2);
                _logger.LogInformation("hostEnvironment webrootpath: " + _hostEnvironment.WebRootPath);

                using (var stream = new FileStream(fullPath2, FileMode.Create))
                {
                    await file2.CopyToAsync(stream);
                }

                eiendom.Image2 = pathForwardSlash2;
                _context.Update(eiendom);

            }

            if (file3 != null)
            {

                var pathForwardSlash3 = "/Images/" + file3.FileName;

                var webPath3 = "Images" + Path.DirectorySeparatorChar + file3.FileName;
                var fullPath3 = Path.Combine(_hostEnvironment.WebRootPath, webPath3);

                //Some logging used for debugging on earlier issues
                _logger.LogInformation("webpath is: " + webPath3);
                _logger.LogInformation("fullpath is: " + fullPath3);
                _logger.LogInformation("hostEnvironment webrootpath: " + _hostEnvironment.WebRootPath);

                using (var stream = new FileStream(fullPath3, FileMode.Create))
                {
                    await file3.CopyToAsync(stream);
                }
                eiendom.Image3 = pathForwardSlash3;
                _context.Update(eiendom);
            }




            //Finally save the changes.

            await _context.SaveChangesAsync();


            //Not sure where to return yet. 
            return RedirectToAction("Index", "Hosting");
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



            //EXTRA CONTROL TO PREVENT OTHER USERS BEING ABLE TO EDIT/SEE OTHER'S LISTINGS.

            var userId = _userManager.GetUserId(User);

            _logger.LogInformation("userId = " + userId);
            _logger.LogInformation("Property Application user Id = " + eiendom.ApplicationUserId);

            if (userId == null || userId != eiendom.ApplicationUserId)
            {


                _logger.LogError("userId is not equal to 'eiendom.ApplicationUserId or is null'");

                return Forbid();
            }


            return View(eiendom);
        }


        // POST: Eiendoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationUserId, PrisPerNatt, Tittel, Beskrivelse, Street, City, Country, ZipCode, State, Soverom, Bad")] Eiendom eiendom)
        {
            var existingEiendom = await _context.Eiendom.FindAsync(id);

            if (existingEiendom == null)
            {
                return NotFound("The EiendomID could not be found.");
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null || userId != existingEiendom.ApplicationUserId)
            {
                _logger.LogError($"Not-Authorized user tried to edit property ID {id}. The user was: {userId}. The user for the listing is: {existingEiendom.ApplicationUserId}");
                return Forbid();
            }


            //For some reason, the ApplicationUser is invalid in Modelstate, so I had to remove it from the modelState, however I am doing the control manually. 
            //https://www.appsloveworld.com/entity-framework/100/61/modelstate-errors-for-all-navigation-properties?expand_article=1
            ModelState.Remove("ApplicationUser");

            if (ModelState.IsValid && userId == existingEiendom.ApplicationUserId)
            {
                try
                {
                    existingEiendom.Tittel = eiendom.Tittel;
                    existingEiendom.Beskrivelse = eiendom.Beskrivelse;
                    existingEiendom.PrisPerNatt = eiendom.PrisPerNatt;
                    existingEiendom.Street = eiendom.Street;
                    existingEiendom.City = eiendom.City;
                    existingEiendom.Country = eiendom.Country;
                    existingEiendom.ZipCode = eiendom.ZipCode;
                    existingEiendom.State = eiendom.State;
                    existingEiendom.Soverom = eiendom.Soverom;
                    existingEiendom.Bad = eiendom.Bad;

                    _context.Update(existingEiendom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EiendomExists(existingEiendom.EiendomID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Hosting");
            }

            return View(existingEiendom);
        }






        [Authorize]
        // GET: Eiendoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eiendom == null)
            {
                return NotFound();
            }






            var eiendom = await _context.Eiendom
            .FirstOrDefaultAsync(m => m.EiendomID == id);



            //EXTRA CONTROL TO PREVENT OTHER USERS BEING ABLE TO EDIT/SEE OTHER'S LISTINGS.
            var userId = _userManager.GetUserId(User);

            _logger.LogInformation("userId = " + userId);
            _logger.LogInformation("Property Application user Id = " + eiendom.ApplicationUserId);

            if (userId == null || userId != eiendom.ApplicationUserId)
            {


                _logger.LogError("userId is not equal to 'eiendom.ApplicationUserId or is null'");

                return Forbid();
            }



            if (eiendom == null)
            {
                return NotFound();
            }

            return View(eiendom);
        }

        [Authorize]
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


            //EXTRA CONTROL TO PREVENT OTHER USERS BEING ABLE TO EDIT/SEE OTHER'S LISTINGS.
            var userId = _userManager.GetUserId(User);

            _logger.LogInformation("userId = " + userId);
            _logger.LogInformation("Property Application user Id = " + eiendom.ApplicationUserId);

            if (userId == null || userId != eiendom.ApplicationUserId)
            {


                _logger.LogError("userId is not equal to 'eiendom.ApplicationUserId or is null'");

                return Forbid();
            }

            var checkEiendom = await _context.Eiendom.Include(l => l.Bookings).FirstOrDefaultAsync(l => l.EiendomID == id);


            //If the eiendom has a booking it should not possible to delete.
            if (checkEiendom.Bookings.Any())
            {
                return View("ListingHasBookingsError");
            }


            if (eiendom != null)
            {
                _context.Eiendom.Remove(eiendom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("index", "hosting");
        }



        private bool EiendomExists(int id)
        {
            return (_context.Eiendom?.Any(e => e.EiendomID == id)).GetValueOrDefault();
        }




        public IActionResult EiendomNotFound()
        {
            return View();
        }



    }

}
