using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Repository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorLocationsController : Controller
    {
        private readonly MongoContext _context;
        private readonly ITutorLocationRepository _tutorLocationRepository;

        public TutorLocationsController(MongoContext context, ITutorLocationRepository tutorLocationRepository)
        {
            _context = context;
            _tutorLocationRepository = tutorLocationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var locations = await _tutorLocationRepository.GetAllAsync();
            return View(locations);
        }

        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorLocations tutorLocation)
        {
            if (ModelState.IsValid)
            {
                tutorLocation.ID = Guid.NewGuid();
                await _tutorLocationRepository.AddAsync(tutorLocation);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorLocation);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var location = await _tutorLocationRepository.GetByIdAsync(Guid.Parse(id));
            if (location == null) return NotFound();

            return PartialView(location);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorLocations tutorLocation)
        {
            if (ModelState.IsValid)
            {
                await _tutorLocationRepository.UpdateAsync(tutorLocation);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorLocation);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _tutorLocationRepository.DeleteAsync(Guid.Parse(id));
            return RedirectToAction(nameof(Index));
        }
    }
}