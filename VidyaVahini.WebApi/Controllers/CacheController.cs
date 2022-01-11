using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Cache;
using VidyaVahini.Entities.Response;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class CacheController : BaseController
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> Remove([FromBody] RemoveCacheCommand removeCache)
        {
            _cacheService.RemoveCache(removeCache);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }
    }
}