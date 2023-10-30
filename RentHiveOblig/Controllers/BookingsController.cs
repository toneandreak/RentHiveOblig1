using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;
using RentHiveOblig.ViewModels;
using System.Security.Claims;

namespace RentHiveOblig.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookingsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<BookingsController> logger)
        {
            _context = context;
            _userManager = userManager; 
            _logger = logger;
        }




        //This will send the form from listingDetails.cshtml to the form at BookingRequest.cshtml

        [Authorize]
        public async Task<IActionResult> SendToBooking(DateTime startTime, DateTime endTime, int eiendomId, double totalPrice, int diffDays)
        {

            _logger.LogInformation($"Received booking request with EiendomId: {eiendomId}");



            /////////Validation for the dates, starty- ande nddate

            //checking if startdate is in the past, does not make sense
            if (startTime < DateTime.Now.Date)
            {
                _logger.LogWarning("Check the input for the dates. Start date is in the past.");
                TempData["ErrorMessage"] = "Check the input for the dates. Start date cannot be in the past.";
                return RedirectToAction("ListingDetails", "Eiendoms", new { id = eiendomId });
            }

            // Checking if end date is earlier or the same as start date
            if (endTime <= startTime)
            {
                _logger.LogWarning("Check the input for the dates. End date must be after start date.");
                TempData["ErrorMessage"] = "Check the input for the dates. End date must be after start date.";
                return RedirectToAction("ListingDetails", "Eiendoms", new { id = eiendomId });
            }


            var eiendom = await _context.Eiendom.FindAsync(eiendomId);

            if (eiendom == null)
            {
                _logger.LogWarning($"Eiendom with id {eiendomId} not found.");
                return View("EiendomNotFound"); 
            }


            var Booking = new Booking()
            {
                GuestId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StartDate = startTime,
                EndDate = endTime,
                PropertyId = eiendomId,
                TotalPrice = totalPrice,
                BookingAccepted = false,
                QuantityDays = diffDays
            };

            var viewModel = new BookingRequestViewModel
            {
                Booking = Booking,
                Eiendom = eiendom
            };

            return View("BookingRequest", viewModel);
        }




        //This will actually create the booking. 

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendBookingRequest(Booking model)
        {
            _logger.LogInformation($"UserId is {model.GuestId}");
            _logger.LogInformation($"Booking request: {model}");


            if (ModelState.IsValid)
            {

                //Validation for the dates, starty- ande nddate


                //checking if startdate is in the past, does not make sense
                if (model.StartDate < DateTime.Now.Date)
                {
                    _logger.LogWarning("Start date is in the past.");
                    ModelState.AddModelError("StartDate", "Start date cannot be in the past.");
                    return View(model);
                }

                if (model.EndDate <= model.StartDate)
                {
                    _logger.LogWarning("End date must be after start date.");
                    ModelState.AddModelError("EndDate", "End date must be after start date.");
                    return View(model);
                }


                try
                {


                    //Calculating the diffDays in serverside for extra security
                    int diffDays = (model.EndDate - model.StartDate).Days;
                    model.QuantityDays = diffDays;

 
                    //Getting pricePerNIght
                    var eiendom = await _context.Eiendom.FindAsync(model.PropertyId);
                    model.TotalPrice = diffDays * eiendom.PrisPerNatt;



                    var newBooking = new Booking
                    {
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        GuestId = model.GuestId,
                        PropertyId = model.PropertyId,
                        BookingAccepted = false, 
                        TotalPrice = model.TotalPrice,
                        QuantityDays = model.QuantityDays
                    };

                    _context.Booking.Add(newBooking);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Booking request confirmed, saved to DB");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the booking.");
                }

                _logger.LogInformation("Booking created");
                return RedirectToAction("SuccessPage"); // Temporarily, need to change to redirect somewhere else.
            }


            return View(model); // Something went wrong
        }











    }
}
