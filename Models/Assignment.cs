using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class Assignment
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
        public DateTime AvailableTimings { get; set; } = DateTime.UtcNow;
        public string PreferredTutorGender { get; set; }
        public string DescriptionOfNeeds { get; set; }
        public ObjectId ParentId { get; set; }
        public ObjectId TutorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
    }
    public enum WorkingType
    {
        Online,
        InPerson
    }
}
