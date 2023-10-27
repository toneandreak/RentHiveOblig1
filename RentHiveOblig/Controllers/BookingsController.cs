using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;

namespace RentHiveOblig.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return _context.Booking != null ?
                        View(await _context.Booking.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Booking'  is null.");
        }



        public async Task<IActionResult> BookingRequest()
        {
            return View(); 
        }



    }
}
