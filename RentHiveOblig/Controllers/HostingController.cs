using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentHiveOblig.Controllers
{


    [Authorize]
    public class HostingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
