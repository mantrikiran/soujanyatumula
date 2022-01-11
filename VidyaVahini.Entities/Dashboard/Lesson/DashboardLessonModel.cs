using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class DashboardLessonModel
    {
        public string LessonId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonCode { get; set; }
        public List<DashboardLessonSectionModel> LessonSections { get; set; }
    }
}
