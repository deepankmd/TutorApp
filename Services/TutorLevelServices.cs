using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    public class TutorLevelServices
    {
        private readonly IMongoCollection<TutorLevel> _tutorLevels;

        public TutorLevelServices(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _tutorLevels = database.GetCollection<TutorLevel>("TutorLevels");
        }

        public async Task<List<TutorLevel>> GetAsync() => await _tutorLevels.Find(_ => true).ToListAsync();

        public async Task<TutorLevel> GetAsync(string id) => await _tutorLevels.Find(t => t.ID.ToString() == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TutorLevel tutorLevel) => await _tutorLevels.InsertOneAsync(tutorLevel);

        public async Task UpdateAsync(string id, TutorLevel tutorLevel) => await _tutorLevels.ReplaceOneAsync(t => t.ID.ToString() == id, tutorLevel);

        public async Task DeleteAsync(string id) => await _tutorLevels.DeleteOneAsync(t => t.ID.ToString() == id);

    }
}
