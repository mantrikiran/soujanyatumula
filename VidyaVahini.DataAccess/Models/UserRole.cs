using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
