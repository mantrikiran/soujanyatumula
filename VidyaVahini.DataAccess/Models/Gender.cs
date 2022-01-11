using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Gender
    {
        public Gender()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int GenderId { get; set; }
        public string GenderCode { get; set; }
        public string GenderDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
