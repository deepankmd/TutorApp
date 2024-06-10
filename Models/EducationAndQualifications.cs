using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class EducationAndQualifications
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string EducationLevel { get; set; }
       public string TutorCategories { get; set; }
       public string AcademicQualifications { get; set; }
       public string Grades { get; set; }
    }
}
