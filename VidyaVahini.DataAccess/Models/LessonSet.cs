using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class LessonSet
    {
        public LessonSet()
        {
            Lessons = new HashSet<Lesson>();
            Teachers = new HashSet<Teacher>();           
        }
               
        public string LessonSetId { get; set; }       
        public int LessonSetOrder { get; set; }
        public int LevelId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Level Level { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; } 
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
