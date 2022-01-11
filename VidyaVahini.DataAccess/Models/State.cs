using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class State
    {
        public State()
        {
            Schools = new HashSet<School>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
