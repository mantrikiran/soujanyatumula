using System.ComponentModel.DataAnnotations;

namespace VidyaVahini.Entities.Query
{
    public class CreateQueryCommand
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string TeacherId { get; set; }
        [Required]
        public string QuestionId { get; set; }
        [Required]
        public int subQuestionId { get; set; }
        public string QueryText { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaBase64String { get; set; }
        public string MediaFileName { get; set; }
        [Required]
        public string LessonSetId { get; set; }
    }
}
