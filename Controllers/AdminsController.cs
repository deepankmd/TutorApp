using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Security.Claims;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;


namespace TutorAppAPI.Controllers
{
    public class AdminsController : Controller
    {
        private readonly MongoContext _context;
        private readonly IMapper _mapper;
        private readonly AdminService _adminService;
        public AdminsController(AdminService adminService, MongoContext context, IMapper mapper)
        {
            _adminService = adminService;
            _context = context;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult Index()
        {
            var admins = _adminService.Get();
            return View(admins);
        }

        [Authorize]
        public IActionResult Details(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Edit(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [Authorize]
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

        [Authorize]
        public IActionResult Delete(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [Authorize]
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
                new Claim(ClaimTypes.Role, UserConstants.AdminRole)
            };

                HttpContext.Session.SetString(UserConstants.UserRole, UserConstants.AdminRole);
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                var notification = await _context.Notification.Find(_ => _.NotificationType == NotificationType.admin && !_.IsRead).ToListAsync();
                var notificationViewModel = _mapper.Map<List<NotificationViewModel>>(notification);
                HttpContext.Session.SetString("Notification", JsonConvert.SerializeObject(notificationViewModel));

                return RedirectToAction("Index", "Admins"); // Redirect to your admin home page.
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

        public async Task<IActionResult> NotifyPanel()
        {
            var user = User;
            var isAdmin = user.IsInRole(UserConstants.AdminRole);
            var isTutor = user.IsInRole(UserConstants.TutorRole);
            var isParent = user.IsInRole(UserConstants.ParentDetails);

            if (isAdmin)
            {
                var notification = await _context.Notification.Find(_ => _.NotificationType == NotificationType.admin).ToListAsync();
                return View(notification);
            }
            else if (isTutor)
            {
                var notification = await _context.Notification.Find(_ => _.NotificationType == NotificationType.Tutor).ToListAsync();
                return PartialView(notification);
            }
            else if (isParent)
            {
                var notification = await _context.Notification.Find(_ => _.NotificationType == NotificationType.Parent).ToListAsync();
                return PartialView(notification);
            }
            return PartialView();
        }
    }
}
