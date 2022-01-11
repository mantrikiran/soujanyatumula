using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class SubQuestionModel
    {
        public int SubQuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int SubQuestionOrder { get; set; }
        public string SubQuestionText { get; set; }
        public int TotalAnswer { get; set; }
        public IEnumerable<SubQuestionOptionModel> Options { get; set; }      
    }
}
