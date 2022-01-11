using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class MentorResponse
    {
        public int MentorResponseId { get; set; }
        public int TeacherResponseId { get; set; }
        public string MentorId { get; set; }
        public string MentorComments { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual TeacherResponse TeacherResponse { get; set; }
    }
}
