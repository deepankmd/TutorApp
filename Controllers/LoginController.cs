using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace TutorAppAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly MongoContext _context;

        public LoginController(MongoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginViewModel loginViewModel)
        {
            var userRecord = _context.ParentDetails
                .Find(a => (a.Email == loginViewModel.Email || a.Mobile == int.Parse(loginViewModel.Email)) && a.Password == loginViewModel.Password);

            if (userRecord.FirstOrDefault() != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "ParentL")
            };

                HttpContext.Session.SetString("UserRole", "ParentDetails");
                HttpContext.Session.SetString("UserID", userRecord.FirstOrDefault()._id.ToString());

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Profile", "ParentDetails");
            }

            else if (userRecord.FirstOrDefault() == null)
            {
                var tutor = _context.Tutors
                    .Find(a => (a.Email == loginViewModel.Email || a.MobileNumber == loginViewModel.PhoneNumber) && a.Password == loginViewModel.Password);

                if (tutor.FirstOrDefault() != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "TutorL")
            };

                    HttpContext.Session.SetString("UserRole", "TutorL");
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(userRecord);
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
