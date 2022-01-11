using System.Collections.Generic;
using VidyaVahini.Entities.Mentor;

namespace VidyaVahini.Repository.Contracts
{
    public interface IMentorRepository
    {
        /// <summary>
        /// Create a new mentor
        /// </summary>
        /// <param name="mentorData">Mentor details</param>
        void AddMentor(MentorData mentorData);

        /// <summary>
        /// Delete a mentor
        /// </summary>
        /// <param name="userId">User id</param>
        void DeleteMentor(string userId);

        /// <summary>
        /// Gets a mentor details
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Mentor details</returns>
        MentorModel GetMentor(string userId);

        /// <summary>
        /// Gets a mentor details
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>Mentor details</returns>
        MentorData GetMentorByUsername(string username);
       
        /// <summary>
        /// Gets all mentor details
        /// </summary>
        /// <returns>Mentoe details</returns>
        IEnumerable<MentorBasicDetails> GetAllMentor();

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
        /// <param name="teacherId">Teacher id</param>
        /// <returns>success/failure</returns>
        bool LoadNextLessonSet(string teacherId);

        /// <summary>
        /// Redo Active Lesson Set
        /// </summary>
        /// <param name="teacherId">Teacher id</param>
        /// <returns>success/failure</returns>
        bool RedoActiveLessonSet(string teacherId);
        bool GetUser(string contactNumber);
    }
}
