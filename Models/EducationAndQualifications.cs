namespace TutorAppAPI.Models
{
    public class EducationAndQualifications
    {
        public Guid ID { get; set; }
        public string EducationLevel { get; set; }
        public string TutorCategories { get; set; }
        public string AcademicQualifications { get; set; }
        public List<TutorGrade> TutorGrade { get; set; }
        public List<TutorGradesSubject> TutorGradesSubject { get; set; }
        public List<TutorGradeValues> TutorGradeValues { get; set; }
    }
}
