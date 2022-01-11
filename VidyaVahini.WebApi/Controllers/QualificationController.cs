using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Qualification;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class QualificationController : BaseController
    {
        private readonly IQualificationService _qualificationService;

        public QualificationController(IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }

        [HttpGet]
        public Response<QualificationModel> Get()
            => GetResponse(_qualificationService
                .GetQualifications());
    }
}