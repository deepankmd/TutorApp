using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

public class TutorSchoolsController : Controller
{
    private readonly MongoContext _dbContext;

    public TutorSchoolsController(MongoContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: TutorSchools/Index
    public async Task<IActionResult> Index()
    {
        var tutorSchools = await _dbContext.TutorSchools.Find(new BsonDocument()).ToListAsync();
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
            await _dbContext.TutorSchools.InsertOneAsync(tutorSchool);
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

        var objectId = new ObjectId(id);
        var tutorSchool = await _dbContext.TutorSchools.Find(ts => ts._id == objectId).FirstOrDefaultAsync();
        if (tutorSchool == null)
        {
            return NotFound();
        }
        return PartialView("Edit", tutorSchool);
    }

    // POST: TutorSchools/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(string id, TutorSchools tutorSchool)
    {
        if (id != tutorSchool._id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var objectId = new ObjectId(id);
            await _dbContext.TutorSchools.ReplaceOneAsync(ts => ts._id == objectId, tutorSchool);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Edit", tutorSchool);
    }

    // POST: TutorSchools/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var objectId = new ObjectId(id);
        await _dbContext.TutorSchools.DeleteOneAsync(ts => ts._id == objectId);
        return RedirectToAction(nameof(Index));
    }
}
