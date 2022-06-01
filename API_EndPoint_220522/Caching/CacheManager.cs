using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EndPoint_220522.Caching
{
    public class CacheManager : ICacheManager
    {
        private IDistributedCache cache;
        private DistributedCacheEntryOptions options;

        public CacheManager(IDistributedCache cache)
        {
            this.cache = cache;
            options = new DistributedCacheEntryOptions
            { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(90), SlidingExpiration = TimeSpan.FromSeconds(30) };
        }

        public async Task<bool> TrySetAsync<T>(string key, T value)
        {
            try
            {
                string json = JsonConvert.SerializeObject(value);
                await cache.SetStringAsync(key, json, options);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<T> TryGetAsync<T>(string key)
        {
            try
            {
                var res = await cache.GetStringAsync(key);
                return JsonConvert.DeserializeObject<T>(res);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
