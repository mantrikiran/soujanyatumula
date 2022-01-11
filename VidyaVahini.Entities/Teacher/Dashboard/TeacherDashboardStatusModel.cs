using System.Collections.Generic;
using VidyaVahini.Core.Enum;

namespace VidyaVahini.Entities.Teacher
{
    public class TeacherDashboardStatusModel
    {
        public string UserId { get; set; }
        public bool RedoActiveLessonSet { get; set; }
        public bool LoadNextLessonSet { get; set; }
        public int LessonsCompletedInLevel { get; set; }
        public int LessonsCompletedInCurrentSet { get; set; }
        public int TotalLessonsInLevel { get; set; }
        public int TotalLessonsInCurrentSet { get; set; }
        public TeacherDashboardQueryModel PendingQueries { get; set; }
        public IEnumerable<TeacherLessonStatusModel> LessonStatuses { get; set; }
    }

    public class TeacherDashboardQueryModel
    {
        public int Count { get; set; }
        public IEnumerable<string> QueryIds { get; set; }
    }

    public class TeacherLessonStatusModel
    {
        public string LessonId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonStatus { get; set; }
        public string LessonCode { get; set; }
        public IEnumerable<TeacherLessonSectionStatusModel> LessonSectionStatuses { get; set; }
    }

    public class TeacherLessonSectionStatusModel
    {
        public string LessonSectionId { get; set; }
        public string LessonSectionCode { get; set; }
        public string LessonSectionStatus { get; set; }
        public string LessonSectionName { get; set; }
        public IEnumerable<TeacherQuestionStatusModel> LessonQuestionStatuses { get; set; }
    }

    public class TeacherQuestionStatusModel
    {
        public string QuestionId { get; set; }
        public string QuestionStatus { get; set; }
    }
}
