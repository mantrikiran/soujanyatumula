using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class TeacherSubQuestionResponse
    {
        public int TeacherSubQuestionResponseId { get; set; }
        public int TeacherResponseId { get; set; }
        public int SubQuestionId { get; set; }
        public int SubQuestionOptionId { get; set; }
        public int AnswerOrder { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual SubQuestion SubQuestion { get; set; }
        public virtual SubQuestionOption SubQuestionOption { get; set; }
        public virtual TeacherResponse TeacherResponse { get; set; }       
    }
}
