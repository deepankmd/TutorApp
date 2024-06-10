using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class TutorLocations
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Zone { get; set; }
        public string ShortDescription { get; set; }
        public string Location { get; set; }

    }
}
