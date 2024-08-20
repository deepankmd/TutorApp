using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorGradeController : Controller
    {
        private readonly MySqlContext _context;

        public TutorGradeController(MySqlContext context)
        {
            //_repository = repository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGrade = await _context.TutorGrade.ToListAsync();
            return View(tutorGrade);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGrade tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGrade.AddAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorGrade.FindAsync(Guid.Parse(id));
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public IActionResult Edit(TutorGrade tutorGrade)
        {
            if (ModelState.IsValid)
            {
                _context.TutorGrade.Update(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var tutorGrade = await _context.TutorGrade.FindAsync(Guid.Parse(id));
            _context.TutorGrade.Update(tutorGrade);
            return RedirectToAction(nameof(Index));
        }
    }
}
