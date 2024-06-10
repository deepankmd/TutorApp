using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services.IServices;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    public class RegisterTutorServices : IRegisterTutorServices
    {
        private readonly IMongoCollection<TutorLevels> _tutorLevels;
        private readonly IMongoCollection<AccountInfo> _accountInfo;
        private readonly IMongoCollection<TutorSubject> _tutorSubject;
        private readonly IMongoCollection<EducationLevel> _educationLevel;
        private readonly IMongoCollection<TutorCategory> _tutorCategory;
        private readonly IMongoCollection<TutorSchools> _tutorSchool;
        private readonly IMongoCollection<TutorGrade> _tutorGrade;
        private readonly IMongoCollection<TutorLocations> _tutorLocations;


        public RegisterTutorServices(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _tutorLevels = database.GetCollection<TutorLevels>("TutorLevel");
            _accountInfo = database.GetCollection<AccountInfo>("AccountInfo");
            _tutorSubject = database.GetCollection<TutorSubject>("TutorSubject");
            _educationLevel = database.GetCollection<EducationLevel>("EducationLevel");
            _tutorCategory = database.GetCollection<TutorCategory>("TutorCategory");
            _tutorSchool = database.GetCollection<TutorSchools>("TutorSchools");
            _tutorGrade = database.GetCollection<TutorGrade>("TutorGrade");
            _tutorLocations = database.GetCollection<TutorLocations>("TutorLocations");
        }

        public async Task<List<TutorLevels>> GetAllTutorLevelsAsync()
        {
            return await _tutorLevels.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorLevels> GetTutorLevelByIdAsync(ObjectId id)
        {
            return await _tutorLevels.Find(t => t._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<AccountInfo>> GetAllAccountInfoAsync()
        {
            return await _accountInfo.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<AccountInfo> GetAccountInfoByIdAsync(ObjectId id)
        {
            return await _accountInfo.Find(a => a._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TutorSubject>> GetAllTutorSubjectAsync()
        {
            return await _tutorSubject.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorSubject> GetTutorSubjectByIdAsync(ObjectId id)
        {
            return await _tutorSubject.Find(a => a._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<EducationLevel>> GetAllEducationLevelAsync()
        {
            return await _educationLevel.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<EducationLevel> GetEducationLevelByIdAsync(ObjectId id)
        {
            return await _educationLevel.Find(a => a._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TutorCategory>> GetAllTutorCategoryAsync()
        {
            return await _tutorCategory.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorCategory> GetTutorCategoryByIdAsync(ObjectId id)
        {
            return await _tutorCategory.Find(a => a._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TutorSchools>> GetAllTutorSchoolAsync()
        {
            return await _tutorSchool.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorSchools> GetTutorSchoolByIdAsync(ObjectId id)
        {
            return await _tutorSchool.Find(a => a._id == id).FirstOrDefaultAsync();
        }
        public async Task<List<TutorGrade>> GetAllTutorGradesAsync()
        {
            return await _tutorGrade.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorGrade> GetTutorGradeByIdAsync(ObjectId id)
        {
            return await _tutorGrade.Find(a => a._id == id).FirstOrDefaultAsync();
        }
        public async Task<List<TutorLocations>> GetAllTutorLocationsAsync()
        {
            return await _tutorLocations.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorLocations> GetTutorLocationsByIdAsync(ObjectId id)
        {
            return await _tutorLocations.Find(a => a._id == id).FirstOrDefaultAsync();
        }
    }
}
