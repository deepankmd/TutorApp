using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly MongoContext _context;
        public NotificationController(MongoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
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
                return View(notification);
            }
            else if (isParent)
            {
                var notification = await _context.Notification.Find(_ => _.NotificationType == NotificationType.Parent).ToListAsync();
                return View(notification);
            }
            return View();
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
