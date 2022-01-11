using System.ComponentModel.DataAnnotations;

namespace VidyaVahini.Entities.Query
{
    public class EditQueryCommand
    {
        [Required]
        public int QueryDataId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string LessonSetId { get; set; }
        public string QueryText { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaBase64String { get; set; }
        public string MediaFileName { get; set; }
    }
}