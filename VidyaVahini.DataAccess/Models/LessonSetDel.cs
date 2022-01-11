using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.DataAccess.Models
{
    public partial class LessonSetDel
    {
        public LessonSetDel()
        {
            Lessons = new HashSet<Lesson>();
            Teachers = new HashSet<DeleteNotTeacher>();
        }

        public string LessonSetId { get; set; }
        public int LessonSetOrder { get; set; }
        public int LevelId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Level Level { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<DeleteNotTeacher> Teachers { get; set; }

    }
}
