using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        //private readonly IRepository<Admins> _repository;
        //private readonly IRepository<Notification> _repositoryNotification;
        private readonly MySqlContext _context;

        public AdminsController(IMapper mapper, MySqlContext context)
        {
            _mapper = mapper;
            //_repositoryNotification = repositoryNotification;
            //_repository = repository;
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var admins = await _context.Admins.ToListAsync();
            return View(admins);
        }

        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {
            var admin = await _context.Admins.FindAsync(id);
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
        public async Task<IActionResult> Create(Admins admin)
        {
            if (ModelState.IsValid)
            {
                await _context.Admins.AddAsync(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Admins admin)
        {
            //if (id != admin._id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _context.Entry(admin).State = EntityState.Modified;
                _context.Admins.Update(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var admins = await _context.Admins.FindAsync(id);

            var admin = _context.Admins.Remove(admins);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var admins = await _context.Admins.FindAsync(id);

            var admin = _context.Admins.Remove(admins);
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
            var adminRecord = await _context.Admins.Where(a => a.Email == admin.Email && a.Password == admin.Password).FirstOrDefaultAsync();

            if (adminRecord != null)
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

                var notification = await _context.Notification
                    .Where(_ => _.NotificationType == NotificationType.admin && !_.IsRead)
                    .ToListAsync();

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
            var allnotification = await _context.Notification.ToListAsync();

            if (isAdmin)
            {
                var notification = allnotification.Where(_ => _.NotificationType == NotificationType.admin);
                return View(notification);
            }
            else if (isTutor)
            {
                var notification = allnotification.Where(_ => _.NotificationType == NotificationType.Tutor);
                return PartialView(notification);
            }
            else if (isParent)
            {
                var notification = allnotification.Where(_ => _.NotificationType == NotificationType.Parent);
                return PartialView(notification);
            }
            return PartialView();
        }
    }
}
