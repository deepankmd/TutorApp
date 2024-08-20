using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TutorAppAPI.Controllers
{
    public class LoginController : Controller
    {
        //private readonly IRepository<ParentDetails> _repository;
        //private readonly IRepository<Tutors> _repositoryTutors;
        private readonly MySqlContext _context;

        public LoginController(MySqlContext context)
        {
            //_repository = repository;
            //_repositoryTutors = repositoryTutors;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginViewModel loginViewModel)
        {
            var userRecord = await _context.ParentDetails.Where(a =>
            (a.Email == loginViewModel.Email || a.Mobile.ToString() == loginViewModel.Email) 
            && a.Password == loginViewModel.Password).ToListAsync();


            if (userRecord?.FirstOrDefault() != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, UserConstants.ParentDetails)
            };
                var user = userRecord.FirstOrDefault();
                HttpContext.Session.SetString(UserConstants.UserRole, UserConstants.ParentDetails);
                HttpContext.Session.SetString(UserConstants.UserID, user.ID.ToString());
                HttpContext.Session.SetString(UserConstants.UserName, user.Name.ToString());
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction(UserConstants.Profile, "ParentDetails");
            }

            else if (userRecord.FirstOrDefault() == null)
            {
                var alltutor = await _context.Tutors
                    .Where(a => (a.Email == loginViewModel.Email || 
                    a.MobileNumber.ToString() == loginViewModel.Email) &&
                    a.Password == loginViewModel.Password).ToListAsync();

                var tutor = alltutor;

                if (tutor.FirstOrDefault() != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, UserConstants.TutorRole)
                    };

                    var user = tutor.FirstOrDefault();
                    HttpContext.Session.SetString(UserConstants.UserRole, UserConstants.TutorRole);
                    HttpContext.Session.SetString(UserConstants.UserID, user.ID.ToString());
                    HttpContext.Session.SetString(UserConstants.UserName, user.Name.ToString());

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction(UserConstants.Profile, UserConstants.Tutors);
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            HttpContext.Session = null;
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        // POST: Account/SendResetLink
        [HttpPost]
        public IActionResult SendResetLink(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                string emailMessage = $"Dear {model.Email},\n\n" +
                                $"We received a request to reset your password for your Tuition Master account. To proceed with resetting your password, please click the link below:\r\n\n\n" +
                                "[Reset Password Link]\r\n" +
                                "If you did not request a password reset, please disregard this email or contact our support team immediately.\r\n" +
                                "Thank you for using Tuition Master.\r\n"+
                                "Best regards,\n" +
                                "Your Tuition Master\n"+
                                "Admin Team\n";

                NotificationService.SendEmailAsync(model.Email, "Tuition Master Password Reset Request", emailMessage);
                ViewBag.Message = "A reset link has been sent to your email.";
                return View("ResetPasswordConfirmation");
            }

            return View("ResetPassword", model);
        }

        // GET: Account/ResetPasswordConfirmation
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
