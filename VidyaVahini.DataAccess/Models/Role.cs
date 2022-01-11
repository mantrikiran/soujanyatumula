﻿using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
