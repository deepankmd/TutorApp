using Microsoft.AspNetCore.Mvc;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;

namespace TutorAppAPI.Controllers
{
    public class TutorGradeValuesController : Controller
    {
        private readonly IRepository<TutorGradeValues> _repository;

        public TutorGradeValuesController(IRepository<TutorGradeValues> repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGradeValues = await _repository.GetAllAsync();
            return View(tutorGradeValues);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGradeValues tutorGrade)
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
        public async Task<IActionResult> Edit(TutorGradeValues tutorGrade)
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
