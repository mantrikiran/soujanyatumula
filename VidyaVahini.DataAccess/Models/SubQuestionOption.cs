using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class SubQuestionOption
    {
        public SubQuestionOption()
        {
            SubQuestionAnswers = new HashSet<SubQuestionAnswer>();
            TeacherSubQuestionResponses = new HashSet<TeacherSubQuestionResponse>();
        }

        public int SubQuestionOptionId { get; set; }
        public int SubQuestionId { get; set; }
        public string OptionText { get; set; }
        public int OptionOrder { get; set; }
        public bool OptionHidden { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual SubQuestion SubQuestion { get; set; }
        public virtual ICollection<SubQuestionAnswer> SubQuestionAnswers { get; set; }
        public virtual ICollection<TeacherSubQuestionResponse> TeacherSubQuestionResponses { get; set; }
    }
}
