using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorGradesSubjectController : Controller
    {
        private readonly MongoContext _context;
        public TutorGradesSubjectController(MongoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGradesSubject = await _context.TutorGradesSubject.Find(_ => true).ToListAsync();
            return View(tutorGradesSubject);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGradesSubject tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGradesSubject.InsertOneAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorGradesSubject.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorGradesSubject tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGradesSubject.ReplaceOneAsync(t => t._id == tutorGrade._id, tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _context.TutorGradesSubject.DeleteOneAsync(t => t._id.ToString() == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
