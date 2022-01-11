using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Enum;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Infrastructure.Exception;
using VidyaVahini.Repository.DataAccess;
using VidyaVahini.Repository.Error;
using VidyaVahini.Repository.School;
using VidyaVahini.Repository.Teacher;
using VidyaVahini.Repository.TeacherClass;
using VidyaVahini.Repository.TeacherSubject;
using VidyaVahini.Repository.UserAccount;
using VidyaVahini.Repository.UserLanguage;
using VidyaVahini.Repository.UserProfile;

namespace VidyaVahini.Services.Teacher
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorRepository _errorRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserLanguageRepository _userLanguageRepository;
        private readonly ITeacherClassRepository _teacherClassRepository;
        private readonly ITeacherSubjectRepository _teacherSubjectRepository;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IErrorRepository errorRepository,
            ISchoolRepository schoolRepository, 
            ITeacherRepository teacherRepository,
            IUserAccountRepository userAccountRepository,
            IUserProfileRepository userProfileRepository,
            IUserLanguageRepository userLanguageRepository,
            ITeacherClassRepository teacherClassRepository,
            ITeacherSubjectRepository teacherSubjectRepository)
        {
            _unitOfWork = unitOfWork;
            _errorRepository = errorRepository;
            _schoolRepository = schoolRepository;
            _teacherRepository = teacherRepository;
            _userAccountRepository = userAccountRepository;
            _userProfileRepository = userProfileRepository;
            _userLanguageRepository = userLanguageRepository;
            _teacherClassRepository = teacherClassRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
        }

        public FindTeacherModel FindTeacherProfile(string email)
        {
            var result = new FindTeacherModel();

            var teacher = _teacherRepository.GetTeacher(email);

            if (teacher == null)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TeacherNotFound));

            var userAccount = _userAccountRepository.GetUserAccount(teacher.UserId);
            result.UserId = userAccount.UserId;
            result.RegistrationDataAvailable = userAccount.IsRegistered;
            result.MaxResendActivationEmailLimitReached = userAccount.ActivationEmailCount >= Constants.MaxResendActivationEmailLimit;

            var school = _schoolRepository.GetSchoolData(teacher.SchoolId);
            result.School = new Entities.School.BaseSchoolData
            {
                Id = school.Id,
                Name = school.Name
            };

            var userProfile = _userProfileRepository.GetUserProfile(userAccount.UserId);
            result.Teacher = new BaseTeacherData
            {
                Id = teacher.TeacherId,
                Name = userProfile.Name
            };

            return result;
        }

        public string RegisterTeacher(RegisterTeacherCommand registerTeacherCommand)
        {
            _userProfileRepository.UpdateUserProfile(
                registerTeacherCommand.UserId,
                registerTeacherCommand.City,
                registerTeacherCommand.GenderId,
                registerTeacherCommand.ContactNumber,
                registerTeacherCommand.QualificationId,
                registerTeacherCommand.OtherQualification,
                registerTeacherCommand.StateId);

            _userLanguageRepository.UpdateUserLanguages(
                registerTeacherCommand.UserId,
                registerTeacherCommand.Languages,
                true);

            _teacherSubjectRepository.UpdateTeacherSubjects(
                registerTeacherCommand.TeacherId,
                registerTeacherCommand.Subjects,
                true);

            _teacherClassRepository.UpdateTeacherClasses(
                registerTeacherCommand.TeacherId,
                registerTeacherCommand.Classes,
                true);

            var token = _userAccountRepository.UpdateUserAccount(
                registerTeacherCommand.UserId,
                registerTeacherCommand.Password);

            _unitOfWork.Commit();

            return token;
        }
    }
}
