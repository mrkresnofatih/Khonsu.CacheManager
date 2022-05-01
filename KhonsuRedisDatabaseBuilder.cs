using StackExchange.Redis;

namespace Khonsu.CacheManager
{
    public class KhonsuRedisDatabaseBuilder : IKhonsuRedisDatabaseBuilder
    {
        public KhonsuRedisDatabaseBuilder()
        {
            Reset();
        }
        
        private string _redisHost;
        private int _redisPort;
        private string _redisPassword;

        private void Reset()
        {
            _redisHost = "localhost";
            _redisPort = 6379;
            _redisPassword = "password";
        }

        private string GetConnectionString()
        {
            return $"{_redisHost}:{_redisPort},abortConnect=false,password={_redisPassword}";
        }

        public IKhonsuRedisDatabaseBuilder SetRedisHost(string host)
        {
            _redisHost = host;
            return this;
        }

        public IKhonsuRedisDatabaseBuilder SetRedisPort(int port)
        {
            _redisPort = port;
            return this;
        }

        public IKhonsuRedisDatabaseBuilder SetRedisPassword(string password)
        {
            _redisPassword = password;
            return this;
        }

        public IDatabase Build()
        {
            IDatabase result = ConnectionMultiplexer
                .Connect(GetConnectionString())
                .GetDatabase();;
            Reset();
            return result;
        }
    }
}