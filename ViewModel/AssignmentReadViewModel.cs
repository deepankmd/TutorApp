using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TutorAppAPI.Models;

namespace TutorAppAPI.ViewModel
{
    public class AssignmentReadViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string SubjectsToBeTutored { get; set; }
        public string StudentLevel { get; set; }
        public string FrequencyOfLessons { get; set; }
        public string YourTuitionBudget { get; set; }
        public DateTime TuitionStartDate { get; set; }
        public string LengthOfCommitment { get; set; }
        public string AvailableTimings { get; set; }
        public string PreferredTutorGender { get; set; }
        public string DescriptionOfNeeds { get; set; }
        public ObjectId ParentId { get; set; }
        public ObjectId TutorId { get; set; }
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public ICollection<TutorLevel> TutorLevel { get; set; }
        public ICollection<TutorSubject> TutorSubject { get; set; }
        public string TutorAvailability { get; set; }
        public bool IsVerified { get; set; } = false;

    }
}
