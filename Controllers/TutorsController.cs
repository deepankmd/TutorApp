using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TutorAppAPI.Models;
using TutorAppAPI.Services;


namespace TutorAppAPI.Controllers
{
    public class TutorsController : Controller
    {
        private readonly TutorService _tutorService;

        public TutorsController(TutorService tutorService)
        {
            _tutorService = tutorService;
        }

        public IActionResult Index()
        {
            var admins = _tutorService.Get();
            return View(admins);
        }

        public IActionResult Details(ObjectId id)
        {
            var admin = _tutorService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tutors tutor)
        {
            if (ModelState.IsValid)
            {
                _tutorService.Create(tutor);
                return RedirectToAction(nameof(Index));
            }
            return View(tutor);
        }

        public IActionResult Edit(ObjectId id)
        {
            var tutor = _tutorService.Get(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ObjectId id, Tutors tutor)
        {
            if (id != tutor._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _tutorService.Update(id, tutor);
                return RedirectToAction(nameof(Index));
            }
            return View(tutor);
        }

        public IActionResult Delete(ObjectId id)
        {
            var tutor = _tutorService.Get(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(ObjectId id)
        {
            _tutorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

