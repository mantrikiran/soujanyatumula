using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class SubQuestionData
    {
        public string QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int QuestionOrder { get; set; }
        public string QuestionText { get; set; }
        public IEnumerable<SubQuestionOptionData> SubQuestionOptions { get; set; }
    }
}
