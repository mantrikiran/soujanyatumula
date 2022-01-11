using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class LessonSection
    {
        public LessonSection()
        {
            Questions = new HashSet<Question>();
        }

        public string LessonSectionId { get; set; }
        public string LessonId { get; set; }
        public int SectionTypeId { get; set; }
        public string LessonSectionName { get; set; }
        public string LessonSectionDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual SectionType SectionType { get; set; }
        public virtual Instruction Instruction { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
