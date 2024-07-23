using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class EducationLevelController : Controller
    {
        private readonly IRepository<EducationLevel> _repository;
        public EducationLevelController(IRepository<EducationLevel> repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var educationLevels = await _repository.GetAllAsync();
            return View(educationLevels);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(EducationLevel entity)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(entity);
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
        public async Task<IActionResult> Edit(EducationLevel tutorGrade)
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
