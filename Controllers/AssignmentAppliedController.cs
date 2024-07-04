using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class AssignmentAppliedController : Controller
    {
        private readonly MongoContext _context;
        private readonly IMapper _mapper;

        public AssignmentAppliedController(MongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var assignmentsApplied = await _context.AssignmentApplied.Find(_ => true).ToListAsync();
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
                var appliedAssignment = await _context.Assignment
                    .Find(t => t._id.ToString() == assignment.AssignmentID)
                    .FirstOrDefaultAsync();
                appliedAssignment.AssignmentStatus = AssignmentStatus.Applied;
                appliedAssignment.AppliedCount = appliedAssignment.AppliedCount +1;
                await _context.Assignment.ReplaceOneAsync(t => t._id == appliedAssignment._id, appliedAssignment);
                
                var assignmentApplied = _mapper.Map<AssignmentApplied>(assignment);
                await _context.AssignmentApplied.InsertOneAsync(assignmentApplied);

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
                    TypeID = appliedAssignment._id.ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ScreenType = ScreenType.AppliedAssignment
                };
                _context.Notification.InsertOne(notification);

                return RedirectToAction("Index", "Assignment");
            }
            return PartialView(assignment);
        }
    }
}
