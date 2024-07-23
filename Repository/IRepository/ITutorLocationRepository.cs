using TutorAppAPI.Models;

namespace TutorAppAPI.Repository
{
    public interface ITutorLocationRepository
    {
        Task<IEnumerable<TutorLocations>> GetAllAsync();
        Task<TutorLocations> GetByIdAsync(Guid id);
        Task AddAsync(TutorLocations tutorLocation);
        Task UpdateAsync(TutorLocations tutorLocation);
        Task DeleteAsync(Guid id);
    }
}
