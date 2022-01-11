using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class SubQuestionDetails : QuestionScoreModel
    {
        public IEnumerable<SubQuestionModel> SubQuestions { get; set; }
        public IEnumerable<SubQuestionResultModel> Results { get; set; }
    }
}
