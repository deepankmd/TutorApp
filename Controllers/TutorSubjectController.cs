using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

public class TutorSubjectController : Controller
{
    //private readonly MongoContext _dbContext;
    //private readonly IRepository<TutorSubject> _repository;
    //private readonly IRepository<TutorLevel> _tutorLevelRepository;
    private readonly MySqlContext _context;

    public TutorSubjectController(MySqlContext context)
    {
        _context = context;
    }

    // GET: TutorSubject/Index
    public async Task<IActionResult> Index()
    {
        var entity = await _context.TutorSubject.ToListAsync();
        return View(entity);
    }

    // GET: TutorSubject/Create
    public async Task<IActionResult> Create()
    {
        await PopulateTutorLevels();
        return PartialView("Create");
    }

    // POST: TutorSubject/Create
    [HttpPost]
    public async Task<IActionResult> Create(TutorSubject tutorSubject)
    {
        if (ModelState.IsValid)
        {
            tutorSubject.ID = Guid.NewGuid();
            await _context.TutorSubject.AddAsync(tutorSubject);
            return RedirectToAction(nameof(Index));
        }

        await PopulateTutorLevels();
        return PartialView("Create", tutorSubject);
    }

    // GET: TutorSubject/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tutorSubject = await _context.TutorSubject.FindAsync(Guid.Parse(id));
        if (tutorSubject == null) return NotFound();

        await PopulateTutorLevels();
        return PartialView("Edit", tutorSubject);
    }

    // POST: TutorSubject/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(string id, TutorSubject tutorSubject)
    {
        if (id != tutorSubject.ID.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            tutorSubject.ID = Guid.Parse(id);
            _context.TutorSubject.Update(tutorSubject);
            return RedirectToAction(nameof(Index));
        }

        await PopulateTutorLevels();
        return PartialView("Edit", tutorSubject);
    }

    // POST: TutorSubject/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var level = await _context.TutorSubject.FindAsync(Guid.Parse(id));
        if (level == null) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateTutorLevels()
    {
        var tutorLevels = await _context.TutorLevel.ToListAsync();

        var selectedItemsTuterLevel = tutorLevels.Select(_ => new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Value = _.ID.ToString(), Text = _.LevelName },
            }, "Value", "Text"));
        ViewBag.TutorLevels = selectedItemsTuterLevel;
    }
}
