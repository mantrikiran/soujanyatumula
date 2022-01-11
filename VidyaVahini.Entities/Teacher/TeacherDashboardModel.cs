using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class TeacherDashboardModel
    {
        public int LevelId { get; set; }
        public string LevelCode { get; set; }
        public int LessonsCompletedInLevel { get; set; }
        public int TotalLessonsInLevel { get; set; }
        public int LessonsCompletedInCurrentSet { get; set; }
        public int TotalLessonsInCurrentSet { get; set; }
        public string LessonSetId { get; set; }
        public List<LessonModel> Lessons { get; set; }
        public int PendingQueriesCount { get; set; }
        public IEnumerable<string> PendingQueryIds { get; set; }
    }
}
