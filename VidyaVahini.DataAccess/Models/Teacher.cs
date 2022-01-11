using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Queries = new HashSet<Query>();
            TeacherClasses = new HashSet<TeacherClass>();
            TeacherResponseStatus = new HashSet<TeacherResponseStatu>();
            TeacherResponses = new HashSet<TeacherResponse>();
            TeacherSubjects = new HashSet<TeacherSubject>();
        }

        public string TeacherId { get; set; }
        public int SchoolId { get; set; }
        public string MentorId { get; set; }
        public string ActiveLessonSetId { get; set; }
        public bool LoadNextLessonSet { get; set; }
        public bool RedoActiveLessonSet { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual LessonSet ActiveLessonSet { get; set; }
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
