using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<Admins> _adminsCollection;

        public AdminService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _adminsCollection = database.GetCollection<Admins>("Admin");
        }

        public List<Admins> Get() =>
            _adminsCollection.Find(admin => true).ToList();

        public Admins Get(ObjectId id) =>
            _adminsCollection.Find(admin => admin._id == id)
            .FirstOrDefault();

        public Admins Create(Admins admin)
        {
            _adminsCollection.InsertOne(admin);
            return admin;
        }

        public void Update(ObjectId id, Admins adminIn) =>
            _adminsCollection.ReplaceOne(admin => admin._id == id, adminIn);

        public void Remove(Admins adminIn) =>
            _adminsCollection.DeleteOne(admin => admin._id == adminIn._id);

        public void Remove(ObjectId id) =>
            _adminsCollection.DeleteOne(admin => admin._id == id);
    }
}
