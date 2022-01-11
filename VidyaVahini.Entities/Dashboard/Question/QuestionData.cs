using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class QuestionData
    {
        public string LessonSectionId { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionOrder { get; set; }
        public int? RecommendedAttempts { get; set; }
        public int? SecondaryAttempts { get; set; }
        public List<SubQuestionData> SubQuestions { get; set; }
        public IEnumerable<QuestionHintData> QuestionHints { get; set; }
    }

    public class QuestionHintData
    {
        public string HintText { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaId { get; set; }
        public string MediaSource { get; set; }
    }
}
