using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class ResponseStateCommand
    {
        public string TeacherId { get; set; }
        public string MentorId { get; set; }
        public IEnumerable<QuestionStateCommand> Questions { get; set; }
    }
}
