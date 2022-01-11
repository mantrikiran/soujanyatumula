using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Entities.Dashboard.LessonSection;
using VidyaVahini.Entities.Response;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> DeleteLessonSet([FromBody] LessonSetCommand lessonSet)
        {
            _dashboardService.DeleteLessonSet(lessonSet);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<LessonSetData> InsertDashboardData([FromBody] InsertLessonSetCommand lessonSet)
            => GetResponse(_dashboardService
                .InsertDashoardData(lessonSet));

        [HttpGet("{lessonSetId}/language/{languageId}")]
        public Response<DashboardModel> Find(string lessonSetId, int languageId)
            => GetResponse(_dashboardService
                .GetDashboard(lessonSetId, languageId));

        [HttpGet("{lessonSetId}/Lessons/{languageId}")]
        public Response<DashboardModel> GetLessons(string lessonSetId, int languageId)
            => GetResponse(_dashboardService.GetLessons(lessonSetId, languageId));

        [HttpGet("{lessonSetId}/LessonSections/{lessonId}/{languageId}")]
        public Response<DashboardLessonModel> GetLessonSections(string lessonSetId, string lessonId, int languageId)
            => GetResponse(_dashboardService.GetLessonSections(lessonSetId, lessonId, languageId));

        [HttpGet("{SectionTypeId}/{languageId}")]
        public Response<DashboardSectionTextModel> GetSectiontext(int sectiontypeId, int languageId)
         => GetResponse(_dashboardService.GetSectiontext(sectiontypeId, languageId));
    
}
}