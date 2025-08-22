using EMC.BuildingBlocks.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EMC.BuildingBlocks.DependencyInjection
{
    public static class RedisInyection
    {

        public static IServiceCollection AddRedisInyection(this IServiceCollection services, IConfiguration config)
        {
            var redisConn = config.GetValue<string>("Redis:ConnectionString");


           // var prueba = config.GetValue<string>("MongoDbSettings:ConnectionString");

            if (!string.IsNullOrEmpty(redisConn))
            {
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConn!));
                services.AddSingleton<IRedisCacheRepository, RedisCacheRepository>();
                services.AddSingleton<ICompanyConfigurationCacheService, CompanyConfigurationCacheService>();
            }
            return services;
        }
    }
}
