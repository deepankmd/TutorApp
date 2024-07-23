using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.ViewModel;


namespace TutorAppAPI.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Admins> _repository;
        private readonly IRepository<Notification> _repositoryNotification;


        public AdminsController(IMapper mapper, IRepository<Notification> repositoryNotification, IRepository<Admins> repository)
        {
            _mapper = mapper;
            _repositoryNotification = repositoryNotification;
            _repository = repository;
        }
        [Authorize]
        public IActionResult Index()
        {
            var admins = _repository.GetAllAsync();
            return View(admins);
        }

        [Authorize]
        public IActionResult Details(Guid id)
        {
            var admin = _repository.GetByIdAsync(id);
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
                _repository.AddAsync(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var admin = _repository.GetByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Admins admin)
        {
            //if (id != admin._id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _repository.UpdateAsync(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var admin = _repository.DeleteAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _repository.DeleteAsync(id);
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
            var alladminRecord = await _repository.GetAllAsync();
            var adminRecord = alladminRecord.Where(a => a.Email == admin.Email && a.Password == admin.Password);


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

                var allnotification = await _repositoryNotification.GetAllAsync();

                var notification = allnotification.Where(_ => _.NotificationType == NotificationType.admin && !_.IsRead);
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
            var allnotification = await _repositoryNotification.GetAllAsync();

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
