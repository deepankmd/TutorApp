using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TutorAppAPI.Models;
using TutorAppAPI.Services.IServices;
using TutorAppAPI.ViewModel;

namespace TutorAppAPI.Controllers
{
    public class RegisterTutorController : Controller
	{
        private readonly IAccountInfoService _accountInfoService;
        private readonly IRegisterTutorServices _registerTutorServices;

        public RegisterTutorController(IAccountInfoService accountInfoService, IRegisterTutorServices registerTutorServices)
        {
            _accountInfoService = accountInfoService;
            _registerTutorServices = registerTutorServices;
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
            TutorRegisterViewModel tutorRegisterViewModel = new TutorRegisterViewModel { AccountInfo = new AccountInfo(),
                TutoringPrefences = new TutoringPrefences { TutorLevels = tutorLevels, TutorSubject = tutorSubject }, EducationLevel = educationLevel,
                TutorCategory = tutorCategory,
                TutorSchool = tutorSchool,
                TutorGrade = tutorGrade,
                TutorLocations = tutorLocations,
            };
            return View(tutorRegisterViewModel);
		}

		[HttpPost]
        public async Task<IActionResult> Register([FromBody] AccountInfo accountInfo)
        {
            if (accountInfo.Password != accountInfo.PasswordConfirm)
            {
                return BadRequest("Password and confirmation password do not match.");
            }

            // Here you would normally hash the password before storing it.
            await _accountInfoService.CreateAsync(accountInfo);

            return Ok("Registration successful.");
        }
    }
}
