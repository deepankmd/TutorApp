﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class AccountInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int MobileNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string NRIC { get; set; }
        public string Citizenship { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
    }
}
