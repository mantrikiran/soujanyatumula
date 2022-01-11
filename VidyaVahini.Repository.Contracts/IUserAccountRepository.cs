using System.Collections.Generic;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Mentor;
using VidyaVahini.Entities.Role;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Entities.UserProfile;

namespace VidyaVahini.Repository.Contracts
{
    public interface IUserAccountRepository
    {
        /// <summary>
        /// Adds a new user account
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="userName">Username</param>
        /// <param name="password">Name</param>
        /// <returns>User account data</returns>
        string AddUserAccount(string userId, string userName, string password);

        /// <summary>
        /// Adds a new user account
        /// </summary>
        /// <param name="userAccount">User Account Details</param>
        /// <param name="userProfile">User Profile Details</param>
        /// <param name="userRoles">User Roles</param>
        /// <param name="userLanguages">User Languages</param>
        /// <param name="teacher">Teacher Details</param>
        /// <param name="mentor">Mentor Details</param>
        void AddUserAccount(
            UserAccountData userAccount,
            UserProfileData userProfile,
            IEnumerable<UserRoleData> userRoles,
            IEnumerable<UserLanguageData> userLanguages,
            TeacherData teacher,
            MentorData mentor);

        /// <summary>
        /// Gets the user account details
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>User account details</returns>
        UserAccountData GetUserAccount(string userId);
        MetorData GetMentorProfile(string teacherId);

        /// <summary>
        /// Gets the user account details
        /// </summary>
        /// <param name="userIds">User Ids</param>
        /// <returns>User account details</returns>
        IEnumerable<UserAccountData> GetUserAccount(IEnumerable<string> userIds);

        /// <summary>
        /// Gets the user account details
        /// </summary>
        /// <param name="userId">Username</param>
        /// <returns>User account details</returns>
        UserAccountData GetUserAccountByUsername(string username);

        /// <summary>
        /// Updates password of the user account
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        string UpdateUserAccount(string userId, string password);

        /// <summary>
        /// Validates the token, disables it and marks the user account as active
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>success/failure</returns>
        bool ActivateAccount(string token);     

        /// <summary>
        /// Activates a user account
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>User profile details</returns>
        UserProfileData ActivateAccountByUserId(string userId);

        /// <summary>
        /// Validates the user account credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User details</returns>
        UserModel AuthenticateUser(string username, string password);

        /// <summary>
        /// Updates the user account with a new token and token expiry
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="tokenExpiry">Token Expiry</param>
        /// <returns>User details</returns>
        ForgotPasswordModel ForgotPassword(string username, int tokenExpiry);

        /// <summary>
        /// Updates the password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">New Password</param>
        /// <returns>success/failure</returns>
        bool ChangePassword(string username, string password);

        /// <summary>
        /// Validates and disables the token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>User basic details</returns>
        UserBasicDetailsModel ValidateToken(string token);
    }
}
