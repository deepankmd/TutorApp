using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TutorAppAPI.Models;
using TutorAppAPI.Services;


namespace TutorAppAPI.Controllers
{
    public class AdminsController : Controller
    {
        private readonly AdminService _adminService;

        public AdminsController(AdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            var admins = _adminService.Get();
            return View(admins);
        }

        public IActionResult Details(ObjectId id)
        {
            var admin = _adminService.Get(id);
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
        public IActionResult Create(Admins admin)
        {
            if (ModelState.IsValid)
            {
                _adminService.Create(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public IActionResult Edit(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ObjectId id, Admins admin)
        {
            if (id != admin._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _adminService.Update(id, admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public IActionResult Delete(ObjectId id)
        {
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(ObjectId id)
        {
            _adminService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
