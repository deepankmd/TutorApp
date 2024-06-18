using TutorAppAPI.Models;

namespace TutorAppAPI.ViewModel
{
    public class TutorRegisterViewModel
    {
        public AccountInfo AccountInfo { get; set; }
        public TutoringPreferences TutoringPreferences { get; set; }
        public EducationAndQualifications EducationAndQualifications { get; set; }
        public List<EducationLevel> EducationLevel { get; set; }
        public List<TutorCategory> TutorCategory { get; set; }
        public List<TutorSchools> TutorSchool { get; set; }
        public List<TutorGrade> TutorGrade { get; set; }
        public string SchoolGrade { get; set; }
        public List<TutorLocations> TutorLocations { get; set; }
        public List<string> EducationLevelSelected { get; set; }
        public List<string> TutorCategorySelected { get; set; }
        public List<TutorGradesSubject> TutorGradesSubject { get; set; }
        public List<TutorGradeValues> TutorGradeValues { get; set; }
    }

    public class TutorRegisterSaveViewModel
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int MobileNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string NRIC { get; set; }
        public string Citizenship { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public List<string> PreferredSelectedSubjects{ get; set; }
        public List<string> PreferredSpecialNeedsExperience { get; set; }
        public List<string> PreferredLocations { get; set; }
        public List<string> EducationLevelSelected { get; set; }
        public List<string> TutorCategorySelected { get; set; }
        public List<string> GradesAndQualifications { get; set; }
    }
}