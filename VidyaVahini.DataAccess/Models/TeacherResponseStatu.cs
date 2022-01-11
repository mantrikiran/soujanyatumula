using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class TeacherResponseStatu
    {
        public int TeacherResponseStatusId { get; set; }
        public string QuestionId { get; set; }
        public string TeacherId { get; set; }
        public int ResponseState { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Question Question { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual UserAccount UpdatedByNavigation { get; set; }
    }
}
