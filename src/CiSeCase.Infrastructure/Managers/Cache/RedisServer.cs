using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace CiSeCase.Infrastructure.Managers.Cache
{
    public class RedisServer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly int _currentDatabaseId = 0;
        private readonly string _connectionString;

        public RedisServer(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("RedisConnection");
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_connectionString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);

        }

        public IDatabase Database => _database;

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(_connectionString).FlushDatabase(_currentDatabaseId);
        }
    }
}