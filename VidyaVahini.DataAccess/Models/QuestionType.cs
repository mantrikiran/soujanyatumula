using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            SubQuestions = new HashSet<SubQuestion>();
        }

        public int QuestionTypeId { get; set; }
        public string QuestionTypeDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<SubQuestion> SubQuestions { get; set; }
    }
}
