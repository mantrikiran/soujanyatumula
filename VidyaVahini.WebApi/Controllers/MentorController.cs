using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Mentor;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class MentorController : BaseController
    {
        private readonly IMentorService _mentorService;

        public MentorController(IMentorService mentorServicee)
        {
            _mentorService = mentorServicee;
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> Activate([FromBody] ActivateCommand activate)
        {
            _mentorService.ActivateMentorAccount(activate);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> Delete([FromBody] DeleteCommand delete)
        {
            _mentorService.DeleteMentorAccount(delete);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [HttpGet("{userId}")]
        public Response<MentorModel> GetMentor(string userId)
            => GetResponse(_mentorService.GetMentor(userId));

        [HttpGet]
        public Response<MentorBasicDetailsModel> GetAll()
            => GetResponse(_mentorService.GetAllMentor());

        [HttpGet("gender/{genderId}/language/{languageId}/state/{stateId}")]
        public Response<PreferredMentorModel> GetMentorForAllocation(int genderId, int languageId, int stateId)
            => GetResponse(_mentorService.GetAvailableMentors(genderId, languageId, stateId));

        [HttpPost]
        public Response<SuccessModel> Register([FromBody] MentorCommand mentorCommand)
        {
            _mentorService.RegisterMentor(mentorCommand);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [HttpGet("dashboard/{userId}")]
        public Response<MentorDashboardModel> GetDashboard(string userId)
            => GetResponse(_mentorService.GetMentorDashboard(userId));

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> LoadNextLessonSet([FromBody] TeacherCommand teacher)
        {
            _mentorService.LoadNextLessonSet(teacher);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> RedoActiveLessonSet([FromBody] TeacherCommand teacher)
        {
            _mentorService.RedoActiveLessonSet(teacher);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }
    }
}