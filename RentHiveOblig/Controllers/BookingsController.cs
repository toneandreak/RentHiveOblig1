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



        [Authorize]
        public async Task<IActionResult> SendToBooking(DateTime startTime, DateTime endTime, int eiendomId, double totalPrice)
        {
            var Booking = new Booking()
            {
                GuestId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StartDate = startTime,
                EndDate = endTime,
                PropertyId = eiendomId,
                TotalPrice = totalPrice,
                BookingAccepted = false
            };

            var Eiendom = _context.Eiendom.Find(eiendomId);

            var viewModel = new BookingRequestViewModel
            {
                Booking = Booking,
                Eiendom = Eiendom
            };

            return View("BookingRequest", viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendBookingRequest(Booking model)
        {

            _logger.LogInformation($"UserId is {model.GuestId}");
            _logger.LogInformation($"Booking request: {model}");



            if (ModelState.IsValid) {
                
                
                try
                {

                    model.BookingAccepted = false; 
                    _context.Booking.Add(model);
                    await _context.SaveChangesAsync();
                  _logger.LogInformation($"Booking request confirmed, saved to DB");


                }catch(Exception ex)
                {
                    _logger.LogError(ex, "An error occured while creating the booking.");

                }

                _logger.LogInformation("Booking created"); 
            }
            return RedirectToAction("SuccessPage"); //Temporarily, need to change to redirect somewhere else.

        }

    }
}
