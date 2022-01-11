using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class LessonModel
    {
        public string LessonId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonCode { get; set; }
        public string LessonStatus { get; set; }
        public List<LessonSectionModel> LessonSections { get; set; }
    }
}
