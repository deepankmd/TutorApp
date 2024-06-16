using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace TutorAppAPI.Models
{
    public class TutoringPreferences 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; } 
        public List<TutorSubject> TutorSubject { get; set; }
        public List<TutorLevel> TutorLevels { get; set; }
        public List<TutorLocations> Locations { get; set; }
        public List<string> SpecialNeedsExperience { get; set; }
        public string SpecialNeedsExperienceDescription { get; set; }
        public List<string> PreferredLocations { get; set; }
        public List<string> SelectedSubjects { get; set; }
    }
}
