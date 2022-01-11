using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class TeacherResponse
    {
        public TeacherResponse()
        {
            TeacherSubQuestionResponses = new HashSet<TeacherSubQuestionResponse>();
        }
        public int? Score { get; set; }
        public int TeacherResponseId { get; set; }
        public string QuestionId { get; set; }
        public string TeacherId { get; set; }
        public string ResponseText { get; set; }
        public string MediaId { get; set; }
        public bool IsRevised { get; set; }
        public int? Attempts { get; set; }       
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Media Media { get; set; }
        public virtual Question Question { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual MentorResponse MentorResponse { get; set; }
        public virtual ICollection<TeacherSubQuestionResponse> TeacherSubQuestionResponses { get; set; }
    }
}
