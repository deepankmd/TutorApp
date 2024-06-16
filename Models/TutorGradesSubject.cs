using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class TutorGradesSubject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string GradeId { get; set; }
    }

    public class TutorGradeValues
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
    }
}
