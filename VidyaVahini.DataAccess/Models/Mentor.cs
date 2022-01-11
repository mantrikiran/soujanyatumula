using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Mentor
    {
        public Mentor()
        {
            MentorResponses = new HashSet<MentorResponse>();
            Teachers = new HashSet<Teacher>();
        }

        public string MentorId { get; set; }
        public bool WorkingInSssvv { get; set; }
        public string SssvvvoluteerName { get; set; }
        public bool WorkingInSaiOrganization { get; set; }
        public string SaiOrganizationVoluteerName { get; set; }
        public string EnglishTeachingExperience { get; set; }
        public string Occupation { get; set; }
        public int TimeCapacity { get; set; }
        public int TeachersCapacity { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual UserAccount MentorNavigation { get; set; }
        public virtual ICollection<MentorResponse> MentorResponses { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
