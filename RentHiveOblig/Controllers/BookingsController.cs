using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;

namespace RentHiveOblig.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookingsController> _logger;
        private readonly ApplicationUser _user;

        public BookingsController(ApplicationDbContext context, ApplicationUser applicationUser, ILogger<BookingsController> logger)
        {
            _context = context;
            _user = applicationUser; 
            _logger = logger;
        }



        [Authorize]
        public async Task<IActionResult> SendToBooking(DateTime startTime, DateTime endTime, int eiendomId, double totalPrice)
        {

            var Booking = new Booking()
            {
                GuestId = _user.Id,
                StartDate = startTime,
                EndDate = endTime,
                PropertyId = eiendomId,
                TotalPrice = totalPrice,
                BookingAccepted = false

            };

            return View("BookingRequest", Booking); 

        }

        [Authorize]
        public async Task<IActionResult> BookingRequest(DateTime startTime, DateTime endTime, int eiendomId, double totalPrice, string guestId)
        {

            _logger.LogInformation($"GuestID {guestId} is requesting a booking.");
            _logger.LogInformation($"UserId is {_user.Id}");

            if(guestId != _user.Id || guestId == null)
            {
                _logger.LogError($"GuestID is the same like user_id or is null.");
                return Forbid(); 
            }



            return View();
        }



    }
}
