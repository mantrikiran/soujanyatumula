using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class SectionType
    {
        public SectionType()
        {
            LessonSections = new HashSet<LessonSection>();
        }

        public int SectionTypeId { get; set; }
        public string SectionTypeCode { get; set; }
        public string SectionTypeDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<LessonSection> LessonSections { get; set; }
    }
}
