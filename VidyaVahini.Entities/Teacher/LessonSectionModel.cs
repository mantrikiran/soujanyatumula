using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class LessonSectionModel
    {
        public string SectionId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string SectionStatus { get; set; }
        public List<QuestionResponseModel> QuestionResponses { get; set; }
    }
}
