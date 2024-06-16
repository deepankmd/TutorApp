using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

public class TutorSubjectController : Controller
{
    private readonly MongoContext _dbContext;

    public TutorSubjectController(MongoContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: TutorSubject/Index
    public async Task<IActionResult> Index()
    {
        var tutorSubjects = await _dbContext.TutorSubject.Find(new BsonDocument()).ToListAsync();
        return View(tutorSubjects);
    }

    // GET: TutorSubject/Create
    public IActionResult Create()
    {
        PopulateTutorLevels();
        return PartialView("Create");
    }

    // POST: TutorSubject/Create
    [HttpPost]
    public async Task<IActionResult> Create(TutorSubject tutorSubject)
    {
        if (ModelState.IsValid)
        {
            await _dbContext.TutorSubject.InsertOneAsync(tutorSubject);
            return RedirectToAction(nameof(Index));
        }

        PopulateTutorLevels();
        return PartialView("Create", tutorSubject);
    }

    // GET: TutorSubject/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var objectId = new ObjectId(id);
        var tutorSubject = await _dbContext.TutorSubject.Find(ts => ts._id == objectId).FirstOrDefaultAsync();
        if (tutorSubject == null)
        {
            return NotFound();
        }

        PopulateTutorLevels();
        return PartialView("Edit", tutorSubject);
    }

    // POST: TutorSubject/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(string id, TutorSubject tutorSubject)
    {
        if (id != tutorSubject._id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var objectId = new ObjectId(id);
            await _dbContext.TutorSubject.ReplaceOneAsync(ts => ts._id == objectId, tutorSubject);
            return RedirectToAction(nameof(Index));
        }

        PopulateTutorLevels();
        return PartialView("Edit", tutorSubject);
    }

    // POST: TutorSubject/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var objectId = new ObjectId(id);
        await _dbContext.TutorSubject.DeleteOneAsync(ts => ts._id == objectId);
        return RedirectToAction(nameof(Index));
    }

    private void PopulateTutorLevels()
    {
        var tutorLevels = _dbContext.TutorLevel.Find(new BsonDocument()).ToList();

        var selectedItemsTuterLevel = tutorLevels.Select(_ => new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Value = _._id.ToString(), Text = _.LevelName },
            }, "Value", "Text"));
        ViewBag.TutorLevels = selectedItemsTuterLevel;
    }
}
