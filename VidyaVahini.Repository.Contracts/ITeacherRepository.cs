using System.Collections.Generic;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Role;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.Teacher.Dashboard;
using VidyaVahini.Entities.Teacher.Lesson;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Entities.UserProfile;

namespace VidyaVahini.Repository.Contracts
{
    public interface ITeacherRepository
    {
        /// <summary>
        /// Gets the teacher details
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Teacher details</returns>
        FindTeacherModel GetTeacher(string email);

        /// <summary>
        /// Registers a teacher account
        /// </summary>
        /// <param name="userAccount">Teacher account details</param>
        /// <param name="userProfile">Teacher profile details</param>
        /// <param name="userLanguages">Teacher languages</param>
        /// <param name="teacherClasses">Teacher classes</param>
        /// <param name="teacherSubjects">Teacher subjects</param>
        /// <returns>success/failure</returns>
        bool RegisterTeacher(
            UserAccountData userAccount,
            UserProfileData userProfile,
            IEnumerable<UserLanguageData> userLanguages,
            IEnumerable<TeacherClassData> teacherClasses,
            IEnumerable<TeacherSubjectData> teacherSubjects);
        bool insertSyncdate(UpdateNotificationCommand syncdateinfo);        /// <summary>
        /// Creates a teacher account
        /// </summary>
        /// <param name="userAccount">Teacher account details</param>
        /// <param name="userProfile">Teacher profile details</param>
        /// <param name="userRole">Teacheraccount  user Role</param>
        /// <param name="teacherData">Teacher data</param>
        bool CreateTeacherAccount(UserAccountData userAccount,
            UserProfileData userProfile, UserRoleData userRole, TeacherData teacherData);

        /// <summary>
        /// Gets teacher dashboard
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Teacher dashboard details</returns>
        TeacherDashboardModel GetTeacherDashboard(string userId);
        bool GetUser(string contactNumber);

        /// <summary>
        /// Gets teacher dashboard
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Teacher dashboard details</returns>
        TeacherDashboardStatusModel GetTeacherDashboardStatus(string userId);
        IEnumerable<NotificationsModel> GetNotifications(string userId);
      int GetNotificount(string userId,int flag);
        string GetSyncDate(string userId);
        void UpdateNotification(NotificationCommand notificationCommand);
        void UpdateNotifyByUserId(UpdateNotificationCommand updatenotificationCommand);
        
        /// <summary>
        /// Gets a lesson section instruction
        /// </summary>
        /// <param name="lessonSectionId">Lesson Section Id</param>
        /// <param name="languageId">Language Id</param>
        /// <returns>lesson section instructions</returns>
        // InstructionModel GetLessonSectionInstructions(string lessonSectionId, int languageId);
        SectionInstructionModel GetLessonSectionInstructions(int SectionTypeId,string RoleId, int languageId);

        /// <summary>
        /// Gets a set of questions based on section
        /// </summary>
        /// <param name="lessonSectionId"></param>
        /// <returns></returns>
        IEnumerable<TeacherDashboardQuestionIdsModel> GetQuestions(string lessonSectionId);

        /// <summary>
        /// Gets questions response based on section and user
        /// </summary>
        /// <param name="lessonSectionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        TeacherLessonModel GetQuestionResponseByLessonId(string lessonId, string userId);
        TeacherLessonSetModel GetQuestionResponseByLessonSetId(string lessonsetId, string userId);
        IEnumerable<TeacherQuestionResponse> GetQuestionResponseByLessonSectionId(string lessonSectionId, string userId);

        /// <summary>
        /// Gets a question details based on question Id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="questionId"></param>
        /// <param name="languageId">Language Id</param>
        /// <returns></returns>
        TeacherDashboardQuestionsListModel GetQuestion(string userId, string questionId, int languageId);

        /// <summary>
        /// Adds or update user's response to questions
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="mediaFilePath">Media file path</param>
        /// <param name="questionResponses">Questions response</param>
        /// <returns>success/failure</returns>
        bool UpsertQuestionResponse(string userId, string lessonSectionId, string mediaFilePath, IEnumerable<QuestionResponseCommand> questionResponses,int Attempts,bool IsPerfectScore);

        /// <summary>
        /// Adds a new teacher response status
        /// </summary>
        /// <param name="userId">Teacher id</param>
        /// <param name="mentorId">Mentor Id</param>
        /// <param name="questions">Questions</param>
        /// <returns>success/failure</returns>
        bool AddResponseState(string userId, string mentorId, IEnumerable<QuestionStateCommand> questions);

        /// <summary>
        /// Deletes teacher responses and queries for all the questions in the lesson set
        /// </summary>
        /// <param name="lessonSetId">Lesson Set id</param>
        /// <param name="teacherId">Teacher id</param>
        void UpdateLessonSetState(string teacherId);

        /// <summary>
        /// Gets all the teachers without an assigned mentor
        /// </summary>
        /// <returns>teacher details</returns>
        TeachersMentorModel GetTeachersMissingMentor();

        /// <summary>
        /// Assigns a mentor to a teacher 
        /// </summary>
        /// <param name="teacherId">User id</param>
        /// <param name="mentorId">User id</param>
        /// <returns>success/failure</returns>
        bool AssignMentor(string teacherId, string mentorId);

        /// <summary>
        /// Gets a user's score for a question
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="questionId">Question id</param>
        /// <returns>Score</returns>
        QuestionScoreModel GetQuestionScore(string userId, string questionId);

        /// <summary>
        /// Update active lesson set of the teacher
        /// </summary>
        /// <param name="teacherId">Teacher id</param>
        /// <param name="lessonSetId">Lesson Set id</param>
        /// <returns>success/failure</returns>
        bool UpdateActiveLessonSet(string teacherId, string lessonSetId);

        /// <summary>
        /// Delete lesson set data
        /// </summary>
        /// <param name="teacherId">Teacher id</param>
        /// <returns>success/failure</returns>
        bool DeleteLessonSetData(string teacherId);
        IEnumerable<TeacherLessonReport> GetTeacherReport();
        IEnumerable<TeacherSummaryLessonReport> GetTeacherSummaryReport();
        int AddTeachers(IEnumerable<AddTeacherData> teacherData);
    }
}
