using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Qualification
    {
        public Qualification()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int QualificationId { get; set; }
        public string QualificationDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
