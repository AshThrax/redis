using Microsoft.Extensions.Caching.Distributed;
using RedisDemo.Interface;
using System.Net;
using System;
using System.Text.Json;
using System.Xml.Linq;

namespace RedisDemo.ExtensionMethods
{
    public class DistributedCacheService : IDistributedCacheService
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            if (_cache == null)
                throw new ArgumentNullException(nameof(_cache));

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unusedExpireTime
            };

            var jsonData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(recordId, jsonData, options);
        }

        public async Task<T> GetRecordAsync<T>(string recordId)
        {
            if (_cache == null)
                throw new ArgumentNullException(nameof(_cache));

            var jsonData = await _cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
