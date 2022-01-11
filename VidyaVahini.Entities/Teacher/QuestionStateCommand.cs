namespace VidyaVahini.Entities.Teacher
{
    public class QuestionStateCommand
    {
        public string QuestionId { get; set; }
        public int State { get; set; }
        public int TeacherResponseId { get; set; }
        public string Comments { get; set; }
    }
}
