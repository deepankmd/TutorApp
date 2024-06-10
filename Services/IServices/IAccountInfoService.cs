using TutorAppAPI.Models;

namespace TutorAppAPI.Services.IServices
{
    public interface IAccountInfoService
    {
        Task CreateAsync(AccountInfo accountInfo);
    }
}
