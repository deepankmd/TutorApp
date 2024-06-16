using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

public class TutorLevelController : Controller
{
    private readonly MongoContext _dbContext;

    public TutorLevelController(MongoContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: TutorLevel/Index
    public async Task<IActionResult> Index()
    {
        var tutorLevels = await _dbContext.TutorLevel.Find(new BsonDocument()).ToListAsync();
        return View(tutorLevels);
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
            await _dbContext.TutorLevel.InsertOneAsync(tutorLevel);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Create", tutorLevel);
    }

    // GET: TutorLevel/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var objectId = new ObjectId(id);
        var tutorLevel = await _dbContext.TutorLevel.Find(tl => tl._id == objectId).FirstOrDefaultAsync();
        if (tutorLevel == null)
        {
            return NotFound();
        }
        return PartialView("Edit", tutorLevel);
    }

    // POST: TutorLevel/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(string id, TutorLevel tutorLevel)
    {
        if (id != tutorLevel._id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var objectId = new ObjectId(id);
            await _dbContext.TutorLevel.ReplaceOneAsync(tl => tl._id == objectId, tutorLevel);
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Edit", tutorLevel);
    }

    // POST: TutorLevel/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var objectId = new ObjectId(id);
        await _dbContext.TutorLevel.DeleteOneAsync(tl => tl._id == objectId);
        return RedirectToAction(nameof(Index));
    }
}
