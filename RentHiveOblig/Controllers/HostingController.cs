using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.DAL;
using RentHiveOblig.Models;
using RentHiveOblig.ViewModels;
using System.Security.Claims;

namespace RentHiveOblig.Controllers
{



    [Authorize]
    public class HostingController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HostingController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch all user's Eiendom objects
            var userEiendoms = await _context.Eiendom
                .Where(e => e.ApplicationUserId == userId)
                .ToListAsync();

            // Fetch the IDs of user's Eiendom objects for further queries
            var userEiendomIds = userEiendoms.Select(e => e.EiendomID).ToList();

            // Fetch all bookings related to the user's Eiendom IDs
            var allBookings = await _context.Booking
                .Where(b => userEiendomIds.Contains(b.EiendomId))
                .ToListAsync();

            // Filter the bookings into different lists based on their statuses
            var pendingBookings = allBookings.Where(b => b.BookingStatus == BookingStatus.Pending).ToList();
            var approvedBookings = allBookings.Where(b => b.BookingStatus == BookingStatus.Accepted).ToList();
            var declinedBookings = allBookings.Where(b => b.BookingStatus == BookingStatus.Declined).ToList();

            var viewModel = new HostingDashBViewModel
            {
                Eiendoms = userEiendoms,
                PendingBookings = pendingBookings,
                ApprovedBookings = approvedBookings,
            };

            return View(viewModel);
        }
    }
}
