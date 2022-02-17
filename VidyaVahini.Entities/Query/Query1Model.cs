using System;
using System.Collections.Generic;
using System.Text;
using VidyaVahini.Entities.Teacher;

namespace VidyaVahini.Entities.Query
{
   public  class Query1Model
    {
        public string QueryId { get; set; }
        public int QueryStatus { get; set; }
        public string LessonId { get; set; }
        public string LessonCode { get; set; }
        public string LessonNumber { get; set; }
        public string LessonSectionId { get; set; }
        public string LessonSectionCode { get; set; }
        public string LessonSectionName { get; set; }
        public string LessonSetId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionId { get; set; }
        public int SubQuestionId { get; set; }
        public string SubQuestionText { get; set; }
        public IEnumerable<TeacherQuestionMedia> QueryQuestionMedia { get; set; }
        public IEnumerable<QResponse> QResponses { get; set; }

    }

    public class QResponse
    {
        public IEnumerable<QueryDataResponse> Responses { get; set; }
        
    }
    public class QueryDataResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string QueryText { get; set; }
        public string FileName { get; set; }
        public DateTime QueryCreatedDate { get; set; }
        public int QueryDataId { get; set; }
        public string MediaStream { get; set; }
    }

    }
