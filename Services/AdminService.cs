using Microsoft.Extensions.Options;
using TutorAppAPI.Models;
using TutorAppAPI.Settings;

namespace TutorAppAPI.Services
{
    //public class AdminService
    //{

    //    public AdminService(IOptions<MongoDbSettings> mongoDbSettings)
    //    {
    //        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
    //        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
    //        _adminsCollection = database.GetCollection<Admins>("Admin");
    //    }

    //    public List<Admins> Get() =>
    //        _adminsCollection.Find(admin => true).ToList();

    //    public Admins Get(Guid id) =>
    //        _adminsCollection.Find(admin => admin.ID == id)
    //        .FirstOrDefault();

    //    public Admins Create(Admins admin)
    //    {
    //        _adminsCollection.InsertOne(admin);
    //        return admin;
    //    }

    //    public void Update(Guid id, Admins adminIn) =>
    //        _adminsCollection.ReplaceOne(admin => admin.ID == id, adminIn);

    //    public void Remove(Admins adminIn) =>
    //        _adminsCollection.DeleteOne(admin => admin.ID == adminIn.ID);

    //    public void Remove(Guid id) =>
    //        _adminsCollection.DeleteOne(admin => admin.ID == id);
    //}
}
