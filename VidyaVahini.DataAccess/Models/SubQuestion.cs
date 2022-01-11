using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class SubQuestion
    {
        public SubQuestion()
        {

            SubQuestionAnswers = new HashSet<SubQuestionAnswer>();
            SubQuestionOptions = new HashSet<SubQuestionOption>();
            TeacherSubQuestionResponses = new HashSet<TeacherSubQuestionResponse>();
        }

        public int SubQuestionId { get; set; }
        public string QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int QuestionOrder { get; set; }
        public string QuestionText { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Question Question { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual ICollection<SubQuestionAnswer> SubQuestionAnswers { get; set; }
        public virtual ICollection<SubQuestionOption> SubQuestionOptions { get; set; }
        public virtual ICollection<TeacherSubQuestionResponse> TeacherSubQuestionResponses { get; set; }
    }
}
