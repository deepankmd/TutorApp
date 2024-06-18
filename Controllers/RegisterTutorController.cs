using Microsoft.AspNetCore.Mvc;
using TutorAppAPI.Models;
using TutorAppAPI.Services;
using TutorAppAPI.Services.IServices;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class RegisterTutorController : Controller
	{
        private readonly IAccountInfoService _accountInfoService;
        private readonly IRegisterTutorServices _registerTutorServices;
        private readonly MongoContext _context;

        public RegisterTutorController(IAccountInfoService accountInfoService, IRegisterTutorServices registerTutorServices, MongoContext context)
        {
            _accountInfoService = accountInfoService;
            _registerTutorServices = registerTutorServices;
            _context = context;
        }

        public async Task<IActionResult> Index()
		{
            var tutorLevels = await _registerTutorServices.GetAllTutorLevelsAsync();
            var tutorSubject = await _registerTutorServices.GetAllTutorSubjectAsync();
            var educationLevel = await _registerTutorServices.GetAllEducationLevelAsync();
            var tutorCategory = await _registerTutorServices.GetAllTutorCategoryAsync();
            var tutorSchool = await _registerTutorServices.GetAllTutorSchoolAsync();
            var tutorGrade = await _registerTutorServices.GetAllTutorGradesAsync();
            var tutorLocations = await _registerTutorServices.GetAllTutorLocationsAsync();
            var tutorGradesSubject = await _registerTutorServices.GetAllTutorGradesSubjectAsync();
            var tutorGradeValues = await _registerTutorServices.GetAllTutorGradeValuesAsync();

            TutorRegisterViewModel tutorRegisterViewModel = new TutorRegisterViewModel { AccountInfo = new AccountInfo(),
                EducationAndQualifications = new EducationAndQualifications{ TutorGradesSubject = tutorGradesSubject, TutorGradeValues = tutorGradeValues },
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
                    accountInfo.MobileNumber = long.Parse(Request.Form["AccountInfo.MobileNumber"]);
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
                    TutorRegisterSaveViewModel.MobileNumber = accountInfo.MobileNumber;
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
                    

                    _context.Tutors.InsertOne(TutorRegisterSaveViewModel);

                    // Create a message for the email
                    string emailMessage = $"Dear {TutorRegisterSaveViewModel.Name},\n\n" +
                                          $"We have successfully created a new Tutor profile for {TutorRegisterSaveViewModel.Name}.\n\n" +
                                          "Thank you for using our service.\n\n" +
                                          "Best regards,\n" +
                                          "Your TutorMaster";
                    string subject = "Tutor Master Registration Team";

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
                    _context.Notification.InsertOne(notification);

                    //await NotificationService.SendEmailAsync(TutorRegisterSaveViewModel.Email, subject, emailMessage);

                    return RedirectToAction("Index","Home");
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "Error saving tutor to MongoDB");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data. Please try again.");
                }
            }

            return View(model);
        }
    }
}
