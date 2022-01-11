using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class LessonData
    {
        public string LessonSetId { get; set; }
        public string LessonId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonName { get; set; }
        public string LessonDescription { get; set; }
        public string LessonCode { get; set; }
        public List<LessonSectionData> LessonSections { get; set; }
    }
}
