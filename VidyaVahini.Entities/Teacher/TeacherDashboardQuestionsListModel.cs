using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class TeacherDashboardQuestionsListModel
    {
        public string QuestionText { get; set; }
        public IEnumerable<QuestionHintText> Hint { get; set; }
        public IEnumerable<TeacherQuestionMedia> MediaSource { get; set; }
        public SubQuestionDetails SubQuestionDetails { get; set; }
    }
}
