using System.Collections.Generic;

namespace VidyaVahini.Entities.Dashboard
{
    public class DashboardLessonSectionModel
    {
        public string SectionId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
        public DashboardSectionInstructionModel Instructions { get; set; }
        public List<DashboardQuestionModel> Questions { get; set; }
      
    }
}
