using System;

namespace VidyaVahini.Entities.UserAccount
{
    public class UserAccountData
    {
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
    }
}
