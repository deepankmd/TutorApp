﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorGradeValuesController : Controller
    {
        private readonly MongoContext _context;
        public TutorGradeValuesController(MongoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tutorGradeValues = await _context.TutorGradeValues.Find(_ => true).ToListAsync();
            return View(tutorGradeValues);
        }
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(TutorGradeValues tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGradeValues.InsertOneAsync(tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var tutorGrade = await _context.TutorGradeValues.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutorGrade == null)
            {
                return NotFound();
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorGradeValues tutorGrade)
        {
            if (ModelState.IsValid)
            {
                await _context.TutorGradeValues.ReplaceOneAsync(t => t._id == tutorGrade._id, tutorGrade);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(tutorGrade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _context.TutorGradeValues.DeleteOneAsync(t => t._id.ToString() == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
