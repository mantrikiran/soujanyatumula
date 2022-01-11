using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher.Dashboard
{
   public class TeacherSummaryLessonReport
    {

        public string LessonSet { get; set; }
        public IEnumerable<TeacherLessonsSummary> Lessons { get; set; }
    }
    public class TeacherLessonsSummary
    {
        public int LessonNumber { get; set; }
        public int TOBEDONE { get; set; }
        public int ONGOING { get; set; }
        public int SUBMITTEDFORREVEW { get; set; }
        public int COMPLETEDANDAPPROVED { get; set; }
    }
    }
