using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Response;

namespace VidyaVahini.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        public Response<T> GetResponse<T>(T body) where T : class
            => new Response<T>()
            {
                Body = body,
                Success = true
            };

        public Response<T> GetResponse<T>(T body, string errorCode, string errorMessage) where T : class
            => new Response<T>()
            {
                Body = body,
                Error = new ErrorDetails()
                {
                    Code = errorCode,
                    Message = errorMessage
                }
            };
    }
}