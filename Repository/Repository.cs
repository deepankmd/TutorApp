using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using TutorAppAPI.Repository.IRepository;

namespace TutorAppAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public Repository(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var dbConnection = Connection)
            {
                var sql = $"SELECT * FROM {_tableName}";
                return await dbConnection.QueryAsync<T>(sql);
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            using (var dbConnection = Connection)
            {
                var sql = $"SELECT * FROM {_tableName} WHERE ID = @ID";
                return await dbConnection.QueryFirstOrDefaultAsync<T>(sql, new { ID = id });
            }
        }

        public async Task AddAsync(T entity)
        {
            using (var dbConnection = Connection)
            {
                var sql = $"INSERT INTO {_tableName} VALUES (@ID, @LevelShortName, @LevelName)";
                await dbConnection.ExecuteAsync(sql, entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using (var dbConnection = Connection)
            {
                var sql = $"UPDATE {_tableName} SET LevelShortName = @LevelShortName, LevelName = @LevelName WHERE ID = @ID";
                await dbConnection.ExecuteAsync(sql, entity);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var dbConnection = Connection)
            {
                var sql = $"DELETE FROM {_tableName} WHERE ID = @ID";
                await dbConnection.ExecuteAsync(sql, new { ID = id });
            }
        }
    }
}
