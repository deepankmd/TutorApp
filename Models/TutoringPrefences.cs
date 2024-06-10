using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace TutorAppAPI.Models
{
    public class TutoringPrefences 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; } 
        public List<TutorSubject> TutorSubject { get; set; }
        public List<TutorLevels> TutorLevels { get; set; }
        public List<TutorLocations> Locations { get; set; }
    }
}
