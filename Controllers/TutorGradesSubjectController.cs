using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorGradesSubjectController : Controller
    {
        private readonly MySqlContext _context;
        //private readonly IRepository<TutorGradesSubject> _repository;

        public TutorGradesSubjectController(MySqlContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGradesSubject = await _context.TutorGradesSubject.ToListAsync();
            return View(tutorGradesSubject);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGradesSubject tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGradesSubject.AddAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorGradesSubject.FindAsync(Guid.Parse(id));
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public IActionResult Edit(TutorGradesSubject tutorGrade)
        {
            if (ModelState.IsValid)
            {
                _context.TutorGradesSubject.Update(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var tutorGrade = await _context.TutorGradesSubject.FindAsync(Guid.Parse(id));

            _context.TutorGradesSubject.Remove(tutorGrade);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
