using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly MongoContext _context;

        public AssignmentController(MongoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignment.Find(_ => true).ToListAsync();
            return View(assignments);
        }

        public IActionResult Create()
        {
            return View(new Assignment { DueDate = DateTime.Now});
        }

        [HttpPost]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                await _context.Assignment.InsertOneAsync(assignment);
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var assignment = await _context.Assignment.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Assignment assignment)
        {
            if (id != assignment._id.ToString())
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _context.Assignment.ReplaceOneAsync(t => t._id == assignment._id, assignment);
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }
    }
}
