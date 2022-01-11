using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class TeacherSubject
    {
        public int TeacherSubjectId { get; set; }
        public string TeacherId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
