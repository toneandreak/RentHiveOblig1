using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Data.Migrations;
using SQLitePCL;
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



        public async Task <IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userEiendoms = await _context.Eiendom.Where(e => e.ApplicationUserId == userId).ToListAsync();
            return View(userEiendoms);
        }
    }
}
