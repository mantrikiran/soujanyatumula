using Microsoft.Extensions.Caching.Memory;
using System;
using VidyaVahini.Infrastructure.Contracts;

namespace VidyaVahini.Infrastructure.Cache
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public MemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key.ToLower());
        }

        public void Set(string key, object value, TimeSpan duration)
        {
            _cache.Set(key.ToLower(), value, duration);
        }

        public void Remove(string key)
        {
            _cache.Remove(key.ToLower());
        }
    }
}
