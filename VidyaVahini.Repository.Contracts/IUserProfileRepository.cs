using System.Collections.Generic;
using VidyaVahini.Entities.UserProfile;

namespace VidyaVahini.Repository.Contracts
{
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Gets user profile information
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User profile information</returns>
        UserProfileData GetUserProfile(string userId);

        /// <summary>
        /// Gets user profile information
        /// </summary>
        /// <param name="userIds">User ids</param>
        /// <returns>User profile information</returns>
        IEnumerable<UserProfileData> GetUserProfile(IEnumerable<string> userIds);

        /// <summary>
        /// Adds a new user profile
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="email">Email</param>
        /// <param name="name">Name</param>
        /// <param name="city">City</param>
        /// <param name="genderId">Gender Id</param>
        /// <param name="mobileNumber">Mobile Number</param>
        /// <param name="qualificationId">Qualification Id</param>
        /// <param name="otherQualification">Other Qialification</param>
        /// <param name="stateId">State Id</param>
        void AddUserProfile(string userId, string email, string name, string city, int genderId,
            string mobileNumber, int qualificationId, string otherQualification, int stateId);

        /// <summary>
        /// Updates a user profile
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="city">City</param>
        /// <param name="genderId">Gender Id</param>
        /// <param name="mobileNumber">Mobile Number</param>
        /// <param name="qualificationId">Qualification Id</param>
        /// <param name="otherQualification">Other Qualification</param>
        /// <param name="stateId">State Id</param>
        void UpdateUserProfile(string userId, string city, int genderId, string mobileNumber, 
            int qualificationId, string otherQualification, int stateId);       
    }
}
