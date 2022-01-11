using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class InsertLessonSetCommand
    {
        public string LessonSetId { get; set; }
        public int LevelId { get; set; }
        public int LessonSetOrder { get; set; }
        public IEnumerable<LessonCommand> Lessons { get; set; }
    }

    public class LessonCommand
    {
        public int LessonNumber { get; set; }
        public IEnumerable<LessonSectionCommand> LessonSections { get; set; }
    }

    public class LessonSectionCommand
    {
        public string SectionIdentifier { get; set; }
        public string LessonSectionName { get; set; }
        public string LessonSectionDescription { get; set; }
        public IEnumerable<QuestionCommand> Questions { get; set; }
    }

    public class QuestionCommand
    {
        public int QuestionOrder { get; set; }
        public string QuestionText { get; set; }
        public string MediaPath { get; set; }
        public IEnumerable<QuestionHintCommand> QuestionHints { get; set; }
        public IEnumerable<SubQuestionCommand> SubQuestions { get; set; }
    }

    public class QuestionHintCommand
    {
        public string HintText { get; set; }
        public string MediaPath { get; set; }
    }

    public class SubQuestionCommand
    {
        public int QuestionOrder { get; set; }
        public string QuestionText { get; set; }
        public IEnumerable<SubQuestionOptionCommand> SubQuestionOptions { get; set; }
    }

    public class SubQuestionOptionCommand
    {
        public int OptionOrder { get; set; }
        public int? AnswerOrder { get; set; }
        public string OptionText { get; set; }
        public bool IsAnswer { get; set; }
    }
}
