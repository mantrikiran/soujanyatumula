using Microsoft.AspNetCore.Mvc;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.Teacher.Dashboard;
using VidyaVahini.Entities.Teacher.Lesson;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("{email}")]
        public Response<FindTeacherModel> Find(string email)
            => GetResponse(_teacherService
                .FindTeacherProfile(email));

        [HttpPost]
        public Response<SuccessModel> Register([FromBody] RegisterTeacherCommand registerTeacher)
        {
            _teacherService.RegisterTeacher(registerTeacher);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> Create([FromBody] CreateTeacherAccountCommand createTeacherAccount)
        {
            bool response = _teacherService.CreateTeacherAccount(createTeacherAccount);

            return GetResponse(new SuccessModel
            {
                Success = response
            });
        }

        [HttpGet("dashboard/{userId}")]
        public Response<TeacherDashboardModel> GetDashboard(string userId)
            => GetResponse(_teacherService
                .GetTeacherDashboard(userId));

        [HttpGet("{userId}/dashboard")]
        public Response<TeacherDashboardStatusModel> GetDashboardStatus(string userId)
            => GetResponse(_teacherService
                .GetTeacherDashboardStatus(userId));

        [HttpGet("GetNotifications/{userId}")]
        public Response<getNotificationsModel> GetNotifications(string userId)
           => GetResponse(_teacherService
               .GetNotifications(userId));


        [HttpPost("insertSyncdate")]
        public Response<SuccessModel> insertSyncdate([FromBody] UpdateNotificationCommand syncdateinfo)
        {
            _teacherService.insertSyncdate(syncdateinfo);

            return GetResponse(new SuccessModel
            {
                Success = true
            });

        }

        [HttpGet("GetSyncDate/{userId}")]
        public Response<GetSyncDate> GetSyncDate(string userId)
          => GetResponse(_teacherService
              .GetSyncDate(userId));

        [HttpGet("GetNotificationsCount/{userId}/{flag}")]
        public Response<GetNotifyCount> GetNotificount(string userId,int flag)
          => GetResponse(_teacherService
              .GetNotificount(userId,flag));


        [Route("UpdateNotification")]
        [HttpPost]
        public Response<SuccessModel> UpdateNotification([FromBody] NotificationCommand notificationCommand)
        {
            _teacherService.UpdateNotification(notificationCommand);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }
        [Route("UpdateNotifyByUserId")]
        [HttpPost]
        public Response<SuccessModel> UpdateNotifyByUserId([FromBody] UpdateNotificationCommand updatenotificationCommand)
        {
            _teacherService.UpdateNotifyByUserId(updatenotificationCommand);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [HttpGet("DetailedStatusReport")]
        public Response<TeacherReport> GetTeacherReport()
           => GetResponse(new TeacherReport { TeachersList = _teacherService.GetTeacherReport() });

        [HttpGet("TeacherSummaryReport")]
        public Response<TeacherSummaryReport> TeacherSummaryReport()
          => GetResponse(new TeacherSummaryReport { Data = _teacherService.GetTeacherSummaryReport() });





        //[HttpGet("dashboard/section/{lessonSectionId}/instructions/{languageId}")]
        //public Response<InstructionModel> GetSectionInstructions(string lessonSectionId, int languageId)
        //    => GetResponse(_teacherService
        //        .GetLessonSectionInstructions(lessonSectionId, languageId));

        [HttpGet("dashboard/section/{SectionTypeId}/{RoleId}/instructions/{languageId}")]
        public Response<SectionInstructionModel> GetSectionInstructions(int SectionTypeId,string RoleId, int languageId)
            => GetResponse(_teacherService
                .GetLessonSectionInstructions(SectionTypeId,RoleId,languageId));

        [HttpGet("dashboard/section/{lessonSectionId}/questions")]
        public Response<TeacherQuestionIdsModel> GetQuestionsByLessonSectionId(string lessonSectionId)
            => GetResponse(new TeacherQuestionIdsModel { TeacherDashboardQuestionIds = _teacherService.GetQuestionsByLessonSectionId(lessonSectionId)});

        [HttpGet("{userId}/question/{questionId}/language/{languageId}")]
        public Response<TeacherDashboardQuestionsListModel> GetQuestionByQuestionId(string userId, string questionId,int languageId)
           => GetResponse(_teacherService
               .GetQuestionByQuestionId(userId, questionId,languageId));


        [HttpGet("{userId}/LessonSetId/{lessonsetId}/responses")]
        public Response<TeacherLessonSetModel> GetTeacherResponsebyLessonSetId(string lessonsetId, string userId)
            => GetResponse(_teacherService.GetTeacherResponsebyLessonSetId(lessonsetId, userId)
            );


        [HttpGet("{userId}/Lesson/{lessonId}/responses")]
        public Response<TeacherLessonModel> GetTeacherResponsebyLessonId(string lessonId, string userId)
            => GetResponse( _teacherService.GetQuestionResponseByLessonId(lessonId, userId)
            );


        [HttpGet("{userId}/section/{lessonSectionId}/responses")]
        public Response<TeacherQuestionResponseModel> GetTeacherResponse(string lessonSectionId, string userId)
            => GetResponse(new TeacherQuestionResponseModel
            {
                TeacherQuestionResponses = _teacherService
                .GetQuestionResponseByLessonSectionId(lessonSectionId, userId)
            });
        [Route("QuestionResponse")]
        [HttpPost]
        public Response<SuccessModel> CreateTeacherQuestionResponse([FromBody] QuestionResponsesCommand createAssignmentResponse)
        {
            bool response = _teacherService.UpsertQuestionResponse(createAssignmentResponse);

            return GetResponse(new SuccessModel
            {
                Success = response
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> UpdateSectionState([FromBody] SectionStateCommand sectionState)
        {
            _teacherService.UpdateSectionResponseState(sectionState);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> UpdateQuestionState([FromBody] ResponseStateCommand responseState)
        {
            _teacherService.UpdateQuestionResponseState(responseState);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [HttpGet("{userId}/question/{questionId}/score")]
        public Response<QuestionScoreModel> GetQuestionScore(string userId, string questionId)
        => GetResponse(_teacherService
                .GetQuestionScore(userId, questionId));

        [HttpGet("mentorassignment")]
        public Response<TeachersMentorModel> GetTeachersMissingMentor()
        => GetResponse(_teacherService
                .GetTeachersMissingMentor());





        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> AssignMentor([FromBody] AssignMentorCommand assignMentor)
        {
            _teacherService.AssignMentor(assignMentor);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<LessonSetData> UpdateActiveLessonSet([FromBody] ActiveLessonSetCommand activeLessonSet)
            => GetResponse(_teacherService
                .UpdateActiveLessonSet(activeLessonSet));

      
        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> DeleteLessonSetData([FromBody] TeacherCommand teacher)
        {
            _teacherService.DeleteLessonSetData(teacher);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }
    }
}