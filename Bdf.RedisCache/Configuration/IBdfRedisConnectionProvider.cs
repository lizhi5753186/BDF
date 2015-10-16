using StackExchange.Redis;

namespace Bdf.RedisCache.Configuration
{
    public interface IBdfRedisConnectionProvider
    {
        ConnectionMultiplexer GetConnection(string connectionString);

        string GetConnectionString(string service);
    }
}