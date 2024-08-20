using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class RegisterTutorController : Controller
    {
        //private readonly IAccountInfoService _accountInfoService;
        //private readonly IRegisterTutorServices _registerTutorServices;
        //private readonly MongoContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly MySqlContext _context;

        public RegisterTutorController(IWebHostEnvironment environment, MySqlContext context)
        {
            _environment = environment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tutorLevels = await _context.TutorLevel.ToListAsync();
            var tutorSubject = await _context.TutorSubject.ToListAsync();
            var educationLevel = await _context.EducationLevel.ToListAsync();
            var tutorCategory = await _context.TutorCategory.ToListAsync();
            var tutorSchool = await _context.TutorSchools.ToListAsync();
            var tutorGrade = await _context.TutorGrade.ToListAsync();
            var tutorLocations = await _context.TutorLocations.ToListAsync();
            var tutorGradesSubject = await _context.TutorGradesSubject.ToListAsync();
            var tutorGradeValues = await _context.TutorGradeValues.ToListAsync();

            TutorRegisterViewModel tutorRegisterViewModel = new TutorRegisterViewModel
            {
                AccountInfo = new AccountInfo(),
                EducationAndQualifications = new EducationAndQualifications { TutorGradesSubject = tutorGradesSubject, TutorGradeValues = tutorGradeValues },
                TutoringPreferences = new TutoringPreferences { TutorLevels = tutorLevels, TutorSubject = tutorSubject, Locations = tutorLocations },
                EducationLevel = educationLevel,
                TutorCategory = tutorCategory,
                TutorSchool = tutorSchool,
                TutorGrade = tutorGrade,
                TutorGradesSubject = tutorGradesSubject,
                TutorGradeValues = tutorGradeValues,
            };
            return View(tutorRegisterViewModel);
        }

        public async Task<IActionResult> Register()
        {
            return await AddTutorEmptyValue();
        }

        private async Task<IActionResult> AddTutorEmptyValue()
        {
            var tutorLevels = await _context.TutorLevel.ToListAsync();
            var tutorSubject = await _context.TutorSubject.ToListAsync();
            var educationLevel = await _context.EducationLevel.ToListAsync();
            var tutorCategory = await _context.TutorCategory.ToListAsync();
            var tutorSchool = await _context.TutorSchools.ToListAsync();
            var tutorGrade = await _context.TutorGrade.ToListAsync();
            var tutorLocations = await _context.TutorLocations.ToListAsync();
            var tutorGradesSubject = await _context.TutorGradesSubject.ToListAsync();
            var tutorGradeValues = await _context.TutorGradeValues.ToListAsync();

            TutorRegisterViewModel tutorRegisterViewModel = new TutorRegisterViewModel
            {
                AccountInfo = new AccountInfo(),
                EducationAndQualifications = new EducationAndQualifications { TutorGradesSubject = tutorGradesSubject, TutorGradeValues = tutorGradeValues },
                TutoringPreferences = new TutoringPreferences { TutorLevels = tutorLevels, TutorSubject = tutorSubject, Locations = tutorLocations },
                EducationLevel = educationLevel,
                TutorCategory = tutorCategory,
                TutorSchool = tutorSchool,
                TutorGrade = tutorGrade,
                TutorGradesSubject = tutorGradesSubject,
                TutorGradeValues = tutorGradeValues,
            };
            return View(tutorRegisterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(List<IFormFile> userInputFiles)
        {
            TutorRegisterViewModel model = new TutorRegisterViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.Name = Request.Form["AccountInfo.Name"];
                    accountInfo.Email = Request.Form["AccountInfo.Email"];
                    accountInfo.MobileNumber = (Request.Form["AccountInfo.MobileNumber"]);
                    accountInfo.Password = Request.Form["AccountInfo.Password"];
                    accountInfo.PasswordConfirm = Request.Form["AccountInfo.PasswordConfirm"];
                    accountInfo.NRIC = Request.Form["AccountInfo.NRIC"];
                    accountInfo.Citizenship = Request.Form["AccountInfo.Citizenship"];
                    accountInfo.DateofBirth = DateTime.Parse(Request.Form["AccountInfo.DateofBirth"]);
                    accountInfo.Gender = Request.Form["AccountInfo.Gender"];
                    accountInfo.Race = Request.Form["AccountInfo.Race"];

                    var selectedSubjects = Request.Form["selectedSubjects"].ToList();
                    var specialNeedsExperience = Request.Form["specialNeeds"].ToList();
                    var preferredLocations = Request.Form["locations"].ToList();

                    var educationLevelSelected = Request.Form["EducationLevelSelected"].ToList();
                    var TutorCategorySelected = Request.Form["TutorCategorySelected"].ToList();

                    model.TutoringPreferences = new TutoringPreferences();
                    model.EducationLevelSelected = educationLevelSelected;
                    model.TutoringPreferences.SpecialNeedsExperience = specialNeedsExperience;
                    model.TutoringPreferences.PreferredLocations = preferredLocations;

                    var TutorRegisterSaveViewModel = new Tutors();

                    TutorRegisterSaveViewModel.Name = accountInfo.Name;
                    TutorRegisterSaveViewModel.Email = accountInfo.Email;
                    TutorRegisterSaveViewModel.MobileNumber = long.Parse(accountInfo.MobileNumber);
                    TutorRegisterSaveViewModel.Password = accountInfo.Password;
                    TutorRegisterSaveViewModel.PasswordConfirm = accountInfo.PasswordConfirm;
                    TutorRegisterSaveViewModel.NRIC = accountInfo.NRIC;
                    TutorRegisterSaveViewModel.Citizenship = accountInfo.Citizenship;
                    TutorRegisterSaveViewModel.DateofBirth = accountInfo.DateofBirth;
                    TutorRegisterSaveViewModel.Gender = accountInfo.Gender;
                    TutorRegisterSaveViewModel.Race = accountInfo.Race;
                    TutorRegisterSaveViewModel.PreferredSelectedSubjects = selectedSubjects;
                    TutorRegisterSaveViewModel.PreferredSpecialNeedsExperience = specialNeedsExperience;
                    TutorRegisterSaveViewModel.PreferredLocations = preferredLocations;
                    TutorRegisterSaveViewModel.EducationLevelSelected = educationLevelSelected;
                    TutorRegisterSaveViewModel.TutorCategorySelected = TutorCategorySelected;


                    await _context.Tutors.AddAsync(TutorRegisterSaveViewModel);

                    // Create a message for the email
                    string emailMessage = $"Dear {TutorRegisterSaveViewModel.Name},\n\n" +
                                          $"We have successfully created a new Tutor profile for {TutorRegisterSaveViewModel.Name}.\n\n" +
                                          "Thank you for using our service.\n\n" +
                                          "Best regards,\n" +
                                          "Your TutorMaster";

                    //string subject = "Tutor Master Registration Team";

                    Notification notification = new Notification
                    {
                        UserName = TutorRegisterSaveViewModel.Name,
                        Subject = "Tutor Created",
                        Email = TutorRegisterSaveViewModel.Email,
                        Mobile = TutorRegisterSaveViewModel.MobileNumber.ToString(),
                        Message = emailMessage,
                        CreatedDate = DateTime.UtcNow,
                        IsRead = true,
                        NotificationType = NotificationType.admin
                    };
                    await _context.Notification.AddAsync(notification);

                    //await NotificationService.SendEmailAsync(TutorRegisterSaveViewModel.Email, subject, emailMessage);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    //_logger.LogError(ex, "Error saving tutor to MongoDB");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data. Please try again.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAccountInfo(TutorRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tutorRegisterSaveViewModel = new Tutors();
                tutorRegisterSaveViewModel.Name = model.AccountInfo.Name;
                tutorRegisterSaveViewModel.Email = model.AccountInfo.Email;
                tutorRegisterSaveViewModel.MobileNumber = long.Parse(model.AccountInfo.MobileNumber);
                tutorRegisterSaveViewModel.Password = model.AccountInfo.Password;
                tutorRegisterSaveViewModel.PasswordConfirm = model.AccountInfo.PasswordConfirm;
                tutorRegisterSaveViewModel.NRIC = model.AccountInfo.NRIC;
                tutorRegisterSaveViewModel.Citizenship = Request.Form["Citizenship"].ToString();
                tutorRegisterSaveViewModel.DateofBirth = model.AccountInfo.DateofBirth;
                tutorRegisterSaveViewModel.Gender = Request.Form["Gender"].ToString();
                tutorRegisterSaveViewModel.Race = Request.Form["Race"].ToString();
                await _context.Tutors.AddAsync(tutorRegisterSaveViewModel);
                model.ID = tutorRegisterSaveViewModel.ID.ToString();

                HttpContext.Session.SetString("RegisterTutorID", model.ID);
                return Json(new { success = true, message = $"{model.ID}" });

            }
            return Json(new { success = false, message = "Failed to validate model" });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTutorPref(TutorRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _id = Guid.Parse(HttpContext.Session.GetString("RegisterTutorID"));

                var tutorRegisterSaveViewModel = await _context.Tutors.Where(_ => _.ID == _id).FirstOrDefaultAsync();

                var selectedSubjects = Request.Form["selectedSubjects"].ToList();
                var specialNeedsExperience = Request.Form["specialNeeds"].ToList();
                var preferredLocations = Request.Form["locations"].ToList();

                tutorRegisterSaveViewModel.PreferredSelectedSubjects = selectedSubjects;
                tutorRegisterSaveViewModel.PreferredSpecialNeedsExperience = specialNeedsExperience;
                tutorRegisterSaveViewModel.TextNeeds = Request.Form["textspecialNeedsExperienceDescription"].ToString();
                tutorRegisterSaveViewModel.PreferredLocations = preferredLocations;

                await _context.Tutors.AddAsync(tutorRegisterSaveViewModel);
                model.ID = tutorRegisterSaveViewModel.ID.ToString();
                return Json(new { success = true, message = $"{model.ID}" });

            }
            return Json(new { success = false, message = "Failed to validate model" });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile profileImage, List<IFormFile> certInputFiles)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "temp");
            Directory.CreateDirectory(uploadsFolder);


            if (profileImage != null)
            {
                var profileImagePath = Path.Combine(uploadsFolder, profileImage.FileName);
                using (var fileStream = new FileStream(profileImagePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(fileStream);
                }
            }

            if (certInputFiles != null && certInputFiles.Count > 0)
            {
                foreach (var certFile in certInputFiles)
                {
                    var certFilePath = Path.Combine(uploadsFolder, certFile.FileName);
                    using (var fileStream = new FileStream(certFilePath, FileMode.Create))
                    {
                        await certFile.CopyToAsync(fileStream);
                    }
                }
            }

            return RedirectToAction("Index"); // Redirect to a different action or view after upload
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile profileImage)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "imageData", "temp");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var _id = Guid.Parse(HttpContext.Session.GetString("RegisterTutorID"));
            var tutorRegisterSaveViewModel = await _context.Tutors.Where(_ => _.ID == _id).FirstOrDefaultAsync();

            if (tutorRegisterSaveViewModel != null)
            {
                tutorRegisterSaveViewModel.FileName = profileImage.FileName;
                tutorRegisterSaveViewModel.FileType = Path.GetExtension(profileImage.FileName);
            }

            if (profileImage != null)
            {
                var profileImagePath = Path.Combine(uploadsFolder, profileImage.FileName);
                using (var fileStream = new FileStream(profileImagePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(fileStream);
                }
                await _context.Tutors.AddAsync(tutorRegisterSaveViewModel);

                return Json(new { success = true, imagePath = "/imageData/temp/" + profileImage.FileName });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateEducation(TutorRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _id = Guid.Parse(HttpContext.Session.GetString("RegisterTutorID"));

                var tutorRegisterSaveViewModel = await _context.Tutors.FirstOrDefaultAsync(_ => _.ID == _id);
                tutorRegisterSaveViewModel.EducationLevelSelected = model.EducationLevelSelected;
                tutorRegisterSaveViewModel.TutorCategorySelected = model.TutorCategorySelected;
                tutorRegisterSaveViewModel.TutorSchools = Request.Form["TutorSchools"].ToList();
                tutorRegisterSaveViewModel.TutorFrom = Request.Form["SchoolFrom"].ToList();
                tutorRegisterSaveViewModel.TutorTo = Request.Form["SchoolTo"].ToList();
                tutorRegisterSaveViewModel.GradesAndQualifications = Request.Form["TutorGradesSubjects"].ToList();

                await _context.Tutors.AddAsync(tutorRegisterSaveViewModel);
                model.ID = tutorRegisterSaveViewModel.ID.ToString();
                return Json(new { success = true, message = $"{model.ID}" });

            }
            return Json(new { success = false, message = "Failed to validate model" });
        }

        [HttpPost]
        public async Task<IActionResult> UploadCertificates(List<IFormFile> certInputFiles)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "temp");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var _id = Guid.Parse(HttpContext.Session.GetString("RegisterTutorID"));

            var tutorRegisterSaveViewModel = _context.Tutors.FirstOrDefault(_ => _.ID == _id);

            if (tutorRegisterSaveViewModel == null)
            {
                var fileNames = certInputFiles.Select(_ => _.FileName).ToList();
                var fileType = certInputFiles.Select(_ => Path.GetExtension(_.FileName)).ToList();
                var fileIDs = certInputFiles.Select(_ => _id + _.FileName).ToList();

                tutorRegisterSaveViewModel.CertFileName = fileNames;
                tutorRegisterSaveViewModel.CertFileType = fileType;
                tutorRegisterSaveViewModel.CertFileID = fileIDs;
            }
            var imagePaths = new List<string>();

            if (certInputFiles != null && certInputFiles.Count > 0)
            {
                foreach (var certFile in certInputFiles)
                {
                    var certFilePath = Path.Combine(uploadsFolder, _id + certFile.FileName);
                    using (var fileStream = new FileStream(certFilePath, FileMode.Create))
                    {
                        await certFile.CopyToAsync(fileStream);
                    }
                    imagePaths.Add("/uploads/temp/" + _id + certFile.FileName);
                }
                await _context.Tutors.AddAsync(tutorRegisterSaveViewModel);
                return Json(new { success = true, imagePaths });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTutor()
        {
            var _id = Guid.Parse(HttpContext.Session.GetString("RegisterTutorID"));

            var tutorRegisterSaveViewModel = await _context.Tutors.FirstOrDefaultAsync(_ => _.ID == _id);

            if (tutorRegisterSaveViewModel == null)
            {

            }
            await _context.Tutors.AddAsync(tutorRegisterSaveViewModel);

            return RedirectToAction("Index", "Home");
        }
    }
}
