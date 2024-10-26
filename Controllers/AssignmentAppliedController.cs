﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class AssignmentAppliedController : Controller
    {
        private readonly IMapper _mapper;
        //private readonly IRepository<AssignmentApplied> _repository;
        //private readonly IRepository<Notification> _repositoryNotification;
        //private readonly IRepository<Assignment> _repositoryAssignment;
        private readonly MySqlContext _context;

        public AssignmentAppliedController(IMapper mapper, MySqlContext context)
        {
            _mapper = mapper;
            _context = context;
            //_repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var assignmentsApplied = await _context.AssignmentApplied.ToListAsync();
            return View(assignmentsApplied);
        }

        public ActionResult Create(string id)
        {
            var assignemntApplied = new AssignmentAppliedViewModel();
            assignemntApplied.AssignmentID = id;
            assignemntApplied.TutorName = HttpContext.Session.GetString("UserName");
            assignemntApplied.UserID = HttpContext.Session.GetString("UserID");
            var assignmentReadViewModel = _mapper.Map<AssignmentAppliedViewModel>(assignemntApplied);
            return PartialView(assignmentReadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AssignmentAppliedViewModel assignment)
        {
            if (ModelState.IsValid)
            {
                var appliedAssignment = await _context.Assignment.FindAsync(assignment.AssignmentID);
                appliedAssignment.AssignmentStatus = AssignmentStatus.Applied;
                appliedAssignment.AppliedCount = appliedAssignment.AppliedCount +1;
                _context.Assignment.Update(appliedAssignment);
                
                var assignmentApplied = _mapper.Map<AssignmentApplied>(assignment);
                await _context.AssignmentApplied.AddAsync(assignmentApplied);

                string emailMessage = $"Dear {assignment.TutorName},\n\n" +
                               "We have successfully created a applied assignment for you\n\n" +
                               "Thank you for using our service.\n\n Your Details will be verified soon." +
                               "We will post you on approval by Parent\n\n\n" +
                               "Best regards,\n" +
                               "Your TuitionMasters";


                Notification notification = new Notification
                {
                    UserName = HttpContext.Session.GetString(UserConstants.UserName),
                    Subject = "Assignment Applied",
                    Message = "",
                    Email = assignment.TutorName,
                    //Mobile = assignment.Mobile.ToString(),
                    NotificationType = NotificationType.Parent,
                    IsRead = false,
                    TypeID = appliedAssignment.ID.ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ScreenType = ScreenType.AppliedAssignment
                };
                await _context.Notification.AddAsync(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Assignment");
            }
            return PartialView(assignment);
        }
    }
}
