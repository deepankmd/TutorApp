//using Dapper;
//using MySql.Data.MySqlClient;
//using System.Data;
//using TutorAppAPI.Models;

//namespace TutorAppAPI.Repository
//{
//    public class TutorLocationRepository : ITutorLocationRepository
//    {
//        private readonly string _connectionString;
//        private IDbConnection Connection => new MySqlConnection(_connectionString);
//        public TutorLocationRepository(string connectionString)
//        {
//            _connectionString = connectionString;
//        }
//        public async Task<IEnumerable<TutorLocations>> GetAllAsync()
//        {
//            using (var dbConnection = Connection)
//            {
//                var sql = "SELECT * FROM TutorLocations";
//                return await dbConnection.QueryAsync<TutorLocations>(sql);
//            }
//        }

//        public async Task<TutorLocations> GetByIdAsync(Guid id)
//        {
//            using (var dbConnection = Connection)
//            {
//                var sql = "SELECT * FROM TutorLocations WHERE ID = @ID";
//                return await dbConnection.QueryFirstOrDefaultAsync<TutorLocations>(sql, new { ID = id });
//            }
//        }

//        public async Task AddAsync(TutorLocations tutorLocation)
//        {
//            using (var dbConnection = Connection)
//            {
//                var sql = "INSERT INTO TutorLocations (ID, Zone, ShortDescription, Location) VALUES (@ID, @Zone, @ShortDescription, @Location)";
//                await dbConnection.ExecuteAsync(sql, tutorLocation);
//            }
//        }

//        public async Task UpdateAsync(TutorLocations tutorLocation)
//        {
//            using (var dbConnection = Connection)
//            {
//                var sql = "UPDATE TutorLocations SET Zone = @Zone, ShortDescription = @ShortDescription, Location = @Location WHERE ID = @ID";
//                await dbConnection.ExecuteAsync(sql, tutorLocation);
//            }
//        }

//        public async Task DeleteAsync(Guid id)
//        {
//            using (var dbConnection = Connection)
//            {
//                var sql = "DELETE FROM TutorLocations WHERE ID = @ID";
//                await dbConnection.ExecuteAsync(sql, new { ID = id });
//            }
//        }
//    }
//}
