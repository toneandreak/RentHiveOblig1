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



        /// <summary>
        /// GET: fetching available listings, pending reservations, approved reservations. 
        /// Doing this in three steps,, first getting a list of the users properties (eiendoms)
        /// second, getting related bookings to the eiendom Id's
        /// third, filtering based on pending and approved. This is done by adding to the lists in a  loop, filtering is based on BookingStatus
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Getting all the properties (Eiendom) that belong to this user (userId which we found)
            var userProperties = await _context.Eiendom
                .Where(e => e.ApplicationUserId == userId)
                .ToListAsync();

            // Make list of propertyIds
            var userPropertyIds = userProperties.Select(e => e.EiendomID).ToList();

            //Get all the bookings related to the propertyIds found earlier.
            //We do this by checking if the booking table (DB) contains the userPropertyIds
            var allBookings = await _context.Booking
                .Where(b => userPropertyIds.Contains(b.EiendomId))
                .ToListAsync();

            //Make two new lists to seperate pending and approved bookings.
            var pendingBookings = new List<Booking>();
            var approvedBookings = new List<Booking>();
            var earlierBookings = new List<Booking>();

            //Run a foreach to add the booking based on the status of the booking. 
            foreach (var booking in allBookings)
            {
                if (booking.BookingStatus == BookingStatus.Pending)
                {
                    pendingBookings.Add(booking);
                }
                else if (booking.BookingStatus == BookingStatus.Accepted && booking.EndDate > DateTime.Now)
                {
                    approvedBookings.Add(booking);
                }
                else if (booking.BookingStatus == BookingStatus.Accepted && booking.EndDate < DateTime.Now)
                {
                    earlierBookings.Add(booking);
                }
            }

        
            var viewModel = new HostingDashBViewModel
            {
                Eiendoms = userProperties,
                PendingBookings = pendingBookings,
                ApprovedBookings = approvedBookings,
                EarlierBookings = earlierBookings
            };


            return View(viewModel);
        }




    }
}
