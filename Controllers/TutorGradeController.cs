using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorGradeController : Controller
    {
        private readonly MongoContext _context;
        private readonly IRepository<TutorGrade> _repository;

        public TutorGradeController(MongoContext context, IRepository<TutorGrade> repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGrade = await _repository.GetAllAsync();
            return View(tutorGrade);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGrade tutorGrade)
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
        public async Task<IActionResult> Edit(TutorGrade tutorGrade)
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
