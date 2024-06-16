using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Claims;
using TutorAppAPI.Models;
using TutorAppAPI.Services;


namespace TutorAppAPI.Controllers
{
    public class AdminsController : Controller
    {
        private readonly AdminService _adminService;
        private readonly MongoContext _context;

        public AdminsController(AdminService adminService, MongoContext context)
        {
            _adminService = adminService;
            _context = context;
        }

        public IActionResult Index()
        {
            var admins = _adminService.Get();
            return View(admins);
        }

        public IActionResult Details(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Admins admin)
        {
            if (ModelState.IsValid)
            {
                _adminService.Create(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public IActionResult Edit(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ObjectId id, Admins admin)
        {
            if (id != admin._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _adminService.Update(id, admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public IActionResult Delete(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(ObjectId id)
        {
            _adminService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admins admin)
        {
            var adminRecord = await _context.Admins
                .FindAsync(a => a.Email == admin.Email && a.Password == admin.Password);
                

            if (adminRecord.FirstOrDefault() != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Admin")
            };

                HttpContext.Session.SetString("UserRole", "Admin");
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Admin"); // Redirect to your admin home page.
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            HttpContext.Session = null;
            return RedirectToAction("Login", "Admin");
        }
    }
}
