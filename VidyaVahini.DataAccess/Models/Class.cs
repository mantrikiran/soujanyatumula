using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Class
    {
        public Class()
        {
            TeacherClasses = new HashSet<TeacherClass>();
        }

        public int ClassId { get; set; }
        public int ClassCode { get; set; }
        public string ClassDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<TeacherClass> TeacherClasses { get; set; }
    }
}
