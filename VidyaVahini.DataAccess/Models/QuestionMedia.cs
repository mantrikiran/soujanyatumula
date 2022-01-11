using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class QuestionMedia
    {
        public int QuestionMediaId { get; set; }
        public int LanguageId { get; set; }
        public string QuestionId { get; set; }
        public string MediaId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Language Language { get; set; }
        public virtual Media Media { get; set; }
        public virtual Question Question { get; set; }

    }
}
