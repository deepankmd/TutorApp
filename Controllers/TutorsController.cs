﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using TutorAppAPI.Helpers;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI.Controllers
{
    public class TutorsController : Controller
    {
        private readonly MongoContext _context;

        public TutorsController(MongoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session != null)
            {
                string userID = HttpContext.Session.GetString(UserConstants.UserID);
                var tutors = await _context.Tutors.Find(_ => true).ToListAsync();
                return View(tutors);
            }
            else
            {
                var tutors = await _context.Tutors.Find(_ => true).ToListAsync();
                return View(tutors);
            }
        }

        public IActionResult Create()
        {
            PopulateDropDowns();
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tutors tutors)
        {
            if (ModelState.IsValid)
            {
                await _context.Tutors.InsertOneAsync(tutors);
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDowns();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = HttpContext.Session.GetString("UserID");
            }
            var tutors = await _context.Tutors.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutors == null)
            {
                return NotFound();
            }
            PopulateDropDowns();

            return PartialView(tutors);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(Tutors tutors)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _context.Tutors.ReplaceOneAsync(t => t._id == tutors._id, tutors);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    PopulateDropDowns();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _context.Tutors.DeleteOneAsync(t => t._id.ToString() == id);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDowns()
        {
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Male", Text = "Male" },
                new SelectListItem { Value = "Female", Text = "Female" }
            }, "Value", "Text");

            ViewBag.Nationality = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Singaporean/PR", Text = "Singaporean/PR" },
                new SelectListItem { Value = "Foreigner", Text = "Foreigner" }
            }, "Value", "Text");
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = HttpContext.Session.GetString("UserID");
            }
            PopulateDropDowns();
            var tutors = await _context.Tutors.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (tutors == null)
            {
                return NotFound();
            }
            return View(tutors);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Tutors tutors)
        {
            if (id != tutors._id.ToString())
            {
                return BadRequest();
            }
            PopulateDropDowns();
            var tutorFromDatabase = await _context.Tutors.Find(t => t._id.ToString() == id).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                tutorFromDatabase.Name = tutors.Name;
                tutorFromDatabase.Email = tutors.Email;
                tutorFromDatabase.MobileNumber = tutors.MobileNumber;
                tutorFromDatabase.Citizenship = tutors.Citizenship;
                tutorFromDatabase.NRIC = tutors.NRIC;
                tutorFromDatabase.Gender = tutors.Gender;
                tutorFromDatabase.Race = tutors.Race;

                await _context.Tutors.ReplaceOneAsync(t => t._id == tutors._id, tutorFromDatabase);


                return RedirectToAction(nameof(Profile), new { id = tutors._id.ToString() });
            }
            return View(tutors);
        }
    }
}