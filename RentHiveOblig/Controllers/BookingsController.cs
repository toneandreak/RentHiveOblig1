using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.DAL;
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


            var booking = new Booking()
            {
                GuestId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StartDate = startTime,
                EndDate = endTime,
                EiendomId = eiendomId,
                TotalPrice = totalPrice,
                BookingStatus = BookingStatus.Pending,
                QuantityDays = diffDays
            };

            var viewModel = new BookingRequestViewModel
            {
                Booking = booking,
                Eiendom = eiendom
            };

            return View("BookingRequest", viewModel);
        }




        //This will actually create the booking. 
        //I know there are redundancies, but I made it work. Might fix it later. 

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendBookingRequest(BookingRequestViewModel model)
        {
            //For some reason GuestId is not getting populated into the model, so I have to do it here too. 
            var guestId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            
            //A lot of loggers for debugging...
            _logger.LogInformation($"UserId is {guestId}");
            _logger.LogInformation($"PropertyId is {model.Booking.EiendomId}");
            _logger.LogInformation($"Checking model startDate: {model.Booking.StartDate}");
            _logger.LogInformation($"Checking model endDate: {model.Booking.EndDate}");
            _logger.LogInformation($"Checking model guestId: {model.Booking.GuestId}");
            _logger.LogInformation($"Checking model bookingstatus: {model.Booking.BookingStatus}");
            _logger.LogInformation($"Checking model QuantityDays: {model.Booking.QuantityDays}");
            _logger.LogInformation($"Checking model TotalPrice: {model.Booking.TotalPrice}");

            var eiendom = await _context.Eiendom.FindAsync(model.Booking.EiendomId);

            if (eiendom == null)
            {
                _logger.LogError($"Property with ID {model.Booking.EiendomId} not found.");
                ModelState.AddModelError("PropertyId", "Invalid Property ID");
                return View("EiendomNotFound"); 
            }


            var viewModel = new BookingRequestViewModel
            {
                Booking = model.Booking,
                Eiendom = eiendom,
            };

            //For some reason, the ApplicationUser is invalid in Modelstate, so I had to remove it from the modelState, however I am doing the control manually. 
            //https://www.appsloveworld.com/entity-framework/100/61/modelstate-errors-for-all-navigation-properties?expand_article=1
            ModelState.Remove("Booking.ApplicationUser");
            ModelState.Remove("Booking.Eiendom");
            ModelState.Remove("Eiendom");
            ModelState.Remove("GuestId"); 


            if (ModelState.IsValid)
            {
                // Validation for the dates: start and end date


                // Checking if start date is in the past
                if (viewModel.Booking.StartDate < DateTime.Now.Date)
                {
                    _logger.LogWarning("Start date is in the past.");
                    ModelState.AddModelError("StartDate", "Start date cannot be in the past.");
                    return View("BookingRequest", viewModel);
                }

                // Checking if end date is earlier or the same as start date
                if (viewModel.Booking.EndDate <= model.Booking.StartDate)
                {
                    _logger.LogWarning("End date must be after start date.");
                    ModelState.AddModelError("EndDate", "End date must be after start date.");
                    return View("BookingRequest", viewModel);
                }

                // If the property doesn't exist
                if (eiendom == null)
                {
                    _logger.LogError($"Property with ID {model.Booking.EiendomId} not found.");
                    ModelState.AddModelError("PropertyId", "Invalid Property ID");
                    return View("BookingRequest", viewModel);
                }

                try
                {
                    // Calculating the diffDays in server-side for extra security
                    int diffDays = (model.Booking.EndDate - model.Booking.StartDate).Days;
                    model.Booking.QuantityDays = diffDays;

                    // Getting pricePerNight
                    model.Booking.TotalPrice = diffDays * eiendom.PrisPerNatt;

                    var newBooking = new Booking
                    {
                        StartDate = model.Booking.StartDate,
                        EndDate = model.Booking.EndDate,
                        GuestId = guestId,
                        EiendomId = model.Booking.EiendomId,
                        BookingStatus = BookingStatus.Pending,
                        TotalPrice = model.Booking.TotalPrice,
                        QuantityDays = model.Booking.QuantityDays
                    };

                    _context.Booking.Add(newBooking);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Booking request confirmed, saved to DB");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the booking.");
                    return View("BookingRequest", viewModel); 
                }

                _logger.LogInformation("Booking created");
                return RedirectToAction("SuccessPage"); // Temporarily, need to change to redirect somewhere else.
            }
            else
            {
 }

                return View("BookingRequest", viewModel); // Something went wrong
            }
        }

    }

