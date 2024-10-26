using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class ParentDetailsController : Controller
    {
        private readonly IMapper _mapper;
        //private readonly IRepository<ParentDetails> _repository;
        //private readonly IRepository<Notification> _repositoryNotification;
        //private readonly IRepository<Assignment> _repositoryAssignment;
        private readonly MySqlContext _context;

        public ParentDetailsController(IMapper mapper, MySqlContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session != null)
            {
                string userID = HttpContext.Session.GetString(UserConstants.UserID);
                ParentDetails parentDetails;
                if (HttpContext.Session.GetString(UserConstants.UserID) != null)
                {
                   parentDetails = await _context.ParentDetails.FindAsync(Guid.Parse(userID));
                }
                parentDetails = new ParentDetails();
                return View(parentDetails);
            }
            else
            {
                var parentDetails = await _context.ParentDetails.ToListAsync();
                return View(parentDetails);
            }
        }

        public IActionResult Create()
        {
            PopulateDropDowns();
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParentDetails parentDetails)
        {
            if (ModelState.IsValid)
            {
                await _context.ParentDetails.AddAsync(parentDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Login");
            }
            PopulateDropDowns();

            // Create a message for the email
            string emailMessage = $"Dear {parentDetails.Name},\n\n" +
                                  $"We have successfully created a student profile for your {parentDetails.RelationShip}, {parentDetails.StudentName}.\n\n" +
                                  "Thank you for using our service.\n\n Your Details will be verified soon." +
                                  "We will post you on admin approval\n\n This process will take maximum 1 hour " +
                                  "Best regards,\n" +
                                  "Your TuitionMasters";

            //string subject = "Student Registration Team"; 

            Notification notification = new Notification { 
                UserName = parentDetails.Name,
                Subject = "Student Created",
                Message = emailMessage,
                Email = parentDetails.Email,
                Mobile = parentDetails.Mobile.ToString(),
                NotificationType = NotificationType.admin,
                IsRead = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _context.Notification.AddAsync(notification);
            await _context.SaveChangesAsync();

            //await NotificationService.SendEmailAsync(parentDetails.Email, subject, emailMessage);
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = HttpContext.Session.GetString("UserID");
            }
            var parentDetails = await _context.ParentDetails.FindAsync(Guid.Parse(id));
            if (parentDetails == null)
            {
                return NotFound();
            }
            PopulateDropDowns();

            return PartialView(parentDetails);
        }
       
        //
        //public async Task<IActionResult> Edit(ParentDetails parentDetails)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _context.ParentDetails.ReplaceOneAsync(t => t._id == parentDetails._id, parentDetails);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    PopulateDropDowns();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var parent = await _context.ParentDetails.FindAsync(Guid.Parse(id));
            _context.ParentDetails.Remove(parent);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDowns()
        {
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Male", Text = "Male" },
                new SelectListItem { Value = "Female", Text = "Female" }
            }, "Value", "Text");

            ViewBag.Nationality = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Singaporean/PR", Text = "Singaporean/PR" },
                new SelectListItem { Value = "Foreigner", Text = "Foreigner" }
            }, "Value", "Text");

            ViewBag.RelationShip = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Son", Text = "Son" },
                new SelectListItem { Value = "Daughter", Text = "Daughter" },
                new SelectListItem { Value = "Others", Text = "Others" }
            }, "Value", "Text");
        }

        [Authorize(Policy = "RequireAnyRole")]
        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = HttpContext.Session.GetString("UserID");
            }
            PopulateDropDowns();
            var parentDetails = await _context.ParentDetails.FindAsync(Guid.Parse(id));
            if (parentDetails == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignment.FindAsync(parentDetails.ID);
            var parentDetailsViewModel = _mapper.Map<ParentDetailsViewModel>(parentDetails);
            var assignmentsViewModel = _mapper.Map<IEnumerable<AssignmentReadViewModel>>(assignments);
            parentDetailsViewModel.Assignment = assignmentsViewModel.ToList();

            return View(parentDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ParentDetails parentDetails)
        {
            if (id != parentDetails.ID.ToString())
            {
                return BadRequest();
            }
            PopulateDropDowns();
            var parentdetailsFromDatabase = await _context.ParentDetails.FindAsync(Guid.Parse(id));
            if (ModelState.IsValid)
            {
                parentdetailsFromDatabase.Name = parentDetails.Name;
                parentdetailsFromDatabase.Email = parentDetails.Email;
                parentdetailsFromDatabase.Mobile = parentDetails.Mobile;
                parentdetailsFromDatabase.StudentName = parentDetails.StudentName;
                parentdetailsFromDatabase.RelationShip = parentDetails.RelationShip;
                parentdetailsFromDatabase.Gender = parentDetails.Gender;
                parentdetailsFromDatabase.Nationality = parentDetails.Nationality;
                parentdetailsFromDatabase.Gender = parentDetails.Gender;
                parentdetailsFromDatabase.PostalCode = parentDetails.PostalCode;
                parentdetailsFromDatabase.Address = parentDetails.Address;

                _context.ParentDetails.Update(parentDetails);
                return RedirectToAction(nameof(Profile), new { id = parentDetails.ID.ToString() });
            }
            return View(parentDetails);
        }
    }
}