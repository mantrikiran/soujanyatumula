using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Demo;
using VidyaVahini.Service.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace VidyaVahini.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class DemoController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IDemoService _demoService;

        public DemoController(ILogger logger, IDemoService demoService)
        {
            _logger = logger;
            _demoService = demoService;
        }

        [HttpGet]
        public Response<DemoResponse> Get()
        {
            var result = _demoService.Demo();
            _logger.LogDebug("Log Test from Demo Controller - Get method.");
            return GetResponse(result);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [Authorize]
        public Response<DemoResponse> GetV1_1()
        {
            var result = _demoService.Demo();
            _logger.LogDebug("Log Test from Demo Controller - GetV1_1 method.");
            return GetResponse(result, "500", "Internal Server Error");
        }
    }
}