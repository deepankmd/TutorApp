﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;
using MongoDB.Driver;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;

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

            if (userRecord?.FirstOrDefault() != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, UserConstants.ParentDetails)
            };
                var user = userRecord.FirstOrDefault();
                HttpContext.Session.SetString(UserConstants.UserRole, UserConstants.ParentDetails);
                HttpContext.Session.SetString(UserConstants.UserID, user._id.ToString());
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
                var tutor = _context.Tutors
                    .Find(a => (a.Email == loginViewModel.Email || a.MobileNumber == int.Parse(loginViewModel.Email)) && a.Password == loginViewModel.Password);

                if (tutor.FirstOrDefault() != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, UserConstants.TutorRole)
            };

                    var user = tutor.FirstOrDefault();
                    HttpContext.Session.SetString(UserConstants.UserRole, UserConstants.TutorRole);
                    HttpContext.Session.SetString(UserConstants.UserID, user._id.ToString());
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
    }
}