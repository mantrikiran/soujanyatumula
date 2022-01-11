using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            QueryDatas = new HashSet<QueryData>();
            TeacherResponseStatus = new HashSet<TeacherResponseStatu>();
            UserLanguages = new HashSet<UserLanguage>();
            UserRoles = new HashSet<UserRole>();
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsRegistered { get; set; }
        public int ActivationEmailCount { get; set; }
        public int FailedLoginAttempt { get; set; }
        public DateTime? LastFailedLoginAttempt { get; set; }
        public DateTime? LockExpiry { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiry { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<QueryData> QueryDatas { get; set; }
        public virtual ICollection<TeacherResponseStatu> TeacherResponseStatus { get; set; }
        public virtual ICollection<UserLanguage> UserLanguages { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
