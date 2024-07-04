using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class AssignmentApplied
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string TutorName { get; set; }
        public string AssignmentID { get; set; }
        public string UserID { get; set; }
        public string TutorLocation { get; set; }
        public string YourRate { get; set; }    
        public string AllAvailableTimings { get; set; }
        public string WhenCanYouStartEarliest { get; set; }
        public string WhyShouldYouBeChosen { get; set; }
    }
}
