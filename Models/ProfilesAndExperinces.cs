using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TutorAppAPI.Models
{
    public class ProfilesAndExperinces
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public BsonArray ProfilePicture {  get; set; }
        public BsonArray UploadOfCertificates { get; set; }
    }
}
