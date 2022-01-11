using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Enum;
using VidyaVahini.Core.Utilities;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Mentor;
using VidyaVahini.Entities.Notification;
using VidyaVahini.Entities.Role;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Entities.UserProfile;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Infrastructure.Exception;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class MentorService : IMentorService
    {
        private readonly ICache _cache;
        private readonly IConfiguration _configuration;
        private readonly IErrorRepository _errorRepository;
        private readonly IMentorRepository _mentorRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly INotificationRepository _notificationRepository;

        public MentorService(
            ICache cache,
            IConfiguration configuration,
            IErrorRepository errorRepository,
            IMentorRepository mentorRepository,
            IUserAccountRepository userAccountRepository,
             IUserProfileRepository userProfileRepository,
            INotificationRepository notificationRepository)
        {
            _cache = cache;
            _configuration = configuration;
            _errorRepository = errorRepository;
            _mentorRepository = mentorRepository;
            _userAccountRepository = userAccountRepository;
            _userProfileRepository = userProfileRepository;
            _notificationRepository = notificationRepository;
        }

        public void ActivateMentorAccount(ActivateCommand activate)
        {
            var userProfile = _userAccountRepository.ActivateAccountByUserId(activate.UserId);

            if(userProfile == null)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.GeneralErrorMessage));

            if (!_notificationRepository.SendEmail(new Email
            {
                Replacements = new Dictionary<string, string>
                {
                    { Constants.NameReplacement, userProfile.Name }
                },
                Subject = Constants.MentorAccountActivationEmailSubject,
                Template = Constants.MentorAccountActivationEmailTemplate,
                To = new List<string> { userProfile.Email }
            }))
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.ErrorSendingNotification));
        }

        public void DeleteMentorAccount(DeleteCommand delete)
            => _mentorRepository.DeleteMentor(delete.UserId);

        public MentorBasicDetailsModel GetAllMentor()
            => new MentorBasicDetailsModel
            {
                Mentors = _mentorRepository.GetAllMentor()
            };

        public MentorModel GetMentor(string userId)
            => _mentorRepository.GetMentor(userId);

        public void RegisterMentor(MentorCommand mentor)
        {
            if (_mentorRepository.GetMentorByUsername(mentor.Email) != null)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.EmailAlreadyRegistered));

          if(_mentorRepository.GetUser(mentor.ContactNumber)==false)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.WithSameContactNumberAlreadyRegistered));

            var userId = Guid.NewGuid().ToString();

            _userAccountRepository.AddUserAccount(
                userAccount: GetUserAccountData(userId, Guid.NewGuid().ToString(), mentor),
                userProfile: GetUserProfileData(userId, mentor),
                userLanguages: GetUserLanguageData(userId, mentor),
                userRoles: GetUserRoleData(userId),
                mentor: GetMentorData(userId, mentor),
                teacher: null);

            if(!_notificationRepository.SendEmail(new Email
            {
                Replacements = new Dictionary<string, string>
                {
                    { Constants.NameReplacement, mentor.Name },
                    { Constants.EmailReplacement, mentor.Email }
                },
                Subject = Constants.NewMentorRegistrationEmailSubject,
                Template = Constants.NewMentorRegistrationEmailTemplate,
                To = _configuration["Notification:CoreTeamEmail"]
                .Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
            }))
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.ErrorSendingNotification));
        }

        public PreferredMentorModel GetAvailableMentors(int teacherGenderId, int teacherLanguageId, int teacherStateId)
        {
            var availableMentors = _mentorRepository.GetAvailableMentors(teacherGenderId, teacherLanguageId, teacherStateId);

            if (!availableMentors.PreferredMentors.Any() && !availableMentors.OtherMentors.Any())
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoMentorAvailable));
            }

            return availableMentors;
        }

        private UserAccountData GetUserAccountData(string userId, string passwordSalt, MentorCommand mentor)
            => new UserAccountData
            {
                ActivationEmailCount = 0,
                FailedLoginAttempt = 0,
                IsActive = false,
                IsRegistered = true,
                LastFailedLoginAttempt = null,
                LockExpiry = null,
                PasswordHash = Util.GetPasswordHash(passwordSalt, mentor.Password),
                PasswordSalt = passwordSalt,
                Token = null,
                TokenExpiry = null,
                UserId = userId,
                Username = mentor.Email
            };

        private UserProfileData GetUserProfileData(string userId, MentorCommand mentor)
            => new UserProfileData
            {
                City = mentor.City,
                Email = mentor.Email,
                GenderId = mentor.GenderId,
                MobileNumber = mentor.ContactNumber,
                Name = mentor.Name,
                OtherQualification = mentor.OtherQualification,
                QualificationId = mentor.QualificationId,
                StateId = mentor.StateId,
                countryid=mentor.countryid,
                UserId = userId
            };

        private IEnumerable<UserLanguageData> GetUserLanguageData(string userId, MentorCommand mentor)
            => mentor
                .Languages
                .Select(x => new UserLanguageData
                {
                    UserId = userId,
                    LanguageId = x
                });

        private IEnumerable<UserRoleData> GetUserRoleData(string userId)
            => new List<UserRoleData>
            {
                new UserRoleData
                {
                    UserId = userId,
                    RoleId = (int)Enums.Role.Mentor
                }
            };

        private MentorData GetMentorData(string userId, MentorCommand mentor)
            => new MentorData
            {
                EnglishTeachingExperience = mentor.PastExperience,
                Occupation = mentor.Occupation,
                SaiVolunteer = mentor.SaiOrganizationVolunteer,
                SssvvVolunteer = mentor.SssvvVolunteer,
                TeacherCapacity = mentor.TeacherCapacity,
                TimeCapacity = mentor.TimeCapacity,
                UserId = userId,
                WorkingInSaiOrganization = mentor.WorkingInSaiOrganization,
                WorkingInSssvv = mentor.WorkingInSssvv
            };

        public MentorDashboardModel GetMentorDashboard(string userId)
        {
            var mentorDashboard = _mentorRepository.GetMentorDashboard(userId);

            if(mentorDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.MentorNotFoundOrHasNoMentees));
            }

            return mentorDashboard;
        }

        public void LoadNextLessonSet(TeacherCommand teacher)
        {
            if (!_mentorRepository.LoadNextLessonSet(teacher.TeacherId))
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TeacherNotFound));
            }

            _cache.Remove(string.Format(Constants.TeacherDashboardCache, teacher.TeacherId));
        }

        public void RedoActiveLessonSet(TeacherCommand teacher)
        {
            if (!_mentorRepository.RedoActiveLessonSet(teacher.TeacherId))
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TeacherNotFound));
            }

            _cache.Remove(string.Format(Constants.TeacherDashboardCache, teacher.TeacherId));
        }
    }
}
