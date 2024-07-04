using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly MongoContext _context;
        private readonly IMapper _mapper;
        public NotificationController(MongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<IActionResult> NotificationDetail(string id, string typeId, ScreenType screenType)
        {
            var notification = await _context.Notification.Find(_ => _._id.ToString() == id).FirstOrDefaultAsync();
           
            var detail = _mapper.Map<NotificationDetailViewModel>(notification);

            switch (screenType)
            {
                case ScreenType.TutorRegister:
                    detail.Tutorṣ = await _context.Tutors.Find(_ => _._id.ToString() == typeId).FirstOrDefaultAsync();
                    break;
                case ScreenType.Assignment:
                    detail.Assignment = await _context.Assignment.Find(_ => _._id.ToString() == typeId).FirstOrDefaultAsync();
                    break;
            }
            return View(detail);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveNotification(string id)
        {
            var notification = await _context.Notification.Find(_ => _._id.ToString() == id).FirstOrDefaultAsync();
            if (notification != null)
            {
                await UpdateNotification(notification);

                if (notification.ScreenType == ScreenType.TutorRegister)
                {
                    var tutor = _context.Tutors.Find(_ => _._id.ToString() == notification.TypeID).FirstOrDefault();
                    if (tutor == null)
                    {
                        return NotFound("Tutor not found");
                    }
                    tutor.IsVerified = true;

                    await _context.Tutors.ReplaceOneAsync(_ => _._id.ToString() == notification.TypeID, tutor);
                }
                else if (notification.ScreenType == ScreenType.Assignment)
                {
                    await UpdateNotification(notification);

                    var assignments = _context.Assignment.Find(_ => _._id.ToString() == notification.TypeID).FirstOrDefault();
                    if (assignments == null)
                    {
                        return NotFound("Assignment not found");
                    }
                    assignments.IsVerified = true;

                    await _context.Assignment.ReplaceOneAsync(_ => _._id.ToString() == notification.TypeID, assignments);
                }
            }
            return RedirectToAction("Index");
        }

        private async Task UpdateNotification(Notification notification)
        {
            notification.IsRead = true;
            await _context.Notification.ReplaceOneAsync(_ => _._id == notification._id, notification);
        }
    }
}
