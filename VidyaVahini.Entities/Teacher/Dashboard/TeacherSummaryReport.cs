using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher.Dashboard
{
   public  class TeacherSummaryReport
    {
        //public string LessonId { get; set; }
        public IEnumerable<TeacherSummaryLessonReport> Data { get; set; }
    }
}
