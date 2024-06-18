using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class ParentDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public string StudentName { get; set; }
        public string Password { get; set;}
        public string PasswordConfirm { get; set; }
        public string RelationShip { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
    }
}
