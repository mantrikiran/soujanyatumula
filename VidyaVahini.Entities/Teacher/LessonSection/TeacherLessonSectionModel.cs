using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher.LessonSection
{
    public  class TeacherLessonSectionModel
    {
        public string LessonSectionId { get; set; }
        public string SectionId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
        
        public IEnumerable<TeacherQuestionResponse> TeacherQuestionResponses { get; set; }
    }
}
