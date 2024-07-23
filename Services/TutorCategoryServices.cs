using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TutorAppAPI.Models;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    //public class TutorCategoryServices
    //{
    //    private readonly IMongoCollection<TutorCategory> _adminsCollection;

    //    public TutorCategoryServices(IOptions<MongoDbSettings> mongoDbSettings)
    //    {
    //        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
    //        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
    //        _adminsCollection = database.GetCollection<TutorCategory>("TutorCategory");
    //    }

    //    public List<TutorCategory> Get() =>
    //        _adminsCollection.Find(admin => true).ToList();

    //    public TutorCategory Get(Guid id) =>
    //        _adminsCollection.Find(admin => admin.ID == id)
    //        .FirstOrDefault();

    //    public TutorCategory Create(TutorCategory admin)
    //    {
    //        _adminsCollection.InsertOne(admin);
    //        return admin;
    //    }

    //    public void Update(Guid id, TutorCategory adminIn) =>
    //        _adminsCollection.ReplaceOne(admin => admin.ID == id, adminIn);

    //    public void Remove(TutorCategory adminIn) =>
    //        _adminsCollection.DeleteOne(admin => admin.ID == adminIn.ID);

    //    public void Remove(string id) =>
    //        _adminsCollection.DeleteOne(admin => admin.ID.ToString() == id);
    //}
}

