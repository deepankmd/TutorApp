using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorGradeController : Controller
    {
        private readonly MongoContext _context;
        public TutorGradeController(MongoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGrade = await _context.TutorGrade.Find(_ => true).ToListAsync();
            return View(tutorGrade);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGrade tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGrade.InsertOneAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorGrade.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorGrade tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGrade.ReplaceOneAsync(t => t._id == tutorGrade._id, tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _context.TutorGrade.DeleteOneAsync(t => t._id.ToString() == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
