using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorCategoryController : Controller
    {
        //private readonly IRepository<TutorCategory> _repository;
        private readonly MySqlContext _context;

        public TutorCategoryController(MySqlContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorCategory = await _context.TutorCategory.ToListAsync();
            return View(tutorCategory);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorCategory tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorCategory.AddAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorCategory.FindAsync(Guid.Parse(id));
            _context.TutorCategory.Update(tutorGrade);
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public IActionResult Edit(TutorCategory tutorGrade)
        {
            if (ModelState.IsValid)
            {
                _context.TutorCategory.Update(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var tutorGrade = await _context.TutorCategory.FindAsync(Guid.Parse(id));
            _context.TutorCategory.Remove(tutorGrade);
            return RedirectToAction(nameof(Index));
        }
    }
}
