using System.Collections.Generic;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.Teacher.Dashboard;
using VidyaVahini.Entities.Teacher.Lesson;

namespace VidyaVahini.Service.Contracts
{
    public interface ITeacherService
    {
        /// <summary>
        /// Gets a teacher profile based on the username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>Teacher Profile</returns>
        FindTeacherModel FindTeacherProfile(string username);

        /// <summary>
        /// Registers a teacher's details in the system
        /// </summary>
        /// <param name="registerTeacher">Teacher details</param>
        void RegisterTeacher(RegisterTeacherCommand registerTeacher);
        void insertSyncdate(UpdateNotificationCommand syncdateinfo);
        void UpdateNotification(NotificationCommand notificationCommand);
        void UpdateNotifyByUserId(UpdateNotificationCommand updatenotificationCommand);


        /// <summary>
        /// Create a Teacher with Email and Username
        /// </summary>
        /// <param name="createTeacherAccount">Teacher details</param>
        bool CreateTeacherAccount(CreateTeacherAccountCommand createTeacherAccount);

        /// <summary>
        /// Gets teacher dashboard
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Teacher dashboard details</returns>
        TeacherDashboardModel GetTeacherDashboard(string userId);

        /// <summary>
        /// Gets teacher dashboard
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Teacher dashboard details</returns>
        TeacherDashboardStatusModel GetTeacherDashboardStatus(string userId);
       getNotificationsModel GetNotifications(string userId);
        GetNotifyCount GetNotificount(string userId,int flag);
        GetSyncDate GetSyncDate(string userId);

        /// <summary>
        /// Gets a lesson section instruction
        /// </summary>
        /// <param name="lessonSectionId">Lesson Section Id</param>
        /// <param name="languageId">Language Id</param>
        /// <returns>lesson section instructions</returns>
        //InstructionModel GetLessonSectionInstructions(string lessonSectionId, int languageId);
        SectionInstructionModel GetLessonSectionInstructions(int SectionTypeId,string RoleId, int languageId);
       IEnumerable<TeacherLessonReport> GetTeacherReport();
        IEnumerable<TeacherSummaryLessonReport> GetTeacherSummaryReport();

        /// <summary>
        /// Gets a set of questions based on section
        /// </summary>
        /// <param name="lessonSectionId"></param>
        /// <returns></returns>
        IEnumerable<TeacherDashboardQuestionIdsModel> GetQuestionsByLessonSectionId(string lessonSectionId);
        /// <summary>
        /// Gets questions response based on section and user
        /// </summary>
        /// <param name="lessonSectionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        TeacherLessonSetModel GetTeacherResponsebyLessonSetId(string lessonsetId, string userId);
        TeacherLessonModel GetQuestionResponseByLessonId(string lessonId, string userId);
        IEnumerable<TeacherQuestionResponse> GetQuestionResponseByLessonSectionId(string lessonSectionId, string userId);

        /// <summary>
        ///  Gets a question details based on question Id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="questionId"></param>
        /// <param name="languageId">Language Id</param>
        /// <returns></returns>
        TeacherDashboardQuestionsListModel GetQuestionByQuestionId(string userId, string questionId, int languageId);

        /// <summary>
        /// Adds or update user's response to questions
        /// </summary>
        /// <param name="assignmentResponse">User response</param>
        /// <returns>success/failure</returns>
        bool UpsertQuestionResponse(QuestionResponsesCommand assignmentResponse);

        /// <summary>
        /// Update teacher response status to submit for a question
        /// </summary>
        /// <param name="responseState">Response state details</param>
        void UpdateQuestionResponseState(ResponseStateCommand responseState);

        /// <summary>
        /// Update teacher response status to complete and submit for a lesson section 
        /// </summary>
        /// <param name="responseState">Response state details</param>
        void UpdateSectionResponseState(SectionStateCommand responseState);

        /// <summary>
        /// Gets all the teachers without an assigned mentor
        /// </summary>
        /// <returns>teacher details</returns>
        TeachersMentorModel GetTeachersMissingMentor();

        /// <summary>
        /// Assigns a mentor to a teacher 
        /// </summary>
        /// <param name="assignMentor">Assignment details</param>
        void AssignMentor(AssignMentorCommand assignMentor);

        /// <summary>
        /// Gets a user's score for a question
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="questionId">Question id</param>
        /// <returns>Score</returns>
        QuestionScoreModel GetQuestionScore(string userId, string questionId);

        /// <summary>
        /// Update active lesson set
        /// </summary>
        /// <param name="activeLessonSet">Current Lesson Set details</param>
        /// <returns>New Lesson Set Id</returns>
        LessonSetData UpdateActiveLessonSet(ActiveLessonSetCommand activeLessonSet);

        /// <summary>
        /// Update active lesson set
        /// </summary>
        /// <param name="activeLessonSet">Current Lesson Set details</param>
        void DeleteLessonSetData(TeacherCommand teacher);
    }
}
