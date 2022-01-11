using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class LessonSectionData
    {
        public string LessonId { get; set; }
        public string LessonSectionId { get; set; }
        public string LessonSectionName { get; set; }
        public string LessonSectionDescription { get; set; }
        public string LessonSectionInstructions { get; set; }
        public int SectionTypeId { get; set; }
        public List<QuestionData> Questions { get; set; }
    }
}
