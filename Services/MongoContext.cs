using MongoDB.Driver;
using TutorAppAPI.Models;

namespace TutorAppAPI.Services
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<TutorLocations> TutorLocations =>
            _database.GetCollection<TutorLocations>("TutorLocations");
        public IMongoCollection<TutorGrade> TutorGrade =>
           _database.GetCollection<TutorGrade>("TutorGrade");

        public IMongoCollection<EducationLevel> EducationLevel =>
           _database.GetCollection<EducationLevel>("EducationLevel");
        public IMongoCollection<TutorLevel> TutorLevel =>
           _database.GetCollection<TutorLevel>("TutorLevel");
        public IMongoCollection<TutorCategory> TutorCategory =>
           _database.GetCollection<TutorCategory>("TutorCategory");
        public IMongoCollection<ParentDetails> ParentDetails =>
            _database.GetCollection<ParentDetails>("ParentDetails");
        public IMongoCollection<Tutors> Tutors =>
            _database.GetCollection<Tutors>("Tutors");
        public IMongoCollection<Admins> Admins =>
            _database.GetCollection<Admins>("Admin");
        public IMongoCollection<Assignment> Assignment =>
           _database.GetCollection<Assignment>("Assignment");

        public IMongoCollection<TutorSubject> TutorSubject =>
            _database.GetCollection<TutorSubject>("TutorSubject");
        public IMongoCollection<TutorSchools> TutorSchools =>
            _database.GetCollection<TutorSchools>("TutorSchools");
    }
}
