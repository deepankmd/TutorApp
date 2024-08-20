using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Repository.IRepository;
using TutorAppAPI.Services;

public class TutorLevelController : Controller
{
    private readonly MySqlContext _context;
    //private readonly IRepository<TutorLevel> _tutorLevelRepository;

    public TutorLevelController(MySqlContext dbContext)
    {
        _context = dbContext;
    }

    // GET: TutorLevel/Index
    public async Task<IActionResult> Index()
    {
        var levels = await _context.TutorLevel.ToListAsync();
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
            await _context.TutorLevel.AddAsync(tutorLevel);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Create", tutorLevel);
    }

    // GET: TutorLevel/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        var level = await _context.TutorLevel.FindAsync(Guid.Parse(id));
        if (level == null) return NotFound();

        return PartialView("Edit", level);
    }

    // POST: TutorLevel/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(TutorLevel tutorLevel)
    {
        if (ModelState.IsValid)
        {
            _context.TutorLevel.Update(tutorLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Edit", tutorLevel);
    }

    // POST: TutorLevel/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var level = await _context.TutorLevel.FindAsync(Guid.Parse(id));
        _context.TutorLevel.Remove(level);
        await _context.SaveChangesAsync();
        if (level == null) return NotFound();
        return View(level);
    }
}
