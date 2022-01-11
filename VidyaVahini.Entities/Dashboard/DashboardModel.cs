using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class DashboardModel
    {
        public int LevelId { get; set; }
        public string LevelCode { get; set; }
        public int TotalLessonsInLevel { get; set; }
        public int TotalLessonsInCurrentSet { get; set; }
        public string LessonSetId { get; set; }       
        public List<DashboardLessonModel> Lessons { get; set; }
    }
}
