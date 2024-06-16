using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class EducationLevelController : Controller
    {
        private readonly MongoContext _context;
        public EducationLevelController(MongoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var educationLevels = await _context.EducationLevel.Find(_ => true).ToListAsync();
            return View(educationLevels);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(EducationLevel tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.EducationLevel.InsertOneAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.EducationLevel.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EducationLevel tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.EducationLevel.ReplaceOneAsync(t => t._id == tutorGrade._id, tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _context.EducationLevel.DeleteOneAsync(t => t._id.ToString() == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
