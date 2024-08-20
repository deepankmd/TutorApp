using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorGradeValuesController : Controller
    {
        //private readonly IRepository<TutorGradeValues> _repository;
        private readonly MySqlContext _context;

        public TutorGradeValuesController(MySqlContext repository)
        {
            _context = repository;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGradeValues = await _context.TutorGradeValues.ToListAsync();
            return View(tutorGradeValues);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGradeValues tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGradeValues.AddAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorGradeValues.FindAsync(Guid.Parse(id));
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public IActionResult Edit(TutorGradeValues tutorGrade)
        {
            if (ModelState.IsValid)
            {
                _context.TutorGradeValues.Update(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var tutorGrade = await _context.TutorGradeValues.FindAsync(Guid.Parse(id));

            _context.TutorGradeValues.Remove(tutorGrade);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
