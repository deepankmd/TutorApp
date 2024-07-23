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
        private readonly IMongoCollection<TutorLevel> _tutorLevels;
        private readonly IMongoCollection<AccountInfo> _accountInfo;
        private readonly IMongoCollection<TutorSubject> _tutorSubject;
        private readonly IMongoCollection<EducationLevel> _educationLevel;
        private readonly IMongoCollection<TutorCategory> _tutorCategory;
        private readonly IMongoCollection<TutorSchools> _tutorSchool;
        private readonly IMongoCollection<TutorGrade> _tutorGrade;
        private readonly IMongoCollection<TutorLocations> _tutorLocations;
        private readonly IMongoCollection<TutorGradesSubject> _tutorGradesSubject;
        private readonly IMongoCollection<TutorGradeValues> _tutorGradeValues;
        private readonly IMongoCollection<Tutors> _tutors;

        
        public RegisterTutorServices(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _tutorLevels = database.GetCollection<TutorLevel>("TutorLevels");
            _accountInfo = database.GetCollection<AccountInfo>("AccountInfo");
            _tutorSubject = database.GetCollection<TutorSubject>("TutorSubject");
            _educationLevel = database.GetCollection<EducationLevel>("EducationLevel");
            _tutorCategory = database.GetCollection<TutorCategory>("TutorCategory");
            _tutorSchool = database.GetCollection<TutorSchools>("TutorSchools");
            _tutorGrade = database.GetCollection<TutorGrade>("TutorGrade");
            _tutorLocations = database.GetCollection<TutorLocations>("TutorLocations");
            _tutorGradesSubject = database.GetCollection<TutorGradesSubject>("TutorGradesSubject");
            _tutorGradeValues = database.GetCollection<TutorGradeValues>("TutorGradeValues");
            _tutors = database.GetCollection<Tutors>("Tutors");
        }

        public async Task<List<TutorLevel>> GetAllTutorLevelsAsync()
        {
            return await _tutorLevels.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorLevel> GetTutorLevelByIdAsync(Guid id)
        {
            return await _tutorLevels.Find(t => t.ID == id).FirstOrDefaultAsync();
        }
        public async Task<List<AccountInfo>> GetAllAccountInfoAsync()
        {
            return await _accountInfo.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<AccountInfo> GetAccountInfoByIdAsync(Guid id)
        {
            return await _accountInfo.Find(a => a.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<TutorSubject>> GetAllTutorSubjectAsync()
        {
            return await _tutorSubject.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorSubject> GetTutorSubjectByIdAsync(Guid id)
        {
            return await _tutorSubject.Find(a => a.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<EducationLevel>> GetAllEducationLevelAsync()
        {
            return await _educationLevel.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<EducationLevel> GetEducationLevelByIdAsync(Guid id)
        {
            return await _educationLevel.Find(a => a.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<TutorCategory>> GetAllTutorCategoryAsync()
        {
            return await _tutorCategory.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorCategory> GetTutorCategoryByIdAsync(Guid id)
        {
            return await _tutorCategory.Find(a => a.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<TutorSchools>> GetAllTutorSchoolAsync()
        {
            return await _tutorSchool.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorSchools> GetTutorSchoolByIdAsync(Guid id)
        {
            return await _tutorSchool.Find(a => a.ID == id).FirstOrDefaultAsync();
        }
        public async Task<List<TutorGrade>> GetAllTutorGradesAsync()
        {
            return await _tutorGrade.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorGrade> GetTutorGradeByIdAsync(Guid id)
        {
            return await _tutorGrade.Find(a => a.ID == id).FirstOrDefaultAsync();
        }
        public async Task<List<TutorLocations>> GetAllTutorLocationsAsync()
        {
            return await _tutorLocations.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorLocations> GetTutorLocationsByIdAsync(string id)
        {
            return await _tutorLocations.Find(a => a.ID.ToString() == id).FirstOrDefaultAsync();
        }
        public async Task<List<TutorGradesSubject>> GetAllTutorGradesSubjectAsync()
        {
            return await _tutorGradesSubject.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorGradesSubject> GetTutorGradesSubjectByIdAsync(Guid id)
        {
            return await _tutorGradesSubject.Find(a => a.ID == id).FirstOrDefaultAsync();
        }
        public async Task<List<TutorGradeValues>> GetAllTutorGradeValuesAsync()
        {
            return await _tutorGradeValues.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<TutorGradeValues> GetTutorGradeValuesByIdAsync(Guid id)
        {
            return await _tutorGradeValues.Find(a => a.ID == id).FirstOrDefaultAsync();
        }


        public Task<TutorLocations> GetTutorLocationsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TutorSchools> GetTutorSchoolByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Tutors Create(Tutors tutor)
        {
            throw new NotImplementedException();
        }
    }
}
