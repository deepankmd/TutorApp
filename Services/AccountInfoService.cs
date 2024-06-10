using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Services.IServices;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    public class AccountInfoService : IAccountInfoService
    {
        private readonly IMongoCollection<AccountInfo> _accountInfoCollection;

        public AccountInfoService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _accountInfoCollection = mongoDatabase.GetCollection<AccountInfo>("AccountInfo");
        }

        public async Task CreateAsync(AccountInfo accountInfo) =>
            await _accountInfoCollection.InsertOneAsync(accountInfo);
    }
}
