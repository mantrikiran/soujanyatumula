using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Question
    {
        public Question()
        {
            Queries = new HashSet<Query>();
            QuestionHints = new HashSet<QuestionHint>();
            QuestionMedias = new HashSet<QuestionMedia>();           
            SubQuestions = new HashSet<SubQuestion>();
            TeacherResponseStatus = new HashSet<TeacherResponseStatu>();
            TeacherResponses = new HashSet<TeacherResponse>();
        }

        public string QuestionId { get; set; }
        public string LessonSectionId { get; set; }
        public int QuestionOrder { get; set; }
        public string QuestionText { get; set; }
        public string MediaId { get; set; }
        public int? RecommendedAttempts { get; set; }
        public int? SecondaryAttempts { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual LessonSection LessonSection { get; set; }      
        public virtual Media Media { get; set; }
        public virtual ICollection<Query> Queries { get; set; }
        public virtual ICollection<QuestionHint> QuestionHints { get; set; }
        public virtual ICollection<QuestionMedia> QuestionMedias { get; set; }
        public virtual ICollection<SubQuestion> SubQuestions { get; set; }
        public virtual ICollection<TeacherResponseStatu> TeacherResponseStatus { get; set; }
        public virtual ICollection<TeacherResponse> TeacherResponses { get; set; }
    }
}
