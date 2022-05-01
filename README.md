# Khonsu.CacheManager

This is a simple redis caching utility library for skipping creating templates.

## How To Get Started

In WebAPI Projects, first, you just create your database.

```c#

public static class CacheDatabaseContext
{
    public static void AddCacheDatabaseContext(this IServiceCollection services)
    {
        var khonsuRedisDatabase = new KhonsuRedisDatabaseBuilder()
            .SetRedisHost("localhost")  // default
            .SetRedisPort(6379)         // default
            .SetRedisPassword("")       // default
            .Build();
        services.AddSingleton(khonsuRedisDatabase);
    }
}
```

Create Cache Repository by inheriting KhonsuCacheRepository<T>, e.g. PlayerAccessTokenCache.
Below is shown an example.

```c#
public class PlayerAccessTokenCache : KhonsuCacheRepository<string>, IPlayerAccessTokenCache
    {
        public PlayerAccessTokenCache(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "PLAYERACCESSTOKEN";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromHours(1);
        }

        public async Task SaveByPlayerId(long playerId, string token)
        {
            var stringPlayerId = playerId.ToString();
            await SaveById(stringPlayerId, token);
        }

        public async Task<string> GetByPlayerId(long playerId)
        {
            var stringPlayerId = playerId.ToString();
            return await GetById(stringPlayerId);
        }

        public async Task DeleteByPlayerId(long playerId)
        {
            var stringPlayerId = playerId.ToString();
            await DeleteById(stringPlayerId);
        }
    }
```

Connect the database to Dependency Injection.

```c#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
        {
            // other services
            services.AddCacheDatabaseContext();
            services.AddSingleton<PlayerAccessTokenCache>();
        }
}
```