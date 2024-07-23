namespace TutorAppAPI.Models
{
    public class TutorGradesSubject
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string GradeId { get; set; }
    }

    public class TutorGradeValues
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string TutorGradeSubjectID { get; set;}
    }
}
