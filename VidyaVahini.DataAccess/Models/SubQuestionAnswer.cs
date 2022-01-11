using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class SubQuestionAnswer
    {
        public int SubQuestionAnswerId { get; set; }
        public int SubQuestionId { get; set; }
        public int SubQuestionOptionId { get; set; }
        public int? AnswerOrder { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual SubQuestion SubQuestion { get; set; }
        public virtual SubQuestionOption SubQuestionOption { get; set; }
    }
}
