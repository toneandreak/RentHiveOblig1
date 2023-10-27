using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentHiveOblig.Data;
using RentHiveOblig.Models;

namespace RentHiveOblig.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> usrMgr, IPasswordHasher<ApplicationUser> passwordHasher, ApplicationDbContext context)
        {
            userManager = usrMgr;
            this.passwordHasher = passwordHasher;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public IActionResult IndexProperties()
        {
            return View(_context.Eiendom);
        }


        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    UserName = user.Email,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(applicationUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password, string firstname, string lastname)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);

                if (!string.IsNullOrEmpty(firstname))
                    user.Firstname = firstname;
                else
                    ModelState.AddModelError("", "Firstname cannot be empty");

                if (!string.IsNullOrEmpty(lastname))
                    user.Lastname = lastname;
                else
                    ModelState.AddModelError("", "Lastname cannot be empty");

                if (!string.IsNullOrEmpty(email) &&
                    !string.IsNullOrEmpty(firstname) &&
                    !string.IsNullOrEmpty(lastname)
                    )
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }


        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}