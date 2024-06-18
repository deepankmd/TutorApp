using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class ParentDetailsController : Controller
    {
        private readonly MongoContext _context;

        public ParentDetailsController(MongoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session != null)
            {
                string userID = HttpContext.Session.GetString(UserConstants.UserID);
                var parentDetails = await _context.ParentDetails.Find(_ => _._id.ToString() == userID).ToListAsync();
                return View(parentDetails);
            }
            else {
                var parentDetails = await _context.ParentDetails.Find(_ => true).ToListAsync();
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
                await _context.ParentDetails.InsertOneAsync(parentDetails);
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDowns();

            // Create a message for the email
            string emailMessage = $"Dear {parentDetails.Name},\n\n" +
                                  $"We have successfully created a student profile for your {parentDetails.RelationShip}, {parentDetails.StudentName}.\n\n" +
                                  "Thank you for using our service.\n\n Your Details will be verified soon." +
                                  "We will post you on admin approval\n\n This process will take maximum 1 hour " +
                                  "Best regards,\n" +
                                  "Your TuitionMasters";

            string subject = "Student Registration Team"; 

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
            _context.Notification.InsertOne(notification);

            //await NotificationService.SendEmailAsync(parentDetails.Email, subject, emailMessage);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = HttpContext.Session.GetString("UserID");
            }
            var parentDetails = await _context.ParentDetails.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (parentDetails == null)
            {
                return NotFound();
            }
            PopulateDropDowns();

            return PartialView(parentDetails);
        }
       
        [HttpPost]
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
            await _context.ParentDetails.DeleteOneAsync(t => t._id.ToString() == id);
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
        }
        [Authorize(Policy = "RequireAnyRole")]
        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = HttpContext.Session.GetString("UserID");
            }
            PopulateDropDowns();
            var parentDetails = await _context.ParentDetails.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (parentDetails == null)
            {
                return NotFound();
            }
            return View(parentDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ParentDetails parentDetails)
        {
            if (id != parentDetails._id.ToString())
            {
                return BadRequest();
            }
            PopulateDropDowns();
            var parentdetailsFromDatabase = await _context.ParentDetails.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
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


                await _context.ParentDetails.ReplaceOneAsync(t => t._id == parentDetails._id, parentDetails);
                return RedirectToAction(nameof(Profile), new { id = parentDetails._id.ToString() });
            }
            return View(parentDetails);
        }
    }
}