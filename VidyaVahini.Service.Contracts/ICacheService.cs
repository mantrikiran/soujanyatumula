using VidyaVahini.Entities.Cache;

namespace VidyaVahini.Service.Contracts
{
    public interface ICacheService
    {
        /// <summary>
        /// Removes cache key
        /// </summary>
        /// <param name="removeCache">Cache key</param>
        /// <returns>Dashboard Data</returns>
        void RemoveCache(RemoveCacheCommand removeCache);
    }
}
