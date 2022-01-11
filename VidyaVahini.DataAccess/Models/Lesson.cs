using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            LessonSections = new HashSet<LessonSection>();
        }

        public string LessonId { get; set; }
        public string LessonSetId { get; set; }
        public string LessonCode { get; set; }
        public int LessonNumber { get; set; }
        public string LessonName { get; set; }
        public string LessonDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual LessonSet LessonSet { get; set; }
        public virtual ICollection<LessonSection> LessonSections { get; set; }
    }
}
