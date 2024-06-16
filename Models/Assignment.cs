using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class Assignment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string SubjectLooking { get; set; }
        public string Description { get; set; }
        public string PreferredTutorGender { get; set; }
        public ObjectId ParentId { get; set; }
        public ObjectId TutorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public WorkingType WorkingType { get; set; }
        public TutorLocations Location { get; set; }

    }
    public enum WorkingType
    {
        Online,
        InPerson
    }
}
