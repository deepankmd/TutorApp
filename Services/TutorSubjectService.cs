
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    public class TutorSubjectServices
    {
        private readonly IMongoCollection<TutorSubject> _tutorSubjects;
        private readonly IMongoCollection<TutorLevel> _tutorLevel;

        public TutorSubjectServices(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _tutorSubjects = database.GetCollection<TutorSubject>("TutorSubject");
            _tutorLevel = database.GetCollection<TutorLevel>("TutorLevel");
        }

        public async Task<List<TutorSubject>> GetAsync() => await _tutorSubjects.Find(_ => true).ToListAsync();

        public async Task<TutorSubject> GetAsync(string id) => await _tutorSubjects.Find(t => t.ID.ToString() == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TutorSubject tutorSubject) => await _tutorSubjects.InsertOneAsync(tutorSubject);

        public async Task UpdateAsync(string id, TutorSubject tutorSubject) => await _tutorSubjects.ReplaceOneAsync(t => t.ID.ToString() == id, tutorSubject);

        public async Task DeleteAsync(string id) => await _tutorSubjects.DeleteOneAsync(t => t.ID.ToString() == id);

    }
}
