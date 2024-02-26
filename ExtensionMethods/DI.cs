using Microsoft.Extensions.Caching.Distributed;
using RedisDemo.Interface;

namespace RedisDemo.ExtensionMethods
{
    public static class DI
    {
        public static IServiceCollection AddDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options => 
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "demoredis";
            });
            services.AddScoped<IDistributedCacheService, DistributedCacheService>();
            return services;
        }
    }
}
