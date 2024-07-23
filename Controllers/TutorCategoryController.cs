using Microsoft.AspNetCore.Mvc;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorCategoryController : Controller
    {
        private readonly MongoContext _context;
        private readonly IRepository<TutorCategory> _repository;

        public TutorCategoryController(MongoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorCategory = await _repository.GetAllAsync();
            return View(tutorCategory);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorCategory tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _repository.GetByIdAsync(Guid.Parse(id));
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
                await _repository.UpdateAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(Guid.Parse(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
