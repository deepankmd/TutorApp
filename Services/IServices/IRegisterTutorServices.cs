using MongoDB.Bson;
using TutorAppAPI.Models;

namespace TutorAppAPI.Services.IServices
{
    public interface IRegisterTutorServices
    {
        Task<List<TutorLevel>> GetAllTutorLevelsAsync();
        Task<TutorLevel> GetTutorLevelByIdAsync(ObjectId id);
        Task<List<AccountInfo>> GetAllAccountInfoAsync();
        Task<AccountInfo> GetAccountInfoByIdAsync(ObjectId id);
        Task<List<TutorSubject>> GetAllTutorSubjectAsync();
        Task<TutorSubject> GetTutorSubjectByIdAsync(ObjectId id);
        Task<List<EducationLevel>> GetAllEducationLevelAsync();
        Task<EducationLevel> GetEducationLevelByIdAsync(ObjectId id);
        Task<List<TutorCategory>> GetAllTutorCategoryAsync();
        Task<TutorCategory> GetTutorCategoryByIdAsync(ObjectId id);
        Task<List<TutorSchools>> GetAllTutorSchoolAsync();
        Task<TutorSchools> GetTutorSchoolByIdAsync(ObjectId id);
        Task<List<TutorGrade>> GetAllTutorGradesAsync();
        Task<TutorGrade> GetTutorGradeByIdAsync(ObjectId id);
        Task<List<TutorLocations>> GetAllTutorLocationsAsync();
        Task<TutorLocations> GetTutorLocationsByIdAsync(ObjectId id);
        Task<List<TutorGradesSubject>> GetAllTutorGradesSubjectAsync();
        Task<TutorGradesSubject> GetTutorGradesSubjectByIdAsync(ObjectId id);
        Task<List<TutorGradeValues>> GetAllTutorGradeValuesAsync();
        Task<TutorGradeValues> GetTutorGradeValuesByIdAsync(ObjectId id);
        Tutors Create(Tutors tutor);
    }
}
