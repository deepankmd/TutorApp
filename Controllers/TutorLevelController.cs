using Microsoft.AspNetCore.Mvc;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

public class TutorLevelController : Controller
{
    private readonly MongoContext _dbContext;
    private readonly IRepository<TutorLevel> _tutorLevelRepository;

    public TutorLevelController(MongoContext dbContext, IRepository<TutorLevel> tutorLevelRepository)
    {
        _dbContext = dbContext;
        _tutorLevelRepository = tutorLevelRepository;
    }

    // GET: TutorLevel/Index
    public async Task<IActionResult> Index()
    {
        var levels = await _tutorLevelRepository.GetAllAsync();
        return View(levels);
    }

    // GET: TutorLevel/Create
    public IActionResult Create()
    {
        return PartialView("Create");
    }

    // POST: TutorLevel/Create
    [HttpPost]
    public async Task<IActionResult> Create(TutorLevel tutorLevel)
    {
        if (ModelState.IsValid)
        {
            tutorLevel.ID = Guid.NewGuid();
            await _tutorLevelRepository.AddAsync(tutorLevel);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Create", tutorLevel);
    }

    // GET: TutorLevel/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        var level = await _tutorLevelRepository.GetByIdAsync(Guid.Parse(id));
        if (level == null) return NotFound();

        return PartialView("Edit", level);
    }

    // POST: TutorLevel/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(TutorLevel tutorLevel)
    {
        if (ModelState.IsValid)
        {
            await _tutorLevelRepository.UpdateAsync(tutorLevel);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Edit", tutorLevel);
    }

    // POST: TutorLevel/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var level = await _tutorLevelRepository.GetByIdAsync(Guid.Parse(id));
        if (level == null) return NotFound();
        return View(level);
    }
}
