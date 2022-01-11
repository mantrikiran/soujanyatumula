using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class DashboardSubQuestionModel
    {
        public int SubQuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int SubQuestionOrder { get; set; }
        public string SubQuestionText { get; set; }
        public int TotalAnswer { get; set; }
        public IEnumerable<int> SubQuestionAnswer { get; set; }
        public List<DashboardSubQuestionOptionModel> Options { get; set; }
     
    }
}
