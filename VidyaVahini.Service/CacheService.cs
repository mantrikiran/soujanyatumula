using VidyaVahini.Entities.Cache;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class CacheService : ICacheService
    {
        private readonly ICache _cache;

        public CacheService(ICache cache)
        {
            _cache = cache;
        }

        public void RemoveCache(RemoveCacheCommand removeCache)
        {
            _cache.Remove(removeCache.Key);
        }
    }
}
