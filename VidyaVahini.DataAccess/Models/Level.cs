using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Level
    {
        public Level()
        {
            LessonSets = new HashSet<LessonSet>();
        }

        public int LevelId { get; set; }
        public string LevelCode { get; set; }
        public string LevelDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<LessonSet> LessonSets { get; set; }
    }
}
