using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class QuestionData
    {
        public string QuestionId { get; set; }
        public int Score { get; set; }
        public IEnumerable<SubQuestionData> SubQuestions { get; set; }
    }
}
