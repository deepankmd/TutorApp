using MongoDB.Bson;
using TutorAppAPI.Models;

namespace TutorAppAPI.Services.IServices
{
    public interface IRegisterTutorServices
    {
        Task<List<TutorLevel>> GetAllTutorLevelsAsync();
        Task<TutorLevel> GetTutorLevelByIdAsync(Guid id);
        Task<List<AccountInfo>> GetAllAccountInfoAsync();
        Task<AccountInfo> GetAccountInfoByIdAsync(Guid id);
        Task<List<TutorSubject>> GetAllTutorSubjectAsync();
        Task<TutorSubject> GetTutorSubjectByIdAsync(Guid id);
        Task<List<EducationLevel>> GetAllEducationLevelAsync();
        Task<EducationLevel> GetEducationLevelByIdAsync(Guid id);
        Task<List<TutorCategory>> GetAllTutorCategoryAsync();
        Task<TutorCategory> GetTutorCategoryByIdAsync(Guid id);
        Task<List<TutorSchools>> GetAllTutorSchoolAsync();
        Task<TutorSchools> GetTutorSchoolByIdAsync(ObjectId id);
        Task<List<TutorGrade>> GetAllTutorGradesAsync();
        Task<TutorGrade> GetTutorGradeByIdAsync(Guid id);
        Task<List<TutorLocations>> GetAllTutorLocationsAsync();
        Task<TutorLocations> GetTutorLocationsByIdAsync(Guid id);
        Task<List<TutorGradesSubject>> GetAllTutorGradesSubjectAsync();
        Task<TutorGradesSubject> GetTutorGradesSubjectByIdAsync(Guid id);
        Task<List<TutorGradeValues>> GetAllTutorGradeValuesAsync();
        Task<TutorGradeValues> GetTutorGradeValuesByIdAsync(Guid id);
        Tutors Create(Tutors tutor);
    }
}
