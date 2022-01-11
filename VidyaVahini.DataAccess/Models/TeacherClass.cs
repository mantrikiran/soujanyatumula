using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class TeacherClass
    {
        public int TeacherClassId { get; set; }
        public string TeacherId { get; set; }
        public int ClassId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Class Class { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
