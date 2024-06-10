using TutorAppAPI.Models;

namespace TutorAppAPI.ViewModel
{
    public class TutorRegisterViewModel
    {
        public AccountInfo AccountInfo { get; set; }
        public TutoringPrefences TutoringPrefences { get; set; }
        public EducationAndQualifications EducationAndQualifications { get; set; }
        public List<EducationLevel> EducationLevel { get; set; }
        public List<TutorCategory> TutorCategory { get; set; }
        public List<TutorSchools> TutorSchool { get; set; }
        public List<TutorGrade> TutorGrade { get; set; }
        public string SchoolGrade { get; set; }
        public List<TutorLocations> TutorLocations { get; set; }
    }
}
