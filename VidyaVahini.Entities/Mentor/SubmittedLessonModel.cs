using System.Collections.Generic;

namespace VidyaVahini.Entities.Mentor
{
    public class SubmittedLessonModel
    {
        public string LessonId { get; set; }
        public int LessonNumber { get; set; }
        public IEnumerable<SubmittedLessonSectionModel> SubmittedLessonSections { get; set; }
    }
}
