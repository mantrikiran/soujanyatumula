namespace VidyaVahini.Entities.Dashboard
{
    public class SubQuestionOptionData
    {
        public int OptionOrder { get; set; }
        public string OptionText { get; set; }
        public bool IsAnswer { get; set; }
        public int? AnswerOrder { get; set; }
    }
}
