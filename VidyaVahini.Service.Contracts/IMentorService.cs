using System.Collections.Generic;
using VidyaVahini.Entities.Mentor;
using VidyaVahini.Entities.Teacher;

namespace VidyaVahini.Service.Contracts
{
    public interface IMentorService
    {
        /// <summary>
        /// Activates a mentor account
        /// </summary>
        /// <param name="activate">User id</param>
        void ActivateMentorAccount(ActivateCommand activate);

        /// <summary>
        /// Deletes a mentor account
        /// </summary>
        /// <param name="delete">User id</param>
        void DeleteMentorAccount(DeleteCommand delete);

        /// <summary>
        /// Gets a mentor details
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Mentor Details</returns>
        MentorModel GetMentor(string userId);

        /// <summary>
        /// Gets all registered mentors
        /// </summary>
        /// <returns>Mentor basic details</returns>
        MentorBasicDetailsModel GetAllMentor();

        /// <summary>
        /// Creates and registers a new mentor in the system
        /// </summary>
        /// <param name="mentor">Mentor Details</param>
        void RegisterMentor(MentorCommand mentor);

        /// <summary>
        /// Gets available mentors based on teacher preferences
        /// </summary>
        /// <param name="teacherGenderId">Gender id</param>
        /// <param name="teacherLanguageId">Language id</param>
        /// <param name="teacherStateId">State id</param>
        /// <returns>Available mentors</returns>
        PreferredMentorModel GetAvailableMentors(int teacherGenderId, int teacherLanguageId, int teacherStateId);

        /// <summary>
        /// Gets the mentor dashboard
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Mentor dashboard</returns>
        MentorDashboardModel GetMentorDashboard(string userId);

        /// <summary>
        /// Progress teacher to the next lesson set
        /// </summary>
        /// <param name="teacher">Teacher id</param>
        void LoadNextLessonSet(TeacherCommand teacher);

        /// <summary>
        /// Redo Active Lesson Set
        /// </summary>
        /// <param name="teacher">Teacher id</param>
        void RedoActiveLessonSet(TeacherCommand teacher);
    }
}
