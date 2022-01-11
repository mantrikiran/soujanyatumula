using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VidyaVahini.Core.Constant;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Mentor;
using VidyaVahini.Entities.Role;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Entities.UserProfile;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccessRepository<UserAccount> _userAccount;
        private readonly IDataAccessRepository<UserProfile> _userProfile;
        private readonly IDataAccessRepository<Teacher> _teacher;
        private readonly IDataAccessRepository<Mentor> _mentor;


        public UserAccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userAccount = _unitOfWork.Repository<UserAccount>();
            _userProfile = _unitOfWork.Repository<UserProfile>();
            _teacher = _unitOfWork.Repository<Teacher>();
            _mentor = _unitOfWork.Repository<Mentor>();
        }

        public string AddUserAccount(string userId, string userName, string password)
        {
            var userAccount = new UserAccount
            {
                ActivationEmailCount = 0,
                Created = DateTime.Now,
                FailedLoginAttempt = 0,
                IsActive = false,
                UserId = userId,
                Username = userName
            };

            userAccount = UpdateUserAccount(
                userAccount: userAccount,
                password: password,
                isRegistered: true,
                addToken: true);

            _userAccount.Add(userAccount);

            return userAccount.Token;
        }

        public void AddUserAccount(
            UserAccountData userAccount, 
            UserProfileData userProfile, 
            IEnumerable<UserRoleData> userRoles, 
            IEnumerable<UserLanguageData> userLanguages, 
            TeacherData teacher, 
            MentorData mentor)
        {
            if (_userAccount.Exist(x => string.Equals(x.UserId, userAccount.UserId, StringComparison.OrdinalIgnoreCase)))
            {

            }
            else
            {
                _userAccount.Add(new UserAccount
                {
                    ActivationEmailCount = userAccount.ActivationEmailCount,
                    Created = DateTime.Now,
                    FailedLoginAttempt = userAccount.FailedLoginAttempt,
                    IsActive = userAccount.IsActive,
                    IsRegistered = userAccount.IsRegistered,
                    LastFailedLoginAttempt = userAccount.LastFailedLoginAttempt,
                    LastUpdated = DateTime.Now,
                    LockExpiry = userAccount.LockExpiry,
                    Mentor = mentor == null ? null : GetMentor(mentor),
                    PasswordHash = userAccount.PasswordHash,
                    PasswordSalt = userAccount.PasswordSalt,
                    Teacher = teacher == null ? null : GetTeacher(teacher),
                    Token = userAccount.Token,
                    TokenExpiry = userAccount.TokenExpiry,
                    UserId = userAccount.UserId,
                    UserLanguages = userLanguages == null ? null : GetUserLanguages(userLanguages),
                    Username = userAccount.Username,
                    UserProfile = userProfile == null ? null : GetUserProfile(userProfile),
                    UserRoles = userRoles == null ? null : GetUserRoles(userRoles)
                });
            }

            _unitOfWork.Commit();
        }

        private UserProfile GetUserProfile(UserProfileData userProfile)
            => new UserProfile
            {
                City = userProfile.City,
                Created = DateTime.Now,
                Email = userProfile.Email,
                GenderId = userProfile.GenderId,
                LastUpdated = DateTime.Now,
                MobileNumber = userProfile.MobileNumber,
                Name = userProfile.Name,
                OtherQualification = userProfile.OtherQualification,
                QualificationId = userProfile.QualificationId,
                StateId = userProfile.StateId,
                countryid = userProfile.countryid,
                UserId = userProfile.UserId
            };

        private UserProfileData GetUserProfile(UserProfile userProfile)
            => new UserProfileData
            {
                City = userProfile.City,
                Email = userProfile.Email,
                GenderId = userProfile.GenderId,
                MobileNumber = userProfile.MobileNumber,
                Name = userProfile.Name,
                OtherQualification = userProfile.OtherQualification,
                QualificationId = userProfile.QualificationId,
                StateId = userProfile.StateId,
                countryid=userProfile.countryid,
                UserId = userProfile.UserId
            };

        private List<UserLanguage> GetUserLanguages(IEnumerable<UserLanguageData> userLanguages)
            => userLanguages
            .Select(x => new UserLanguage
            {
                Created = DateTime.Now,
                LanguageId = x.LanguageId,
                LastUpdated = DateTime.Now,
                UserId = x.UserId
            }).ToList();

        private List<UserRole> GetUserRoles(IEnumerable<UserRoleData> userRoles)
            => userRoles
            .Select(x => new UserRole
            {
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RoleId = x.RoleId,
                UserId = x.UserId
            }).ToList();

        private Teacher GetTeacher(TeacherData teacher)
            => new Teacher
            {
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                SchoolId = teacher.SchoolId,
                TeacherId = teacher.UserId
            };

        private Mentor GetMentor(MentorData mentor)
            => new Mentor
            {
                Created = DateTime.Now,
                EnglishTeachingExperience = mentor.EnglishTeachingExperience,
                LastUpdated = DateTime.Now,
                Occupation = mentor.Occupation,
                SaiOrganizationVoluteerName = mentor.SaiVolunteer,
                SssvvvoluteerName = mentor.SssvvVolunteer,
                TeachersCapacity = mentor.TeacherCapacity,
                TimeCapacity = mentor.TimeCapacity,
                MentorId = mentor.UserId,
                WorkingInSaiOrganization = mentor.WorkingInSaiOrganization,
                WorkingInSssvv = mentor.WorkingInSssvv
            };

        public UserAccountData GetUserAccount(string userId)
        {
            var userAccount = _userAccount
                .Find(x => string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase));

            return GetUserAccountData(userAccount);
        }

        public IEnumerable<UserAccountData> GetUserAccount(IEnumerable<string> userIds)
            => _userAccount
            .GetAll()
            .Where(x => userIds.Any(y => string.Equals(y, x.UserId, StringComparison.OrdinalIgnoreCase)))
            .Select(x => GetUserAccountData(x));

        public UserAccountData GetUserAccountByUsername(string username)
        {
            var userAccount = _userAccount
                .Find(x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase));

            return GetUserAccountData(userAccount);
        }

        private UserAccountData GetUserAccountData(UserAccount userAccount)
            => userAccount == null ? null : new UserAccountData
            {
                ActivationEmailCount = userAccount.ActivationEmailCount,
                FailedLoginAttempt = userAccount.FailedLoginAttempt,
                IsActive = userAccount.IsActive,
                IsRegistered = userAccount.IsRegistered,
                LastFailedLoginAttempt = userAccount.LastFailedLoginAttempt ?? DateTime.MinValue,
                LockExpiry = userAccount.LockExpiry ?? DateTime.MinValue,
                PasswordHash = userAccount.PasswordHash,
                PasswordSalt = userAccount.PasswordSalt,
                Token = userAccount.Token,
                TokenExpiry = userAccount.TokenExpiry ?? DateTime.MinValue,
                UserId = userAccount.UserId,
                Username = userAccount.Username
            };

        public string UpdateUserAccount(string userId, string password)
        {
            var userAccount = _userAccount
                .Find(x => x.UserId == userId);

            userAccount = UpdateUserAccount(
                userAccount: userAccount,
                password: password,
                isRegistered: true,
                addToken: true);

            _userAccount.Update(userAccount);

            return userAccount.Token;
        }

        public bool ActivateAccount(string token)
        {
            var userAccount = _userAccount.Find(x => x.Token == token);

            if (userAccount == null)
                return false;

            DeactivateToken(userAccount);

            userAccount.IsActive = true;
            
            _userAccount.Update(userAccount);

            return _unitOfWork.Commit() > 0;
        }

        public UserProfileData ActivateAccountByUserId(string userId)
        {
            var userAccount = _userAccount
                .Filter(filter: x => string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase), 
                    includeProperties: Constants.UserProfileProperty)
                .FirstOrDefault();

            if (userAccount == null)
                return null;

            userAccount.IsActive = true;
            userAccount.Token = null;
            userAccount.TokenExpiry = null;
            userAccount.LastUpdated = DateTime.Now;

            _userAccount.Update(userAccount);

            _unitOfWork.Commit();

            return userAccount.UserProfile == null ? null : GetUserProfile(userAccount.UserProfile);
        }

        public UserModel AuthenticateUser(string username, string password)
        {

            var includeProperties = new StringBuilder(Constants.UserProfileProperty);
            includeProperties.Append($",{Constants.TeacherProperty}");
            includeProperties.Append($",{Constants.UserRolesProperty}");
            includeProperties.Append($",{Constants.UserLanguagesProperty}");
            includeProperties.Append($",{Constants.UserRolesProperty}.{Constants.RoleProperty}");
            includeProperties.Append($",{Constants.UserLanguagesProperty}.{Constants.LanguageProperty}");           

            var userAccount = _userAccount
                .Filter(filter: x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (userAccount == null || !userAccount.IsActive)
                return null;

            var byteValue = Encoding.UTF8.GetBytes(password + userAccount.PasswordSalt);

            if (userAccount.PasswordHash != Convert.ToBase64String(byteValue))
                return null;

            return new UserModel
            {
                Name = userAccount.UserProfile.Name,
                UserId = userAccount.UserId,
                Email = userAccount.UserProfile.Email,
                UserRoles = userAccount.UserRoles.Select(x => new RoleData
                {
                    Id = x.RoleId,
                    Name = x.Role.RoleDescription
                }),
                UserData = new UserDataModel
                {
                    ActiveLessonSetId = userAccount.Teacher?.ActiveLessonSetId,
                    UserLanguages = userAccount.UserLanguages.Select(x => new LanguageData
                    {
                        Id = x.LanguageId,
                        Name = x.Language.LanguageName
                    })

                }
            }; 
        
        }

        public MetorData GetMentorProfile(string teacherid)
        {
            var includeProperties = new StringBuilder(Constants.UserProfileProperty);
            includeProperties.Append($",{Constants.TeacherProperty}");

            var includeProperties1 = new StringBuilder();

            includeProperties1.Append($",{Constants.TeacherNavigationProperty}");
            includeProperties1.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}");



            var userAccount = _userAccount
                .Filter(filter: x => string.Equals(x.Username, teacherid, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherNavigation.Teacher.TeacherId,userAccount.UserId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties1.ToString())
                .FirstOrDefault();


            var includeProperties2 = new StringBuilder();
            includeProperties2.Append(Constants.MentorNavigationProperty);
            includeProperties2.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}");
            includeProperties2.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}.{Constants.StateProperty}");

            var mentor = _mentor
           .Filter(filter: x => string.Equals(x.MentorId, teacher.MentorId, StringComparison.OrdinalIgnoreCase),
               includeProperties: includeProperties2.ToString())
           .FirstOrDefault();

            if (mentor != null)
            {
                return new MetorData
                {
                    Name = mentor.MentorNavigation?.UserProfile?.Name,
                    MobileNumber = mentor.MentorNavigation?.UserProfile?.MobileNumber,
                    State = mentor.MentorNavigation?.UserProfile?.State?.StateName

                };

              
            }

            else
            {
                return new MetorData
                {
                    Name = "",
                    MobileNumber = "",
                    State = ""
                };

               
            }

        }




        private UserAccount UpdateUserAccount(UserAccount userAccount, string password, bool isRegistered, bool addToken)
        {
            userAccount.PasswordSalt = Guid.NewGuid().ToString();
            var byteValue = Encoding.UTF8.GetBytes(password + userAccount.PasswordSalt);
            userAccount.PasswordHash = Convert.ToBase64String(byteValue);

            if (addToken)
            {
                userAccount.Token = Guid.NewGuid().ToString();
                userAccount.TokenExpiry = DateTime.Now.AddDays(Constants.TokenExpiry);
                userAccount.ActivationEmailCount++;
            }

            if (isRegistered)
            {
                userAccount.IsRegistered = isRegistered;
            }
            userAccount.LastUpdated = DateTime.Now;

            return userAccount;
        }

        public string GenerateToken(string userId, int tokenExpiry)
        {
            var userAccount = _userAccount
                .Find(x => string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase));

            userAccount.Token = Guid.NewGuid().ToString();
            userAccount.TokenExpiry = DateTime.Now.AddDays(tokenExpiry);
            userAccount.LastUpdated = DateTime.Now;

            _userAccount.Update(userAccount);

            return userAccount.Token;
        }

        public ForgotPasswordModel ForgotPassword(string username, int tokenExpiry)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.UserProfileProperty);

            var userAccount = _userAccount
                .Filter(filter: x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase) && x.IsRegistered,
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (userAccount == null)
                return null;

            userAccount.Token = GenerateToken(userAccount.UserId, tokenExpiry);

            _unitOfWork.Commit();

            return new ForgotPasswordModel
            {
                Email = userAccount.UserProfile.Email,
                Name = userAccount.UserProfile.Name,
                Token = userAccount.Token
            };
        }

        public bool ChangePassword(string username, string password)
        {
            var userAccount = _userAccount
                .Find(x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase));

            if (userAccount == null)
                return false;

            UpdateUserAccount(userAccount, password, false, false);

            return _unitOfWork.Commit() > 0;
        }


        public UserBasicDetailsModel ValidateToken(string token)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.UserProfileProperty);

            var userAccount = _userAccount
                .Filter(filter: x => x.Token == token,
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (userAccount == null)
                return null;

            _unitOfWork.Commit();

            return new UserBasicDetailsModel
            {
                Username = userAccount.Username,
                Name = userAccount.UserProfile.Name
            };
        }

        private UserAccount DeactivateToken(UserAccount userAccount)
        {
            userAccount.Token = null;
            userAccount.TokenExpiry = null;
            userAccount.LastUpdated = DateTime.Now;

            _userAccount.Update(userAccount);

            return userAccount;
        }
    }
}
