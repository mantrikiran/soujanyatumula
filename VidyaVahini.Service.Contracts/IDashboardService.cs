using System.Collections.Generic;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Entities.Dashboard.LessonSection;

namespace VidyaVahini.Service.Contracts
{
    public interface IDashboardService
    {
        /// <summary>
        /// Deletes a lesson set ad associated content
        /// </summary>
        /// <param name="lessonSet">Lesson set id</param>
        void DeleteLessonSet(LessonSetCommand lessonSet);

        /// <summary>
        /// Insert dashboard content
        /// </summary>
        /// <param name="lessonSet">Dashboard content</param>
        /// <returns>Dashboard data</returns>
        LessonSetData InsertDashoardData(InsertLessonSetCommand lessonSet);

        /// <summary>
        /// Gets dashboard lessons, section and question for the lesson set
        /// </summary>
        /// <param name="lessonSetId">Lesson Set id</param>
        /// <param name="languageId">Language id</param>
        /// <returns>Dashboard Data</returns>
        DashboardModel GetDashboard(string lessonSetId, int languageId);
        DashboardModel GetLessons(string lessonSetId, int languageId);
        DashboardLessonModel GetLessonSections(string lessonSetId, string lessonId, int languageId);
        DashboardSectionTextModel GetSectiontext(int sectiontypeId, int languageId);
    }
}
