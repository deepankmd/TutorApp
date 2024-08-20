using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IMapper _mapper;
        //private readonly IRepository<Notification> _repository;
        //private readonly IRepository<Assignment> _repositoryAssignment;
        //private readonly IRepository<Tutors> _repositoryTutors;
        private readonly MySqlContext _context;

        public NotificationController(IMapper mapper,MySqlContext context)
        {
            _mapper = mapper;
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
                var notification = await _context.Notification.Where(_ => _.NotificationType == NotificationType.admin).ToListAsync();
                return View(notification);
            }
            else if (isTutor)
            { 
             var notification = await _context.Notification.Where(_ => _.NotificationType == NotificationType.Tutor).ToListAsync();
            return View(notification.Where(_ => _.NotificationType == NotificationType.Tutor));
            }
            else if (isParent)
            {
                var notification = await _context.Notification.Where(_ => _.NotificationType == NotificationType.Parent).ToListAsync();
                return View(notification.Where(_ => _.NotificationType == NotificationType.Parent));
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
                var notification = await _context.Notification.Where(_ => _.NotificationType == NotificationType.admin).ToListAsync();
                return View(notification);
            }
            else if (isTutor)
            {
                var notification = await _context.Notification.Where(_ => _.NotificationType == NotificationType.Tutor).ToListAsync();
                return View(notification.Where(_ => _.NotificationType == NotificationType.Tutor));
            }
            else if (isParent)
            {
                var notification = await _context.Notification.Where(_ => _.NotificationType == NotificationType.Parent).ToListAsync();
                return View(notification.Where(_ => _.NotificationType == NotificationType.Parent));
            }
            return PartialView();
        }

        public async Task<IActionResult> NotificationDetail(string id, string typeId, ScreenType screenType)
        {
            var notification = await _context.Notification.Where(_ => _.ID == Guid.Parse(id)).ToListAsync();
           
            var detail = _mapper.Map<NotificationDetailViewModel>(notification);

            switch (screenType)
            {
                case ScreenType.TutorRegister:
                    detail.Tutorṣ = await _context.Tutors.FirstOrDefaultAsync(_ => _.ID == Guid.Parse(typeId));
                    break;
                case ScreenType.Assignment:
                    detail.Assignment = await _context.Assignment.FirstOrDefaultAsync( _ => _.ID == Guid.Parse(typeId));
                    break;
            }
            return View(detail);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveNotification(string id)
        {
            var notification = await _context.Notification.FirstOrDefaultAsync(_ => _.ID == Guid.Parse(id));
            if (notification != null)
            {
                UpdateNotification(notification);

                if (notification.ScreenType == ScreenType.TutorRegister)
                {
                    var tutor = await _context.Tutors.FirstOrDefaultAsync(_ => _.ID == Guid.Parse(notification.TypeID));
                    if (tutor == null)
                    {
                        return NotFound("Tutor not found");
                    }
                    tutor.IsVerified = true;

                    _context.Tutors.Update(tutor);
                }
                else if (notification.ScreenType == ScreenType.Assignment)
                {
                    UpdateNotification(notification);
                    var assignments = await _context.Assignment.FirstOrDefaultAsync(_ => _.ID.ToString() == (notification.TypeID));
                    if (assignments == null)
                    {
                        return NotFound("Assignment not found");
                    }
                    assignments.IsVerified = true;

                    _context.Assignment.Update(assignments);
                }
            }
            return RedirectToAction("Index");
        }

        private void UpdateNotification(Notification notification)
        {
            notification.IsRead = true;
            _context.Notification.Update(notification);
        }
    }
}
