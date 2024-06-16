using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

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
            
            var parentDetails = await _context.ParentDetails.Find(_ => true).ToListAsync();
            return View(parentDetails);
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
        public async Task<IActionResult> Edit(ParentDetails parentDetails)
        {
            if (ModelState.IsValid)
            {
                await _context.ParentDetails.ReplaceOneAsync(t => t._id == parentDetails._id, parentDetails);
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDowns();
            return RedirectToAction(nameof(Index));
        }

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
            if (ModelState.IsValid)
            {
                await _context.ParentDetails.ReplaceOneAsync(t => t._id == parentDetails._id, parentDetails);
                return RedirectToAction(nameof(Profile), new { id = parentDetails._id.ToString() });
            }
            return View(parentDetails);
        }
    }
}