using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class QuestionResponseCommand
    {
        public string QuestionId { get; set; }
        public string ResponseText { get; set; }
        public string MediaStream { get; set; }
        public IEnumerable<SubQuestionCommand> SubQuestions { get; set; }
    }
    public class QuestionResponsesCommand
    {
        public string UserId { get; set; }
        public string LessonSetId { get; set; }
        public string LessonSectionId { get; set; }
        public IEnumerable<QuestionResponseCommand> QuestionResponses { get; set; }
        public int Attempts { get; set; }
        public bool IsPerfectScore { get; set; }
    }

    public class SubQuestionCommand
    {
        public int SubQuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public IEnumerable<int> Answers { get; set; }
    }
}
