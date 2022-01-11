using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class LessonSetData
    {
        public string LessonSetId { get; set; }
        public int LessonSetOrder { get; set; }
        public int LevelId { get; set; }
        public List<LessonData> Lessons { get; set; }
    }
}
