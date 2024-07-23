using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
        private readonly IRepository<Notification> _repository;
        private readonly IRepository<Assignment> _repositoryAssignment;
        private readonly IRepository<Tutors> _repositoryTutors;

        public NotificationController(IMapper mapper, IRepository<Notification> repository,
            IRepository<Assignment> repositoryAssignment, IRepository<Tutors> repositoryTutors)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryAssignment = repositoryAssignment;
            _repositoryTutors = repositoryTutors;
        }
        public async Task<IActionResult> Index()
        {
            var user = User;
            var isAdmin = user.IsInRole(UserConstants.AdminRole);
            var isTutor = user.IsInRole(UserConstants.TutorRole);
            var isParent = user.IsInRole(UserConstants.ParentDetails);

            if (isAdmin)
            {
                var notification = await _repository.GetAllAsync();
                return View(notification.Where(_ => _.NotificationType == NotificationType.admin));
            }
            else if (isTutor)
            {
                var notification = await _repository.GetAllAsync();
                return View(notification.Where(_ => _.NotificationType == NotificationType.Tutor));
            }
            else if (isParent)
            {
                var notification = await _repository.GetAllAsync();
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
                var notification = await _repository.GetAllAsync();                    
                return View(notification.Where(_ => _.NotificationType == NotificationType.admin));
            }
            else if (isTutor)
            {
                var notification = await _repository.GetAllAsync();
                return View(notification.Where(_ => _.NotificationType == NotificationType.Tutor));
            }
            else if (isParent)
            {
                var notification = await _repository.GetAllAsync();
                return View(notification.Where(_ => _.NotificationType == NotificationType.Parent));
            }
            return PartialView();
        }

        public async Task<IActionResult> NotificationDetail(string id, string typeId, ScreenType screenType)
        {
            var notification = await _repository.GetByIdAsync(Guid.Parse(id));
           
            var detail = _mapper.Map<NotificationDetailViewModel>(notification);

            switch (screenType)
            {
                case ScreenType.TutorRegister:
                    detail.Tutorṣ = await _repositoryTutors.GetByIdAsync(Guid.Parse(typeId));
                    break;
                case ScreenType.Assignment:
                    detail.Assignment = await _repositoryAssignment.GetByIdAsync(Guid.Parse(typeId));
                    break;
            }
            return View(detail);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveNotification(string id)
        {
            var notification = await _repository.GetByIdAsync(Guid.Parse(id));
            if (notification != null)
            {
                await UpdateNotification(notification);

                if (notification.ScreenType == ScreenType.TutorRegister)
                {
                    var tutor = await _repositoryTutors.GetByIdAsync(Guid.Parse(notification.TypeID));
                    if (tutor == null)
                    {
                        return NotFound("Tutor not found");
                    }
                    tutor.IsVerified = true;

                    await _repositoryTutors.UpdateAsync(    tutor);
                }
                else if (notification.ScreenType == ScreenType.Assignment)
                {
                    await UpdateNotification(notification);

                    var assignments = await _repositoryAssignment.GetByIdAsync(Guid.Parse(notification.TypeID));
                    if (assignments == null)
                    {
                        return NotFound("Assignment not found");
                    }
                    assignments.IsVerified = true;

                    await _repositoryAssignment.UpdateAsync(assignments);
                }
            }
            return RedirectToAction("Index");
        }

        private async Task UpdateNotification(Notification notification)
        {
            notification.IsRead = true;
            await _repository.UpdateAsync(notification);
        }
    }
}
