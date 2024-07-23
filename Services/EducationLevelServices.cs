using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    public class EducationLevelServices
    {
        private readonly IMongoCollection<EducationLevel> _adminsCollection;

        public EducationLevelServices(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _adminsCollection = database.GetCollection<EducationLevel>("EducationLevel");
        }

        public List<EducationLevel> Get() =>
            _adminsCollection.Find(admin => true).ToList();

        public EducationLevel Get(Guid id) =>
            _adminsCollection.Find(admin => admin.ID == id)
            .FirstOrDefault();

        public EducationLevel Create(EducationLevel admin)
        {
            _adminsCollection.InsertOne(admin);
            return admin;
        }

        public void Update(Guid id, EducationLevel adminIn) =>
            _adminsCollection.ReplaceOne(admin => admin.ID == id, adminIn);

        public void Remove(EducationLevel adminIn) =>
            _adminsCollection.DeleteOne(admin => admin.ID == adminIn.ID);

        public void Remove(string id) =>
            _adminsCollection.DeleteOne(admin => admin.ID.ToString() == id);
    }
}
