using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Repository;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorLocationsController : Controller
    {
        //private readonly ITutorLocationRepository _tutorLocationRepository;
        private readonly MySqlContext _context;

        public TutorLocationsController(MySqlContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var locations = await _context.TutorLocations.ToListAsync();
            return View(locations);
        }

        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorLocations tutorLocation)
        {
            if (ModelState.IsValid)
            {
                tutorLocation.ID = Guid.NewGuid();
                await _context.TutorLocations.AddAsync(tutorLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorLocation);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var location = await _context.TutorLocations.FindAsync(Guid.Parse(id));
            if (location == null) return NotFound();

            return PartialView(location);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorLocations tutorLocation)
        {
            if (ModelState.IsValid)
            {
              _context.TutorLocations.Update(tutorLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorLocation);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var location = await _context.TutorLocations.FindAsync(Guid.Parse(id));
            _context.TutorLocations.Remove(location);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}