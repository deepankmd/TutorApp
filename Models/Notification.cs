using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum NotificationType
    {
        admin,
        Parent,
        Tutor
    }
}
