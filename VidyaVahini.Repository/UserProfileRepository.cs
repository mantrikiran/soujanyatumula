using System;
using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.UserProfile;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccessRepository<UserProfile> _userProfileRepository;

        public UserProfileRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userProfileRepository = _unitOfWork.Repository<UserProfile>();
        }

        public void AddUserProfile(string userId, string email, string name, string city, int genderId, 
            string mobileNumber, int qualificationId, string otherQualification, int stateId)
        {
            var userProfile = new UserProfile
            {
                Created = DateTime.Now,
                Email = email,
                Name = name,
                UserId = userId
            };

            userProfile = UpdateUserProfile(
                userProfile: userProfile,
                city: city,
                genderId: genderId,
                mobileNumber: mobileNumber,
                qualificationId: qualificationId,
                otherQualification: otherQualification,
                stateId: stateId);

            _userProfileRepository.Add(userProfile);
        }

        public UserProfileData GetUserProfile(string userId)
        {
            var userProfile = _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.UserProfile>()
                .Find(x => string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase));

            return userProfile == null ? null : GetUserProfileData(userProfile);
        }

        public IEnumerable<UserProfileData> GetUserProfile(IEnumerable<string> userIds)
            => _userProfileRepository
            .GetAll()
            .Where(x => userIds.Any(y => string.Equals(y, x.UserId, StringComparison.OrdinalIgnoreCase)))
            .Select(x => GetUserProfileData(x));

        private UserProfileData GetUserProfileData(UserProfile userProfile)
            => new UserProfileData
            {
                City = userProfile.City,
                Email = userProfile.Email,
                GenderId = userProfile.GenderId ?? 0,
                MobileNumber = userProfile.MobileNumber,
                Name = userProfile.Name,
                OtherQualification = userProfile.OtherQualification,
                QualificationId = userProfile.QualificationId ?? 0,
                StateId = userProfile.StateId ?? 0,
                UserId = userProfile.UserId
            };

        public  void UpdateUserProfile(string userId, string city, int genderId, string mobileNumber, int qualificationId, string otherQualification, int stateId)
        {
            var userProfile = _unitOfWork.Repository<VidyaVahini.DataAccess.Models.UserProfile>().Find(x => x.UserId == userId);

            userProfile = UpdateUserProfile(
                userProfile: userProfile,
                city: city,
                genderId: genderId,
                mobileNumber: mobileNumber,
                qualificationId: qualificationId,
                otherQualification: otherQualification,
                stateId: stateId);

            _unitOfWork.Repository<VidyaVahini.DataAccess.Models.UserProfile>().Update(userProfile);
        }

        private UserProfile UpdateUserProfile(UserProfile userProfile, string city, int genderId, string mobileNumber, int qualificationId, string otherQualification, int stateId)
        {
            userProfile.City = city;
            userProfile.GenderId = genderId;
            userProfile.MobileNumber = mobileNumber;
            userProfile.QualificationId = qualificationId;
            userProfile.OtherQualification = otherQualification;
            userProfile.StateId = stateId;
            userProfile.LastUpdated = DateTime.Now;

            return userProfile;
        }
    }
}
