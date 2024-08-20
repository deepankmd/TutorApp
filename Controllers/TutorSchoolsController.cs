using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
public class TutorSchoolsController : Controller
{
    //private readonly IRepository<TutorSchools> _repository;
    private readonly MySqlContext _context;

    public TutorSchoolsController(MySqlContext context)
    {
        _context = context;
    }

    // GET: TutorSchools/Index
    public async Task<IActionResult> Index()
    {
        var tutorSchools = await _context.TutorSchools.ToListAsync();
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
            await _context.TutorSchools.AddAsync(tutorSchool);
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

        var tutorSchool = await _context.TutorSchools.FindAsync(Guid.Parse(id));
        _context.TutorSchools.Update(tutorSchool);
        await _context.SaveChangesAsync();
        if (tutorSchool == null) return NotFound();

        return PartialView("Edit", tutorSchool);
    }

    // POST: TutorSchools/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(string id, TutorSchools tutorSchool)
    {
        if (ModelState.IsValid)
        {
            _context.TutorSchools.Update(tutorSchool);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Edit", tutorSchool);
    }

    // POST: TutorSchools/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var tutorSchool = await _context.TutorSchools.FindAsync(Guid.Parse(id));
        _context.TutorSchools.Remove(tutorSchool);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
