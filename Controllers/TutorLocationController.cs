using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorLocationsController : Controller
    {
        private readonly MongoContext _context;

        public TutorLocationsController(MongoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tutorLocations = await _context.TutorLocations.Find(_ => true).ToListAsync();
            return View(tutorLocations);
        }

        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorLocations tutorLocation)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorLocations.InsertOneAsync(tutorLocation);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorLocation);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorLocation = await _context.TutorLocations.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutorLocation == null)
            {
                return NotFound();
            }
            return PartialView(tutorLocation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorLocations tutorLocation)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorLocations.ReplaceOneAsync(t => t._id == tutorLocation._id, tutorLocation);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorLocation);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _context.TutorLocations.DeleteOneAsync(t => t._id.ToString() == id);
            return RedirectToAction(nameof(Index));
        }
    }
}