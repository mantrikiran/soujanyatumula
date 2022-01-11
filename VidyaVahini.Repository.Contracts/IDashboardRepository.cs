using System.Collections.Generic;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Entities.Dashboard.LessonSection;

namespace VidyaVahini.Repository.Contracts
{
    public interface IDashboardRepository
    {
        /// <summary>
        /// Deletes a lesson set
        /// </summary>
        /// <param name="lessonSetId"></param>
        /// <returns>success/failure</returns>
        bool DeleteLessonSet(string lessonSetId);

        /// <summary>
        /// Gets all lesson sets
        /// </summary>
        /// <returns>Lesson set data</returns>
        IEnumerable<LessonSetData> GetAllLessonSets();

        /// <summary>
        /// Gets dashboard lessons, section and question for the lesson set
        /// </summary>
        /// <param name="lessonSetId">Lesson Set id</param>
        /// <returns>Dashboard Lessons</returns>
        /// <param name="languageId">Language id</param>
        DashboardModel GetDashboard(string lessonSetId, int languageId);

        /// <summary>
        /// Insert a lesson set
        /// </summary>
        /// <param name="lessonSet">Lesson set details</param>
        /// <returns>Lesson set details</returns>
        LessonSetData InsertLessonSet(LessonSetData lessonSet);

        /// <summary>
        /// Inserts a lesson
        /// </summary>
        /// <param name="lesson">Lesson details</param>
        /// <returns>Lesson details</returns>
        LessonData InsertLesson(LessonData lesson);

        /// <summary>
        /// Inserts a lesson section
        /// </summary>
        /// <param name="lessonSection">Lesson section details</param>
        /// <returns>Lesson section details</returns>
        LessonSectionData InsertLessonSection(LessonSectionData lessonSection);

        /// <summary>
        /// Inserts a question
        /// </summary>
        /// <param name="questionData">Question details</param>
        /// <returns>Question details</returns>
        QuestionData InsertQuestion(QuestionData questionData);

        /// <summary>
        /// Inserts the question media
        /// </summary>
        /// <param name="questionMedia">Question media</param>
        /// <returns>Question media</returns>
        QuestionMediaData InsertQuestionMedia(QuestionMediaData questionMedia);

        /// <summary>
        /// Inserts a sub-question
        /// </summary>
        /// <param name="subQuestion">Subquestion details</param>
        /// <returns>Subquestion details</returns>
        SubQuestionData InsertSubQuestion(SubQuestionData subQuestion);
        DashboardModel GetLessons(string lessonSetId, int languageId);
        DashboardLessonModel GetLessonSections(string lessonSetId, string lessonId, int languageId);
        DashboardSectionTextModel GetSectiontext(int sectiontypeid, int userlanguageId);
    }
}