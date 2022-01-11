using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.DataAccess.Models
{
    public partial class DeleteNotTeacher
    {
        public DeleteNotTeacher()
        {
            Queries = new HashSet<Query>();
            TeacherClasses = new HashSet<TeacherClass>();
            TeacherResponseStatus = new HashSet<TeacherResponseStatu>();
            TeacherResponses = new HashSet<TeacherResponse>();
            TeacherSubjects = new HashSet<TeacherSubject>();
        }

        public virtual Mentor Mentor { get; set; }
        public virtual School School { get; set; }
        public virtual UserAccount TeacherNavigation { get; set; }
        public virtual ICollection<Query> Queries { get; set; }
        public virtual ICollection<TeacherClass> TeacherClasses { get; set; }
        public virtual ICollection<TeacherResponseStatu> TeacherResponseStatus { get; set; }
        public virtual ICollection<TeacherResponse> TeacherResponses { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
