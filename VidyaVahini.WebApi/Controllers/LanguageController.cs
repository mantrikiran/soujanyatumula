using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Language;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public Response<LanguageModel> Get([FromQuery] bool includeEnglish)
            => GetResponse(_languageService
                .GetLanguages(includeEnglish));
    }
}