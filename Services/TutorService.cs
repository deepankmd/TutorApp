using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    public class TutorService
    {
        private readonly IMongoCollection<Tutors> _tutorsCollection;

        public TutorService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _tutorsCollection = database.GetCollection<Tutors>("Tutors");
        }

        public List<Tutors> Get() =>
            _tutorsCollection.Find(tutor => true).ToList();

        public Tutors Get(ObjectId id) =>
            _tutorsCollection.Find(tutor => tutor._id == id)
            .FirstOrDefault();

        public Tutors Create(Tutors tutors)
        {
            _tutorsCollection.InsertOne(tutors);
            return tutors;
        }

        public void Update(ObjectId id, Tutors tutorIn) =>
            _tutorsCollection.ReplaceOne(tutor => tutor._id == id, tutorIn);

        public void Remove(Tutors tutorIn) =>
            _tutorsCollection.DeleteOne(tutor => tutor._id == tutorIn._id);

        public void Remove(ObjectId id) =>
            _tutorsCollection.DeleteOne(tutor => tutor._id == id);
    }
}
