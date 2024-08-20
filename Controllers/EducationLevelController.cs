using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class EducationLevelController : Controller
    {
        private readonly MySqlContext _context;
        public EducationLevelController(MySqlContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var educationLevels = await _context.EducationLevel.ToListAsync();
            return View(educationLevels);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(EducationLevel entity)
        {
            if (ModelState.IsValid)
            {
                await _context.EducationLevel.AddAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(entity);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.EducationLevel.FirstOrDefaultAsync(_ => _.ID.Equals(Guid.Parse(id)));
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public IActionResult Edit(EducationLevel tutorGrade)
        {
            if (ModelState.IsValid)
            {
                _context.EducationLevel.Update(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var level = await _context.EducationLevel.FindAsync(id);
            _context.EducationLevel.Remove(level);
            return RedirectToAction(nameof(Index));
        }
    }
}
