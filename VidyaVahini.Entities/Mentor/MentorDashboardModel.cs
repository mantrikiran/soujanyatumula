using System.Collections.Generic;

namespace VidyaVahini.Entities.Mentor
{
    public class MentorDashboardModel
    {
        public int PendingSubmissionActions { get; set; }
        public int PendingQueryActions { get; set; }
        public IEnumerable<MenteeProgressModel> Mentees { get; set; }
    }
}
