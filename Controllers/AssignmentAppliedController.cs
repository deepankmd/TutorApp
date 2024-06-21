using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class AssignmentAppliedController : Controller
    {
        private readonly MongoContext _context;
        private readonly IMapper _mapper;

        public AssignmentAppliedController(MongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var assignmentsApplied = await _context.AssignmentApplied.Find(_ => true).ToListAsync();
            return View(assignmentsApplied);
        }

        public ActionResult Create()
        {
            return PartialView(new AssignmentApplied());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AssignmentApplied assignment)
        {
            if (ModelState.IsValid)
            {
                await _context.AssignmentApplied.InsertOneAsync(assignment);
                return RedirectToAction("Index");
            }
            return PartialView(assignment);
        }

    }
}
