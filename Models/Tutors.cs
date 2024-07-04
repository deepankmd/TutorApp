using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TutorAppAPI.Models
{
    public class Tutors
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string NRIC { get; set; }
        public string Citizenship { get; set; }

        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public List<string> PreferredSelectedSubjects { get; set; }
        public string TextNeeds { get; set; }
        public List<string> PreferredSpecialNeedsExperience { get; set; }
        public List<string> PreferredLocations { get; set; }
        public List<string> EducationLevelSelected { get; set; }
        public List<string> TutorCategorySelected { get; set; }
        public List<string> GradesAndQualifications { get; set; }
        public List<string> TutorSchools { get; set; }
        public List<string> TutorFrom { get; set; }
        public List<string> TutorTo { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileID { get; set; }

        public List<string> CertFileName { get; set; }
        public List<string> CertFileType { get; set; }
        public List<string> CertFileID { get; set; }

        public bool IsVerified { get; set; } = false;

    }
}
