using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

public class TutorSchoolsController : Controller
{
    private readonly MongoContext _dbContext;
    private readonly IRepository<TutorSchools> _repository;

    public TutorSchoolsController(MongoContext dbContext, IRepository<TutorSchools> repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }

    // GET: TutorSchools/Index
    public async Task<IActionResult> Index()
    {
        var tutorSchools = await _repository.GetAllAsync();
        return View(tutorSchools);
    }

    // GET: TutorSchools/Create
    public IActionResult Create()
    {
        return PartialView("Create");
    }

    // POST: TutorSchools/Create
    [HttpPost]
    public async Task<IActionResult> Create(TutorSchools tutorSchool)
    {
        if (ModelState.IsValid)
        {
            tutorSchool.ID = Guid.NewGuid();
            await _repository.AddAsync(tutorSchool);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Create", tutorSchool);
    }

    // GET: TutorSchools/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tutorSchool = await _repository.GetByIdAsync(Guid.Parse(id));
        if (tutorSchool == null) return NotFound();

        return PartialView("Edit", tutorSchool);
    }

    // POST: TutorSchools/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(string id, TutorSchools tutorSchool)
    {
        if (ModelState.IsValid)
        {
            await _repository.UpdateAsync(tutorSchool);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Edit", tutorSchool);
    }

    // POST: TutorSchools/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var level = await _repository.GetByIdAsync(Guid.Parse(id));
        if (level == null) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}
