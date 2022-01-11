using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class DashboardQuestionModel
    {
        public int QuestionOrder { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int RecommendedAttempts { get; set; }
        public int SecondaryAttempts { get; set; }
        public int MaximumScore { get; set; }

        public List<DashboardQuestionHintModel> Hints { get; set; }
        public List<DashboardQuestionMediaModel> Media { get; set; }
        public List<DashboardSubQuestionModel> SubQuestions { get; set; }
    }
}
