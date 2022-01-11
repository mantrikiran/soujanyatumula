using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class QuestionHint
    {
        public int QuestionHintId { get; set; }
        public string QuestionId { get; set; }
        public string HintText { get; set; }
        public string MediaId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Media Media { get; set; }
        public virtual Question Question { get; set; }
    }
}
