using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Subject;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class SubjectController : BaseController
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public Response<SubjectModel> Get()
            => GetResponse(_subjectService
                .GetSubjects());
    }
}