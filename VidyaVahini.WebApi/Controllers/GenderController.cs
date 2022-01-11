using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Gender;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class GenderController : BaseController
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet]
        public Response<GenderModel> Get()
            => GetResponse(_genderService
                .GetGenders());
    }
}