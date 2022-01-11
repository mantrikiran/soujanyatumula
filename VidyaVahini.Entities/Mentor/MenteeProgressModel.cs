using System.Collections.Generic;

namespace VidyaVahini.Entities.Mentor
{
    public class MenteeProgressModel
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherLanguageId { get; set; }
        public int LevelId { get; set; }
        public string Level { get; set; }
        public string LevelStatus { get; set; }
        public string ActiveLessonSetId { get; set; }
        public int LessonStartNumber { get; set; }
        public int LessonEndNumber { get; set; }
        public bool IsComplete { get; set; }
        public int PendingSubmissionActionCount { get; set; }
        public IEnumerable<SubmittedLessonModel> SubmittedLessons { get; set; }
        public int PendingQueryActionCount { get; set; }
        public IEnumerable<string> PendingQueryIds { get; set; }
        public string LessonSetStatus { get; set; }
    }
}
