using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher.Dashboard
{
  public class TeacherLessonReport
    {
        //public IEnumerable<TeacherReportModel> TeacherReport { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ContactNumber { get; set; }
        public string MentorName { get; set; }    
        // public TeacherDashboardQueryModel PendingQueries { get; set; }
        public IEnumerable<TeacherReportLessonSet> LessonSet { get; set; }
        public IEnumerable<TeacherReportLessonStatusModel> LessonStatuses { get; set; }

    }

    public class TeacherReportLessonSet
    {
        public int LessonSet { get; set; }
        public IEnumerable<string> TOBEDONE { get; set; }
        public IEnumerable<string> ONGOING { get; set; }
        public IEnumerable<string> SUBMITTEDFORREVIEW { get; set; }
        public IEnumerable<string> COMPLETEDANDAPPROVED { get; set; }
    }
        public class TeacherReportLessonStatusModel
    {
        public string LessonId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonName { get; set; }
        public string LessonStatus { get; set; }
        public string LessonCode { get; set; }
        public IEnumerable<TeacherReportLessonSectionStatusModel> LessonSectionStatuses { get; set; }
    }

    public class TeacherReportLessonSectionStatusModel
    {
        public string LessonSectionId { get; set; }
        public string LessonSectionCode { get; set; }
        public string LessonSectionStatus { get; set; }
        public string LessonSectionName { get; set; }
        public IEnumerable<TeacherReportQuestionStatusModel> LessonQuestionStatuses { get; set; }
    }

    public class TeacherReportQuestionStatusModel
    {
        public string QuestionId { get; set; }
        public string QuestionStatus { get; set; }
    }

}
