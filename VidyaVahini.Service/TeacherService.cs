using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Enum;
using VidyaVahini.Core.Utilities;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Notification;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.Role;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.Teacher.Dashboard;
using VidyaVahini.Entities.Teacher.Lesson;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Entities.UserProfile;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Infrastructure.Exception;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly ICache _cache;
        private readonly IErrorRepository _errorRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IDashboardRepository _dashboardRepository;
        private readonly INotificationRepository _notificationRepository;

        public TeacherService(
            ICache cache,
            IErrorRepository errorRepository,
            ITeacherRepository teacherRepository,
            IDashboardRepository dashboardRepository,
            INotificationRepository notificationRepository)
        {
            _cache = cache;
            _errorRepository = errorRepository;
            _teacherRepository = teacherRepository;
            _dashboardRepository = dashboardRepository;
            _notificationRepository = notificationRepository;
        }

        public FindTeacherModel FindTeacherProfile(string email)
        {
            var teacher = _teacherRepository.GetTeacher(email);

            if (teacher == null)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TeacherNotFound));

            return teacher;
        }

        public void RegisterTeacher(RegisterTeacherCommand registerTeacher)
        {
            if (_teacherRepository.GetUser(registerTeacher.ContactNumber) != true)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.WithSameContactNumberAlreadyRegistered));
            }
            var token = Guid.NewGuid().ToString();

            var success = _teacherRepository.RegisterTeacher(
                userAccount: GetTeacherAccountData(token, registerTeacher),
                userProfile: GetTeacherProfileData(registerTeacher),
                userLanguages: GetTeacherLanguages(registerTeacher),
                teacherClasses: GetTeacherClasses(registerTeacher),
                teacherSubjects: GetTeacherSubjects(registerTeacher));

            if (!success)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.GeneralErrorMessage));

            var sent = _notificationRepository.SendEmail(new Email
            {
                Replacements = new Dictionary<string, string>
                {
                    { Constants.TokenReplacement, token },
                    { Constants.NameReplacement, registerTeacher.Name },
                    { Constants.EmailReplacement, registerTeacher.Email },
                    { Constants.TokenExpiryReplacement, Convert.ToString(Constants.TokenExpiry) }
                },
                Subject = Constants.AccountActivationEmailSubject,
                Template = Constants.AccountActivationEmailTemplate,
                To = new List<string> { registerTeacher.Email }
            });

            if (!sent)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.ErrorSendingNotification));
        }
        public void insertSyncdate(UpdateNotificationCommand syncdateinfo)
        {          

            var success = _teacherRepository.insertSyncdate(syncdateinfo); 
          
        }
        private UserAccountData GetTeacherAccountData(string token, RegisterTeacherCommand registerTeacher)
        {
            var passwordSalt = Guid.NewGuid().ToString();

            return new UserAccountData
            {
                IsRegistered = true,
                PasswordHash = Util.GetPasswordHash(passwordSalt, registerTeacher.Password),
                PasswordSalt = passwordSalt,
                Token = token,
                TokenExpiry = DateTime.Today.AddDays(Constants.TokenExpiry),
                UserId = registerTeacher.UserId
            };
        }

        private UserProfileData GetTeacherProfileData(RegisterTeacherCommand registerTeacher)
            => new UserProfileData
            {
                City = registerTeacher.City,
                GenderId = registerTeacher.GenderId,
                MobileNumber = registerTeacher.ContactNumber,
                OtherQualification = registerTeacher.OtherQualification,
                QualificationId = registerTeacher.QualificationId,
                StateId = registerTeacher.StateId,
                countryid=registerTeacher.countryid,
                UserId = registerTeacher.UserId
            };

        private IEnumerable<UserLanguageData> GetTeacherLanguages(RegisterTeacherCommand registerTeacher)
            => registerTeacher
                .Languages
                .Select(x => new UserLanguageData
                {
                    UserId = registerTeacher.UserId,
                    LanguageId = x
                });

        private IEnumerable<TeacherClassData> GetTeacherClasses(RegisterTeacherCommand registerTeacher)
            => registerTeacher
                .Classes
                .Select(x => new TeacherClassData
                {
                    ClassId = x,
                    TeacherId = registerTeacher.UserId
                });

        private IEnumerable<TeacherSubjectData> GetTeacherSubjects(RegisterTeacherCommand registerTeacher)
            => registerTeacher
                .Subjects
                .Select(x => new TeacherSubjectData
                {
                    SubjectId = x,
                    TeacherId = registerTeacher.UserId
                });

        public bool CreateTeacherAccount(CreateTeacherAccountCommand createTeacherAccount)
        {
            if (_teacherRepository.GetTeacher(createTeacherAccount.Email) != null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.EmailAlreadyRegistered));
            }

            else
            {
                var userId = Guid.NewGuid().ToString();
                UserAccountData userAccountData = new UserAccountData
                {
                    UserId = userId,
                    Username = createTeacherAccount.Email,
                    IsActive = false,
                    IsRegistered = false,
                    ActivationEmailCount = 0,
                    FailedLoginAttempt = 0
                };

                UserProfileData userProfileData = new UserProfileData
                {
                    UserId = userId,
                    Name = createTeacherAccount.Name,
                    Email = createTeacherAccount.Email
                };

                UserRoleData userRoleData = new UserRoleData
                {
                    UserId = userId,
                    RoleId = (int)Enums.Role.Teacher
                };

                TeacherData teacherData = new TeacherData
                {
                    UserId = userId,
                    SchoolId = createTeacherAccount.SchoolId
                };

                bool teacherAccountResponse = _teacherRepository.CreateTeacherAccount(userAccountData,
                    userProfileData, userRoleData, teacherData);

                return teacherAccountResponse;
            }
        }


        public SchoolDataUploadModel AddTeachers(Stream fileStream)
        {
            List<AddTeacherData> teacherData = new List<AddTeacherData>();
            List<ErrorExcelRow> errorExcelRows = new List<ErrorExcelRow>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var reader = ExcelReaderFactory.CreateReader(fileStream))
            {
                for (int row = 1; reader.Read(); row++)
                {
                    //if (row == 1) continue;
                    var teacher = new AddTeacherData();

                    if (string.IsNullOrWhiteSpace(reader.GetString(1)))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 1,
                            ErrorMessage = "required data is missing",
                        });
                        continue;
                    }
                    else
                    {
                        teacher.Email = reader.GetValue(0)?.ToString();
                    }                    
                    if (string.IsNullOrWhiteSpace(reader.GetValue(1)?.ToString()))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 2,
                            ErrorMessage = "required data is missing",
                        });
                        continue;
                    }
                    else
                    {
                        teacher.Name = reader.GetValue(1)?.ToString();
                    }
                  
                    if (string.IsNullOrWhiteSpace(reader.GetValue(2)?.ToString()))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 3,
                            ErrorMessage = "required data is missing",
                        });
                        continue;
                    }
                    else
                    {
                        teacher.SchoolCode = reader.GetValue(2)?.ToString();
                    }
                   
                    teacherData.Add(teacher);
                }
            }

            var teachersAdded = _teacherRepository.AddTeachers(teacherData);
            return new SchoolDataUploadModel
            {
                RecordsUploaded = teachersAdded,
                DuplicateRecords = teacherData.Count - teachersAdded,
                RowsNotUploaded = errorExcelRows,
            };
        }







        public TeacherDashboardModel GetTeacherDashboard(string userId)
        {
            var teacherDashboard = _teacherRepository.GetTeacherDashboard(userId);

            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoLessonsAvailable));
            }

            foreach(var lesson in teacherDashboard.Lessons)
            {

                foreach (var section in lesson.LessonSections)
                {
                    section.SectionStatus = GetLessonSectionStatus(section.QuestionResponses.Select(x => x.ResponseState));
                }

                lesson.LessonStatus = GetLessonStatus(lesson.LessonSections.Select(x => x.SectionStatus));
            }

            return teacherDashboard;
        }

        public void UpdateNotification(NotificationCommand notificationCommand)
        {          
            _teacherRepository.UpdateNotification(notificationCommand);

        }
        public void UpdateNotifyByUserId(UpdateNotificationCommand updatenotificationCommand)
        {
            _teacherRepository.UpdateNotifyByUserId(updatenotificationCommand);

        }
        
        public getNotificationsModel GetNotifications(string userId)
            {

            getNotificationsModel notiModel = new getNotificationsModel();
             var notifications = _teacherRepository.GetNotifications(userId);

            if (notifications == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoLessonsAvailable));
            }
            notiModel.Notifications = notifications;
            return notiModel;
        }
        public GetSyncDate GetSyncDate(string userId)
        {
            GetSyncDate syncdate = new GetSyncDate();

            var notifications = _teacherRepository.GetSyncDate(userId);

            if (notifications == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoLessonsAvailable));
            }
            syncdate.Syncdate = notifications;
            return syncdate;
        }

        public GetNotifyCount GetNotificount(string userId,int flag)
        {
            GetNotifyCount notiModel = new GetNotifyCount();

            var notifications = _teacherRepository.GetNotificount(userId,flag);

            if (notifications == 0)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoLessonsAvailable));
            }
            notiModel.NotificationsCount = notifications;
            return notiModel;
        }

        public TeacherDashboardStatusModel GetTeacherDashboardStatus(string userId)
        {
            //var teacherDashboard = _cache.Get<TeacherDashboardStatusModel>(string.Format(Constants.TeacherDashboardCache, userId));


           // if (teacherDashboard == null)
            //{
              var  teacherDashboard = _teacherRepository.GetTeacherDashboardStatus(userId);

                if (teacherDashboard == null)
                {
                    throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoLessonsAvailable));
                }

                //_cache.Set(string.Format(Constants.TeacherDashboardCache, userId), teacherDashboard, TimeSpan.FromMinutes(Constants.TeacherDashboardCacheDuration));
            //}

            return teacherDashboard;
        }




        public IEnumerable<TeacherLessonReport> GetTeacherReport()
        {
            IEnumerable<TeacherLessonReport> teacherDashboard = _teacherRepository.GetTeacherReport();

            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoLessonsAvailable));
            }

            return teacherDashboard;
        }

        public IEnumerable<TeacherSummaryLessonReport> GetTeacherSummaryReport()
        {
            IEnumerable<TeacherSummaryLessonReport> teacherDashboard = _teacherRepository.GetTeacherSummaryReport();

            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoLessonsAvailable));
            }

            return teacherDashboard;
        }



        private string GetLessonSectionStatus(IEnumerable<int> responseStates)
        {
            var sectionStatus = string.Empty;

            if (!responseStates.Any() || responseStates.All(x => x == (int)Enums.ResponseStatus.TO_BE_DONE))
            {
                sectionStatus = Enums.LessonSectionStatus.TO_BE_DONE.ToString();
            }
            else if (responseStates.All(x => x == (int)Enums.ResponseStatus.COMPLETED_AND_APPROVED))
            {
                sectionStatus = Enums.LessonSectionStatus.COMPLETED_AND_APPROVED.ToString();
            }
            else if (responseStates.All(x => x == (int)Enums.ResponseStatus.SUBMITTED_FOR_REVIEW || x == (int)Enums.ResponseStatus.COMPLETED_AND_APPROVED))
            {
                sectionStatus = Enums.LessonSectionStatus.SUBMITTED_FOR_REVIEW.ToString();
            }
            else if (responseStates.Any(x => x == (int)Enums.ResponseStatus.REDO_SUBMISSION))
            {
                sectionStatus = Enums.LessonSectionStatus.REDO_SUBMISSION.ToString();
            }
            else
            {
                sectionStatus = Enums.LessonSectionStatus.COMPLETE_AND_SUBMIT.ToString();
            }

            return sectionStatus;
        }

        private string GetLessonStatus(IEnumerable<string> sectionStatus)
        {
            var lessonStatus = string.Empty;

            if (sectionStatus.All(x => x == Enums.LessonSectionStatus.TO_BE_DONE.ToString()))
            {
                lessonStatus = Enums.LessonStatus.TO_BE_DONE.ToString();
            }
            else if (sectionStatus.All(x => x == Enums.LessonSectionStatus.COMPLETED_AND_APPROVED.ToString()))
            {
                lessonStatus = Enums.LessonStatus.COMPLETED_AND_REVIEWED.ToString();
            }
            else if (sectionStatus.All(x => x == Enums.LessonSectionStatus.SUBMITTED_FOR_REVIEW.ToString()) ||
                (sectionStatus.Any(x => x == Enums.LessonSectionStatus.REDO_SUBMISSION.ToString()) && 
                !sectionStatus.All(x => x == Enums.LessonSectionStatus.REDO_SUBMISSION.ToString())))
            {
                lessonStatus = Enums.LessonStatus.SUBMITTED_FOR_REVIEW.ToString();
            }
            else
            {
                lessonStatus = Enums.LessonStatus.ONGOING.ToString();
            }

            return lessonStatus;
        }

        //public InstructionModel GetLessonSectionInstructions(string lessonSectionId, int languageId)
        //{
        //    var instructions = _teacherRepository.GetLessonSectionInstructions(lessonSectionId, languageId);

        //    if(instructions == null)
        //    {
        //        throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.GeneralErrorMessage));
        //    }

        //    return instructions;
        //}

        public SectionInstructionModel GetLessonSectionInstructions(int SectionTypeId,string RoleId, int languageId)
        {
            var instructions = _teacherRepository.GetLessonSectionInstructions(SectionTypeId, RoleId, languageId);

            if (instructions == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.GeneralErrorMessage));
            }

            return instructions;
        }
        public IEnumerable<TeacherDashboardQuestionIdsModel> GetQuestionsByLessonSectionId(string lessonSectionId)
        {
            var teacherDashboard = _teacherRepository.GetQuestions(lessonSectionId);
            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.SectionNotFound));
            }
            return teacherDashboard;
        }

        public TeacherLessonSetModel GetTeacherResponsebyLessonSetId(string lessonsetId, string userId)
        {
            var teacherDashboard = _teacherRepository.GetQuestionResponseByLessonSetId(lessonsetId, userId);
            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.SectionNotFound));
            }
            return teacherDashboard;
        }

        public TeacherLessonModel GetQuestionResponseByLessonId(string lessonId, string userId)
        {
            var teacherDashboard = _teacherRepository.GetQuestionResponseByLessonId(lessonId, userId);
            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.SectionNotFound));
            }
            return teacherDashboard;
        }

        public IEnumerable<TeacherQuestionResponse> GetQuestionResponseByLessonSectionId(string lessonSectionId, string userId)
        {
            var teacherDashboard = _teacherRepository.GetQuestionResponseByLessonSectionId(lessonSectionId, userId);
            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.SectionNotFound));
            }
            return teacherDashboard;
        }

        public TeacherDashboardQuestionsListModel GetQuestionByQuestionId(string userId, string questionId, int languageId)
        {
            var teacherDashboard = _teacherRepository.GetQuestion(userId, questionId,languageId);

            if (teacherDashboard == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.QuestionNotFound));
            }

            return teacherDashboard;
        }
        public bool UpsertQuestionResponse(QuestionResponsesCommand assignmentResponse)
        {
            string queryMediaFilePath = $@"{Constants.UserMediaFilePath}\\{assignmentResponse.UserId}\\{assignmentResponse.LessonSetId}\\{Constants.QuestionResponseFolderName}";

            bool teacherAccountResponse = _teacherRepository.UpsertQuestionResponse(assignmentResponse.UserId, assignmentResponse.LessonSectionId, 
                queryMediaFilePath, assignmentResponse.QuestionResponses,assignmentResponse.Attempts,assignmentResponse.IsPerfectScore);

            _cache.Remove(string.Format(Constants.TeacherDashboardCache, assignmentResponse.UserId));

            return teacherAccountResponse;
        }

        public void UpdateQuestionResponseState(ResponseStateCommand responseState)
        {            
            if (!responseState.Questions.All(x => Enum.IsDefined(typeof(Enums.ResponseStatus), x.State)))
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidState));
            }

            var success = _teacherRepository.AddResponseState(responseState.TeacherId, responseState.MentorId, responseState.Questions);

            if (!success)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.QuestionNotFound));
            }

            _cache.Remove(string.Format(Constants.TeacherDashboardCache, responseState.TeacherId));
        }

        public void UpdateSectionResponseState(SectionStateCommand responseState)
        {
            if (!Enum.IsDefined(typeof(Enums.ResponseStatus), responseState.State))
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidState));
            }
            //get section id and update the state based on lessonid

            var sectionQuestions = _teacherRepository.GetQuestions(responseState.LessonSectionId);

           
            var success = _teacherRepository.AddResponseState(
                responseState.UserId, 
                string.Empty, 
                sectionQuestions.Select(x => new QuestionStateCommand
                {
                    QuestionId = x.QuestionId,
                    State = responseState.State
                }));

            if (!success)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.SectionNotFound));
            }

            _cache.Remove(string.Format(Constants.TeacherDashboardCache, responseState.UserId));
        }

        public QuestionScoreModel GetQuestionScore(string userId, string questionId)
        {
            var scoreModel = _teacherRepository.GetQuestionScore(userId, questionId);

            if(scoreModel == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.QuestionOrUserResponseNotFound));
            }

            return scoreModel;
        }

        public TeachersMentorModel GetTeachersMissingMentor()
        {
            var teachers = _teacherRepository.GetTeachersMissingMentor();

            if(!teachers.Teachers.Any())
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.NoTeacherMissingMentor));
            }

            return teachers;
        }

        public void AssignMentor(AssignMentorCommand assignMentor)
        {
            var success = _teacherRepository.AssignMentor(assignMentor.TeacherId, assignMentor.MentorId);

            if(!success)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidTeacherOrMentor));
            }
        }

        public LessonSetData UpdateActiveLessonSet(ActiveLessonSetCommand activeLessonSet)
        {
            var lessonSets = GetAllLessonSets();

            if (lessonSets == null || !lessonSets.Any() || string.Equals(lessonSets.Last()?.LessonSetId, activeLessonSet.CurrentLessonSetId, StringComparison.OrdinalIgnoreCase))
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.UnableToFetchLessonSetsOrIsLastLessonSet));
            }

            DeleteLessonSetData(new TeacherCommand { TeacherId = activeLessonSet.TeacherId });

            var newLessonSetId = lessonSets
                .Select(x => x.LessonSetId)
                .SkipWhile(x => !string.Equals(x, activeLessonSet.CurrentLessonSetId, StringComparison.OrdinalIgnoreCase))
                .Skip(1)
                .First();

            var success = _teacherRepository.UpdateActiveLessonSet(activeLessonSet.TeacherId, newLessonSetId);

            if (!success)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.GeneralErrorMessage));
            }

            _cache.Remove(string.Format(Constants.TeacherDashboardCache, activeLessonSet.TeacherId));

            return new LessonSetData { LessonSetId = newLessonSetId };
        }

        public void DeleteLessonSetData(TeacherCommand teacher)
        {
            var success = _teacherRepository.DeleteLessonSetData(teacher.TeacherId);

            if (!success)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TeacherNotFound));
            }

            _cache.Remove(string.Format(Constants.TeacherDashboardCache, teacher.TeacherId));
        }

        private IEnumerable<LessonSetData> GetAllLessonSets()
        {
            var lessonSets = _cache.Get<IEnumerable<LessonSetData>>(Constants.LessonSetCache);

            if(lessonSets == null)
            {
                lessonSets = _dashboardRepository.GetAllLessonSets();
                _cache.Set(Constants.LessonSetCache, lessonSets, TimeSpan.FromMinutes(Constants.LessonSetCacheDuration));
            }

            return lessonSets;
        }
    }
}
