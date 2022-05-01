using StackExchange.Redis;

namespace Khonsu.CacheManager
{
    public interface IKhonsuRedisDatabaseBuilder
    {
        IKhonsuRedisDatabaseBuilder SetRedisHost(string host);

        IKhonsuRedisDatabaseBuilder SetRedisPort(int port);

        IKhonsuRedisDatabaseBuilder SetRedisPassword(string password);

        IDatabase Build();
    }
}