using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class SubQuestionData
    {
        public int QuestionId { get; set; }

        public IEnumerable<int> Answers { get; set; }
    }
}
