using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TutorAppAPI.Models;

namespace TutorAppAPI.ViewModel
{
    public class AssignmentViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Email { get; set; }
        public string SubjectsToBeTutored { get; set; }
        public string StudentLevel { get; set; }
        public string FrequencyOfLessons { get; set; }
        public string YourTuitionBudget { get; set; }
        public DateTime TuitionStartDate { get; set; } = DateTime.UtcNow;
        public string LengthOfCommitment { get; set; }
        public string AvailableTimings { get; set; }
        public string PreferredTutorGender { get; set; }
        public string DescriptionOfNeeds { get; set; }
        public ObjectId ParentId { get; set; }
        public ObjectId TutorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public ICollection<TutorLevel> TutorLevel { get; set; }
        public ICollection<TutorSubject> TutorSubject { get; set; }
        public string TutorAvailability {  get; set; }
    }

    public class TutorSubjectViewModel
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string TutorLevelID { get; set; }
    }
    public enum WorkingType
    {
        Online,
        InPerson
    }
}
