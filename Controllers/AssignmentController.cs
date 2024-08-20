using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly MySqlContext _context;
        private readonly IMapper _mapper;

        public AssignmentController(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignment.ToListAsync();
            var parent = await _context.ParentDetails.ToListAsync();
            var assignmentViewModel = _mapper.Map<IEnumerable<AssignmentReadViewModel>>(assignments);
            assignmentViewModel.Select(_ => _.Address = parent.FirstOrDefault(p => p.ID == _.ParentId).Address);
            return View(assignmentViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var subjects = await _context.TutorSubject.ToListAsync();
            var level = await _context.TutorLevel.ToListAsync();

            var assignmentViewModel = _mapper.Map<AssignmentViewModel>(new Assignment { DueDate = DateTime.Now });
            assignmentViewModel.TutorSubject = subjects;
            assignmentViewModel.TutorLevel = level;

            return View(assignmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssignmentViewModel assignmentViewModel)
        {
            var assignment = _mapper.Map<Assignment>(assignmentViewModel);
            assignment.ParentId = HttpContext.Session.GetString(UserConstants.UserID);
            assignment.AssignmentStatus = AssignmentStatus.Pending;

            var parent = await _context.ParentDetails.FirstOrDefaultAsync(_ => _.ID.ToString() == assignment.ParentId);

            string emailMessage = $"Dear {parent.Name},\n\n" +
                               $"We have successfully created a Assignment for your {parent.RelationShip}, {parent.StudentName}.\n\n" +
                               "Thank you for using our service.\n\n Your Details will be verified soon." +
                               "We will post you on admin approval\n\n\n" +
                               "Best regards,\n" +
                               "Your TuitionMasters";


            if (ModelState.IsValid)
            {
                await _context.Assignment.AddAsync(assignment);
                Notification notification = new Notification
                {
                    UserName = HttpContext.Session.GetString(UserConstants.UserName),
                    Subject = "Assignment Created",
                    Message = "",
                    Email = parent.Email,
                    Mobile = parent.Mobile.ToString(),
                    NotificationType = NotificationType.admin,
                    IsRead = false,
                    TypeID = assignment.ID.ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ScreenType = ScreenType.Assignment
                };
                await _context.Notification.AddAsync(notification);
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var assignment = await _context.Assignment.FirstOrDefaultAsync(t => t.ID.ToString() == id);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Assignment assignment)
        {
            if (id != assignment.ID.ToString())
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Assignment.Update(assignment);
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }

        public async Task<IActionResult> Details(string id)
        {
            var assignments = await _context.Assignment.Where(_ => _.ID.ToString()== id).ToListAsync();
            var parent = await _context.ParentDetails.ToListAsync();
            var assignmentViewModel = _mapper.Map<IEnumerable<AssignmentReadViewModel>>(assignments);
            assignmentViewModel.Select(_ => _.Address = parent.FirstOrDefault(p => p.ID == _.ParentId).Address);
            return View(assignmentViewModel);
        }

        public async Task<ActionResult> ViewApplicants(string id)
        {
            var appliedAssignments = await _context.AssignmentApplied.Where(_ => _.AssignmentID == id).ToListAsync();

            var assignmentAppliedViewModel = _mapper.Map<IEnumerable<AssignmentAppliedViewModel>>(appliedAssignments);
            return View(assignmentAppliedViewModel);
        }

        public async Task<ActionResult> Approve(AssignmentAppliedViewModel assignmentApplied)
        {
            var assignment = await _context.Assignment.FirstOrDefaultAsync(_ => _.ID.ToString() == assignmentApplied.AssignmentID);

            assignment.AssignmentStatus = AssignmentStatus.Closed;
            _context.Assignment.Update(assignment);
            return View(assignmentApplied);
        }



    }
}
