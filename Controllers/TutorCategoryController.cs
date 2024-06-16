using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorCategoryController : Controller
    {
        private readonly MongoContext _context;
        public TutorCategoryController(MongoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorCategory = await _context.TutorCategory.Find(_ => true).ToListAsync();
            return View(tutorCategory);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorCategory tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorCategory.InsertOneAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorCategory.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorCategory tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorCategory.ReplaceOneAsync(t => t._id == tutorGrade._id, tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _context.TutorCategory.DeleteOneAsync(t => t._id.ToString() == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
