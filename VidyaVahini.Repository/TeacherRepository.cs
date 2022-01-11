
using Dapper;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Enum;
using VidyaVahini.Core.Utilities;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Role;
using VidyaVahini.Entities.School;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Entities.Teacher.Dashboard;
using VidyaVahini.Entities.Teacher.Lesson;
using VidyaVahini.Entities.Teacher.LessonSection;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Entities.UserProfile;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IDataAccessRepository<Teacher> _teacher;
        private readonly IDataAccessRepository<Notifications> _notification;
        private readonly IDataAccessRepository<Question> _question;
        private readonly IDataAccessRepository<TeacherResponse>_teacherResponse;
        private readonly IDataAccessRepository<LessonSection>_lessonSection;
        private readonly IDataAccessRepository<LessonSet> _lessonSet;
        private readonly IDataAccessRepository<Lesson> _lesson;
        private readonly IDataAccessRepository<UserProfile> _userprofile;
        private readonly IDataAccessRepository<Query> _query;
        private readonly IDataAccessRepository<TeacherClass> _teacherClass;
        private readonly IDataAccessRepository<TeacherResponseStatu> _teacherResponseStatus;
        private readonly IDataAccessRepository<TeacherSubject> _teacherSubject;
        private readonly IDataAccessRepository<SectionType> _sectionType;
        private readonly IDataAccessRepository<SubQuestion> _subQuestion;
        private readonly IDataAccessRepository<UserProfile> _userProfile;
        private readonly IDataAccessRepository<syncdateinfo> _syncdateinfo;
        private MySqlConnection _dbconnection;
        private MySqlConnection _dbconnection1;
        private MySqlConnection _dbconnectionnew;
        private MySqlConnection _dbconnectionnew1;
        private string Connectionstring = "VidyaVahiniDb";
        

        public TeacherRepository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _lessonSection = _unitOfWork.Repository<LessonSection>();
            _lessonSet = _unitOfWork.Repository<LessonSet>();
            _lesson = _unitOfWork.Repository<Lesson>();
            _teacherResponse = _unitOfWork.Repository<TeacherResponse>();
            _teacher = _unitOfWork.Repository<Teacher>();
            _notification= _unitOfWork.Repository<Notifications>();
            _question = _unitOfWork.Repository<Question>();
            _userprofile = _unitOfWork.Repository<UserProfile>();
            _query = _unitOfWork.Repository<Query>();
            _teacherClass = _unitOfWork.Repository<TeacherClass>();
            _teacherResponseStatus= _unitOfWork.Repository<TeacherResponseStatu>();
            _teacherSubject=_unitOfWork.Repository<TeacherSubject>();
            _sectionType = _unitOfWork.Repository<SectionType>();
            _subQuestion = _unitOfWork.Repository<SubQuestion>();
            _userProfile = _unitOfWork.Repository<UserProfile>();
            _syncdateinfo = _unitOfWork.Repository<syncdateinfo>();
            _dbconnection = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
             }


        public bool GetUser(string contactNumber)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.SchoolProperty);
            includeProperties.Append($",{Constants.TeacherNavigationProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}");

            var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherNavigation.UserProfile.MobileNumber, contactNumber),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (teacher == null)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

            public FindTeacherModel GetTeacher(string email)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.SchoolProperty);
            includeProperties.Append($",{Constants.TeacherNavigationProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}");

            var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherNavigation.UserProfile.Email, email, StringComparison.OrdinalIgnoreCase) && !x.TeacherNavigation.IsActive,
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (teacher == null)
                return null;

            return new FindTeacherModel
            {
                MaxResendActivationEmailLimitReached = (teacher.TeacherNavigation?.ActivationEmailCount ?? 0)
                    >= Constants.MaxResendActivationEmailLimit,
                RegistrationDataAvailable = teacher.TeacherNavigation?.IsRegistered ?? false,
                School = new BaseSchoolData
                {
                    Id = teacher.SchoolId,
                    Name = $"{teacher.School?.SchoolName}, {teacher.School?.City}"
                },
                Teacher = new BaseTeacherData
                {
                    UserId = teacher.TeacherId,
                    Name = teacher.TeacherNavigation?.UserProfile?.Name
                }
            };
        }

        public bool RegisterTeacher(
            UserAccountData userAccount,
            UserProfileData userProfile,
            IEnumerable<UserLanguageData> userLanguages,
            IEnumerable<TeacherClassData> teacherClasses,
            IEnumerable<TeacherSubjectData> teacherSubjects)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.TeacherNavigationProperty);
            includeProperties.Append($",{Constants.TeacherClassProperty}");
            includeProperties.Append($",{Constants.TeacherSubjectProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserLanguagesProperty}");

            var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherId, userAccount.UserId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (teacher == null)
                return false;

            UpdateTeacherAccount(userAccount, teacher);
            UpdateTeacherProfile(userProfile, teacher);
            UpdateTeacherClasses(teacherClasses, teacher);
            UpdateTeacherSubjects(teacherSubjects, teacher);
            UpdateTeacherLanguages(userLanguages, teacher);

            return _unitOfWork.Commit() > 0;
        }
        public bool insertSyncdate(UpdateNotificationCommand syncdateinfo)
        {
            var syncdateinfo1 = _syncdateinfo.Find(x => x.userId == syncdateinfo.userId);
            if (syncdateinfo1 == null)
            {
                string userId = syncdateinfo.userId;
                string syncdate = DateTime.Now.ToString("dd-MMM-yyyy h:mm:ss tt");
                _unitOfWork
                  .Repository<syncdateinfo>()
                  .Add(getsyncdateinfo(userId, syncdate));
            }
            else 
            {
                var syncdateinfos = _syncdateinfo.Filter(x => x.userId == syncdateinfo.userId);


                if (syncdateinfos != null)
                {
                    foreach (var sync in syncdateinfos)
                    {
                        sync.syncdate = DateTime.Now.ToString("dd-MMM-yyyy h:mm:ss tt");
                        _syncdateinfo.Update(sync);
                        _unitOfWork.Commit();
                    }

                }
            }
           

            return _unitOfWork.Commit() > 0;
        }
        private syncdateinfo getsyncdateinfo(string userId,string syncdate)
          => new syncdateinfo
          {
             userId=userId,
             syncdate=syncdate
          };
        private void UpdateTeacherLanguages(IEnumerable<UserLanguageData> userLanguages, Teacher teacher)
        {
            if (teacher.TeacherNavigation.UserLanguages != null && teacher.TeacherNavigation.UserLanguages.Any())
            {
                _unitOfWork
                    .Repository<UserLanguage>()
                    .Delete(teacher.TeacherNavigation.UserLanguages);
            }

            _unitOfWork
                .Repository<UserLanguage>()
                .Add(userLanguages.Select(x => new UserLanguage
                {
                    Created = DateTime.Now,
                    LanguageId = x.LanguageId,
                    LastUpdated = DateTime.Now,
                    UserId = x.UserId
                }));
        }

        private void UpdateTeacherSubjects(IEnumerable<TeacherSubjectData> teacherSubjects, Teacher teacher)
        {
            var teacherSubjectRepository = _unitOfWork.Repository<TeacherSubject>();

            if (teacher.TeacherSubjects != null && teacher.TeacherSubjects.Any())
            {
                teacherSubjectRepository.Delete(teacher.TeacherSubjects);
            }

            teacherSubjectRepository
                .Add(teacherSubjects.Select(x => new TeacherSubject
                {
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    SubjectId = x.SubjectId,
                    TeacherId = x.TeacherId
                }));
        }

        private void UpdateTeacherClasses(IEnumerable<TeacherClassData> teacherClasses, Teacher teacher)
        {
            var teacherClassRepository = _unitOfWork.Repository<TeacherClass>();

            if (teacher.TeacherClasses != null && teacher.TeacherClasses.Any())
            {
                teacherClassRepository.Delete(teacher.TeacherClasses);
            }

            teacherClassRepository
                .Add(teacherClasses.Select(x => new TeacherClass
                {
                    ClassId = x.ClassId,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    TeacherId = x.TeacherId
                }));
        }

        private void UpdateTeacherProfile(UserProfileData userProfile, Teacher teacher)
        {
            teacher.TeacherNavigation.UserProfile.City = userProfile.City;
            teacher.TeacherNavigation.UserProfile.GenderId = userProfile.GenderId;
            teacher.TeacherNavigation.UserProfile.MobileNumber = userProfile.MobileNumber;
            teacher.TeacherNavigation.UserProfile.QualificationId = userProfile.QualificationId;
            teacher.TeacherNavigation.UserProfile.OtherQualification = userProfile.OtherQualification;
            teacher.TeacherNavigation.UserProfile.StateId = userProfile.StateId;
            teacher.TeacherNavigation.UserProfile.countryid = userProfile.countryid;
        }

        private void UpdateTeacherAccount(UserAccountData userAccount, Teacher teacher)
        {
            teacher.TeacherNavigation.IsRegistered = userAccount.IsRegistered;
            teacher.TeacherNavigation.LastUpdated = DateTime.Now;
            teacher.TeacherNavigation.PasswordHash = userAccount.PasswordHash;
            teacher.TeacherNavigation.PasswordSalt = userAccount.PasswordSalt;
            teacher.TeacherNavigation.Token = userAccount.Token;
            teacher.TeacherNavigation.TokenExpiry = userAccount.TokenExpiry;
            teacher.TeacherNavigation.ActivationEmailCount++;
        }

        private void AddTeacherProfile(UserProfileData userProfile, Teacher teacher)
        {
            teacher.TeacherNavigation.UserProfile = new UserProfile
            {
                UserId = userProfile.UserId,
                Name = userProfile.Name,
                Email = userProfile.Email
            };
        }

        private void AddTeacherAccount(UserAccountData userAccount, Teacher teacher)
        {
            teacher.TeacherNavigation = new UserAccount
            {
                UserId = userAccount.UserId,
                Username = userAccount.Username,
                IsActive = userAccount.IsActive,
                IsRegistered = userAccount.IsRegistered,
                ActivationEmailCount = userAccount.ActivationEmailCount,
                FailedLoginAttempt = userAccount.FailedLoginAttempt
            };
        }

        public bool CreateTeacherAccount(UserAccountData userAccount,
            UserProfileData userProfile, UserRoleData userRole, TeacherData teacherData)
        {
            var teacher = _teacher.Add(new Teacher
            {
                TeacherId = teacherData.UserId,
                SchoolId = teacherData.SchoolId,
                ActiveLessonSetId = _configuration["LessonConfiguration:InitialLessonSetId"]
            });

            AddTeacherAccount(userAccount, teacher);

            AddTeacherProfile(userProfile, teacher);

            _unitOfWork
                .Repository<UserRole>()
                .Add(new UserRole
                {
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    RoleId = userRole.RoleId,
                    UserId = userRole.UserId
                });

            return _unitOfWork.Commit() > 0;
        }
        public void UpdateNotification(NotificationCommand notificationCommand)
        {
            var notification = _notification.Filter(x => x.Id ==notificationCommand.Id);

           
                if (notification != null)
                {
                foreach (var notify in notification)
                {
                    notify.status = 0;
                    _notification.Update(notify);
                    _unitOfWork.Commit();
                }
                  
                }            
        }
         public void UpdateNotifyByUserId(UpdateNotificationCommand updatenotificationCommand)
        {
            IEnumerable<Notifications> notification = null;

            if (updatenotificationCommand.status == 2)
            {
                notification = _notification.Filter(x => x.msgto == updatenotificationCommand.userId && x.status == 1);
            }
            else if (updatenotificationCommand.status == 0)
            {
                notification = _notification.Filter(x => x.msgto == updatenotificationCommand.userId);
            }
           

            if (notification != null)
            {
                foreach (var notify in notification)
                {
                    notify.status = updatenotificationCommand.status;
                    _notification.Update(notify);
                    _unitOfWork.Commit();
                }

            }
        }
        public IEnumerable<NotificationsModel> GetNotifications(string userId)
        {
            var notification = _notification.Filter(x => x.msgto == userId && x.status!=0).OrderByDescending(x => x.created_date);
            List<NotificationsModel> notificationModels = new List<NotificationsModel>();
            foreach (var notifications in notification)
            {
                notificationModels.Add(new NotificationsModel
                {
                    Id=notifications.Id,
                    from = notifications.msgfrom,
                    to = notifications.msgto,
                    roleid = notifications.roleid,
                    message = notifications.message,
                    created_date = notifications.created_date,
                    status = notifications.status

                });
            }

            return notificationModels.AsEnumerable();

        }
        public string GetSyncDate(string userId)
        {
            var syncdateinfo1 = _syncdateinfo.Find(x => x.userId == userId);
            string date = syncdateinfo1.syncdate;

            return date;

        }
        public int GetNotificount(string userId,int flag)
        {
            int count = 0;
            if (flag == 0)
            {
                var notification = _notification.Filter(x => x.msgto == userId && x.status != 0).OrderByDescending(x => x.created_date);
                count = notification.Count();
            }
            else if (flag == 1)
            {
                var notification = _notification.Filter(x => x.msgto == userId && x.status == 1).OrderByDescending(x => x.created_date);
                count = notification.Count();
            }
            

            return count;

        }
        public TeacherDashboardStatusModel GetTeacherDashboardStatus(string userId)
        {
            var includeProperties = new StringBuilder(Constants.QueriesProperty);
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}.{Constants.LessonSetsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.SectionTypeProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}.{Constants.LessonSetsProperty}.{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}");
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.SectionTypeProperty}");
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.TeacherResponseStatusProperty}");

            var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();
            //var teacher = _teacher
            //    .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase))
            //    .FirstOrDefault();
            teacher.Queries = (_query
                .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase))).ToList();

            teacher.TeacherClasses= (_teacherClass
                .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase))).ToList();

            teacher.TeacherResponseStatus = (_teacherResponseStatus
                .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase))).ToList();
            teacher.TeacherResponses = (_teacherResponse
               .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase))).ToList();
            teacher.TeacherSubjects = (_teacherSubject
             .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase))).ToList();
            //teacher.ActiveLessonSet =_lessonSet
            //     .Filter(filter: x => x.LessonSetId != null)
            //      .OrderBy(x => x.LessonSetOrder).ToList();

            var teacherDashboard = new TeacherDashboardStatusModel();

            teacherDashboard.UserId = userId;

            teacherDashboard.RedoActiveLessonSet = teacher.RedoActiveLessonSet;

            teacherDashboard.LoadNextLessonSet = teacher.LoadNextLessonSet;

            teacherDashboard.TotalLessonsInCurrentSet = teacher.ActiveLessonSet.Lessons.Count();
            teacherDashboard.TotalLessonsInLevel = teacher.ActiveLessonSet.Lessons.Count();

            teacherDashboard.PendingQueries = new TeacherDashboardQueryModel
            {
                Count = teacher.Queries.Where(x => x.QueryStatus == (int)Enums.QueryStatus.PENDING_WITH_TEACHER).Count(),
                QueryIds = teacher.Queries.Where(x => x.QueryStatus == (int)Enums.QueryStatus.PENDING_WITH_TEACHER).Select(x => x.QueryId)
            };

            teacherDashboard.LessonStatuses = teacher
                .ActiveLessonSet
                .Lessons
                .Select(x => GetLessonStatus(userId, x));

            teacherDashboard.LessonsCompletedInCurrentSet = teacherDashboard
                .LessonStatuses
                //.Where(x => x.LessonStatus == Enums.LessonStatus.COMPLETED_AND_REVIEWED.ToString() && x.LessonNumber != Constants.AssessmentLessonNumber)
                .Where(x => x.LessonStatus == Enums.LessonStatus.COMPLETED_AND_REVIEWED.ToString())

                .Count();

            teacherDashboard.LessonsCompletedInLevel = teacherDashboard.LessonsCompletedInCurrentSet + teacher
                .ActiveLessonSet
                .Level
                .LessonSets
                .OrderBy(x => x.LessonSetOrder)
                .TakeWhile(x => !string.Equals(x.LessonSetId, teacher.ActiveLessonSetId, StringComparison.OrdinalIgnoreCase))
                //.Sum(x => x.Lessons.Where(x => x.LessonNumber != Constants.AssessmentLessonNumber).Count());
                .Sum(x => x.Lessons.Count());

            return teacherDashboard;
        }
        


        public IEnumerable<TeacherSummaryLessonReport> GetTeacherSummaryReport()
        {
            var includeProperties = new StringBuilder(Constants.QueriesProperty);
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}.{Constants.LessonSetsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}.{Constants.LessonSetsProperty}.{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.SectionTypeProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.TeacherResponseStatusProperty}");



            // var teacherReport = new TeacherReport();
            //IEnumerable<TeacherLessonReport> teacherReport1 =null;

            var lessonsSummary = new TeacherLessonsSummary();
            List<TeacherSummaryLessonReport> listreport = new List<TeacherSummaryLessonReport>();
            var teacherDashboard = new TeacherDashboardStatusModel();


            var lessonSet = _lessonSet
                 .Filter(filter: x => x.LessonSetId != null)
                  .OrderBy(x => x.LessonSetOrder);


            foreach (var ls in lessonSet)
            {

                var teacherReport = new TeacherSummaryLessonReport();
               

                teacherReport.LessonSet = ls.LessonSetOrder.ToString();

                var lessons = _lesson
                 .Filter(filter: x => x.LessonSetId == ls.LessonSetId)
                 .OrderBy(x => x.LessonNumber);
                List<TeacherLessonsSummary> lesslist = new List<TeacherLessonsSummary>();

                foreach (var less in lessons)
                {
                    int tbcount = 0;
                    int cacount = 0;
                    int srcount = 0;
                    int ongcount = 0;
                    int teachercount = 0;
                    var teacherless = new TeacherLessonsSummary();
                    teacherless.LessonNumber = less.LessonNumber;

                    //     var allteacher = _teacher
                    //.Filter(filter: x => x.MentorId != null && x.SchoolId > 1,
                    //    includeProperties: includeProperties.ToString());
                    DataTable _dt1 = new DataTable();
                    _dt1.Load(_dbconnection.ExecuteReader("select * from teacher where mentorid!='' and schoolid>1"));
                    //int cnt = int.Parse(_dt1.Rows[0]["cnt"].ToString());
                    teachercount = _dt1.Rows.Count;
                    for (int i=0;i<= _dt1.Rows.Count-1;i++)
                    {
                        DataTable _dt = new DataTable();
                        _dt.Load(_dbconnection.ExecuteReader("select distinct (questionid) from teacherresponsestatus where questionid in (select questionid from question  where lessonsectionid in (select lessonsectionid from lessonsection where lessonid = '" + less.LessonId + "')) and ResponseState = 4 and teacherid='"+ _dt1.Rows[i]["TeacherId"].ToString() + "'"));
                        // List<int> ca = new List<int>();
                        if (_dt.Rows.Count > 0)
                        {
                            if (_dt.Rows.Count == 10)
                            {
                                cacount = cacount + 1;
                                teacherless.COMPLETEDANDAPPROVED = cacount;
                            }
                            else
                            {
                                DataTable _dton = new DataTable();
                                _dton.Load(_dbconnection.ExecuteReader("select distinct(questionid) as cnt from teacherresponsestatus where questionid in (select questionid from question  where lessonsectionid in (select lessonsectionid from lessonsection where lessonid = '" + less.LessonId + "')) and (ResponseState = 1 or ResponseState = 2 or ResponseState = 3 or ResponseState = 4)  and teacherid='" + _dt1.Rows[i]["TeacherId"].ToString() + "'"));
                                // List<int> on = new List<int>();
                                //List<int> tb = new List<int>();
                                if (_dton.Rows.Count > 0)
                                {
                                    int cnt3 = _dton.Rows.Count;
                                    if (cnt3 <= 10)
                                    {
                                        ongcount = ongcount + 1;
                                    }

                                }
                                else
                                {
                                    tbcount = tbcount + 1;
                                }

                            }

                        }
                        else 
                        {

                            DataTable _dtsr = new DataTable();
                            _dtsr.Load(_dbconnection.ExecuteReader("select distinct(questionid) as cnt from teacherresponsestatus where questionid in (select questionid from question  where lessonsectionid in (select lessonsectionid from lessonsection where lessonid = '" + less.LessonId + "')) and ResponseState = 2 and teacherid='" + _dt1.Rows[i]["TeacherId"].ToString() + "'  and questionid not in (select questionid from teacherresponsestatus where questionid in (select questionid from question  where lessonsectionid in (select lessonsectionid from lessonsection where lessonid = '" + less.LessonId + "')) and ResponseState = 4 and teacherid='" + _dt1.Rows[i]["TeacherId"].ToString() + "'  ) "));
                            //List<int> sr = new List<int>();
                            if (_dtsr.Rows.Count > 0)
                            {
                                int cnt2 = _dtsr.Rows.Count;
                                if (cnt2 == 10)
                                {
                                    srcount = srcount + 1;
                                    teacherless.SUBMITTEDFORREVEW = srcount;
                                }
                                else
                                {
                                    DataTable _dton = new DataTable();
                                    _dton.Load(_dbconnection.ExecuteReader("select distinct(questionid) as cnt from teacherresponsestatus where questionid in (select questionid from question  where lessonsectionid in (select lessonsectionid from lessonsection where lessonid = '" + less.LessonId + "')) and (ResponseState = 1 or ResponseState = 2 or ResponseState = 3 or ResponseState = 4)  and teacherid='" + _dt1.Rows[i]["TeacherId"].ToString() + "'"));
                                    // List<int> on = new List<int>();
                                    //List<int> tb = new List<int>();
                                    if (_dton.Rows.Count > 0)
                                    {
                                        int cnt3 = _dton.Rows.Count;
                                        if (cnt3 <= 10)
                                        {
                                            ongcount = ongcount + 1;
                                        }

                                    }
                                    else
                                    {
                                        tbcount = tbcount + 1;
                                    }


                                }
                            }
                            else
                            {
                                DataTable _dton = new DataTable();
                                _dton.Load(_dbconnection.ExecuteReader("select distinct(questionid) as cnt from teacherresponsestatus where questionid in (select questionid from question  where lessonsectionid in (select lessonsectionid from lessonsection where lessonid = '" + less.LessonId + "')) and (ResponseState = 1 or ResponseState = 2 or ResponseState = 3 or ResponseState = 4)  and teacherid='" + _dt1.Rows[i]["TeacherId"].ToString() + "'"));
                                // List<int> on = new List<int>();
                                //List<int> tb = new List<int>();
                                if (_dton.Rows.Count > 0)
                                {
                                    int cnt3 = _dton.Rows.Count;
                                    if (cnt3 <= 10)
                                    {
                                        ongcount = ongcount + 1;
                                    }

                                }
                                else
                                {
                                    tbcount = tbcount + 1;
                                }

                            }
                        }

                        teacherless.ONGOING = ongcount;
                        teacherless.TOBEDONE = tbcount;
                    }                 


                    _dbconnection.Close();
                    _dbconnection.Open();
                    lesslist.Add(teacherless);
                }              
                teacherReport.Lessons = lesslist.AsEnumerable();  
                listreport.Add(teacherReport);
            }

            return listreport.AsEnumerable();
        }

        

        public IEnumerable<TeacherLessonReport> GetTeacherReport()
        {
            _dbconnection1 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));

            var includeProperties = new StringBuilder(Constants.ActiveLessonSetProperty);
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}");
           // includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}");
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}");
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}.{Constants.LessonSetsProperty}");
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}");
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}.{Constants.LessonSetsProperty}.{Constants.LessonsProperty}");
            //includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}");
           // includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.SectionTypeProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.TeacherResponseStatusProperty}");
         
           
            var tr = new TeacherReport();
            List<TeacherLessonReport> listreport = new List<TeacherLessonReport>();
           
            DataTable _dt1 = new DataTable();
            _dt1.Load(_dbconnection1.ExecuteReader("select * from teacher where mentorid!='' and schoolid>1"));


            for (int i = 0; i <= _dt1.Rows.Count - 1; i++)
            {   
                List<TeacherReportLessonSet> stadet = new List<TeacherReportLessonSet>();
                var teacherReport = new TeacherLessonReport();
                var teacherDashboard = new TeacherDashboardStatusModel();
                int lessset = 0;
                string userId = _dt1.Rows[i]["TeacherId"].ToString();

                teacherReport.UserId = userId;
                var username = _userprofile.Find(x => string.Equals(x.UserId, teacherReport.UserId));
                teacherReport.UserName = username.Email;
                teacherReport.ContactNumber = username.MobileNumber;

                var mentorname = _userprofile.Find(x => string.Equals(x.UserId, _dt1.Rows[i]["MentorId"].ToString()));
                teacherReport.MentorName = mentorname.Email;
               
                var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherId, _dt1.Rows[i]["TeacherId"].ToString(), StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

                var lessonSet = _lessonSet
                     .Filter(filter: x => x.LessonSetId == teacher.ActiveLessonSetId)
                      .OrderBy(x => x.LessonSetOrder);                

                foreach (var ls in lessonSet)
                {
                   lessset = ls.LessonSetOrder;
                }               

                var lessonSet1 = _lessonSet
                   .Filter(filter: x => x.LessonSetId!=null)
                    .OrderBy(x => x.LessonSetOrder);

                foreach (var lst1 in lessonSet1)
                {
                    var lessonsetdet = new TeacherReportLessonSet();
                    List<string> SFR = new List<string>();
                    List<string> CAR = new List<string>();
                    List<string> TBD = new List<string>();
                    List<string> ONG = new List<string>();
                    string lessid = "";
                     lessid= lst1.LessonSetId;
                    lessonsetdet.LessonSet = lst1.LessonSetOrder;
                    var lessons = _lesson
                 .Filter(filter: x => x.LessonSetId == lst1.LessonSetId)
                 .OrderBy(x => x.LessonNumber);

                    foreach (var less in lessons)
                    {
                        teacherReport.LessonStatuses = GetLessonTeacherStatus(userId, less);

                        foreach (var lesssta in teacherReport.LessonStatuses)
                        {
                            string Status = lesssta.LessonStatus;
                            if (Status == "SUBMITTED_FOR_REVIEW")
                            {
                                SFR.Add(lesssta.LessonName);
                            }
                            else if (Status == "COMPLETED_AND_REVIEWED")
                            {
                                CAR.Add(lesssta.LessonName);
                            }
                            else if (Status == "TO_BE_DONE")
                            {
                                TBD.Add(lesssta.LessonName);
                            }
                            else if (Status == "ONGOING")
                            {
                                ONG.Add(lesssta.LessonName);
                            }
                            if (lessid != teacher.ActiveLessonSetId)
                            {
                                if (lessset > lessonsetdet.LessonSet)
                                {
                                    CAR.Add(lesssta.LessonName);
                                    lessonsetdet.TOBEDONE = null;
                                    lessonsetdet.ONGOING = null;
                                    lessonsetdet.SUBMITTEDFORREVIEW = null;
                                    lessonsetdet.COMPLETEDANDAPPROVED = CAR;
                                }
                                else if(lessset < lessonsetdet.LessonSet)
                                {                                    
                                    lessonsetdet.TOBEDONE = TBD;
                                    lessonsetdet.ONGOING = null;
                                    lessonsetdet.SUBMITTEDFORREVIEW = null;
                                    lessonsetdet.COMPLETEDANDAPPROVED = null; 
                                }
                             
                            }
                            else
                            {
                                lessonsetdet.TOBEDONE = TBD;
                                lessonsetdet.ONGOING = ONG;
                                lessonsetdet.SUBMITTEDFORREVIEW = SFR;
                                lessonsetdet.COMPLETEDANDAPPROVED = CAR;
                            }
                        }                      
                    }
                    stadet.Add(lessonsetdet);
                    teacherReport.LessonSet = stadet.AsEnumerable();
                    teacherReport.LessonStatuses = null;
                }
                listreport.Add(teacherReport);
            }
           
            return listreport.AsEnumerable();
        }

      
        private IEnumerable<TeacherReportLessonStatusModel> GetLessonTeacherStatus(string userId, Lesson lesson)
        {
            List<TeacherReportLessonStatusModel> lst = new List<TeacherReportLessonStatusModel>();
             var lessonStatus = new TeacherReportLessonStatusModel();

            lessonStatus.LessonId = lesson.LessonId;

            lessonStatus.LessonCode = lesson.LessonCode;

            lessonStatus.LessonNumber = lesson.LessonNumber;
            lessonStatus.LessonName = lesson.LessonName;

            lessonStatus.LessonSectionStatuses = lesson
                .LessonSections
                .Select(x => GetLessonTeacherSectionStatus(userId, x));

            lessonStatus.LessonStatus =
                GetLessonTeacherStatus(lessonStatus.LessonSectionStatuses.Select(x => x.LessonSectionStatus));

            lst.Add(lessonStatus);
            return lst.AsEnumerable();
        }


        private TeacherLessonStatusModel GetLessonStatus(string userId, Lesson lesson)
        {
            var lessonStatus = new TeacherLessonStatusModel();

            lessonStatus.LessonId = lesson.LessonId;

            lessonStatus.LessonCode = lesson.LessonCode;

            lessonStatus.LessonNumber = lesson.LessonNumber;

            lessonStatus.LessonSectionStatuses = lesson
                .LessonSections
                .Select(x => GetLessonSectionStatus(userId, x));

            lessonStatus.LessonStatus =
                GetLessonStatus(lessonStatus.LessonSectionStatuses.Select(x => x.LessonSectionStatus));

            return lessonStatus;
        }

       

        private TeacherReportLessonSectionStatusModel GetLessonTeacherSectionStatus(string userId, LessonSection lessonSection)
        {
            var lessonSectionStatus = new TeacherReportLessonSectionStatusModel();

            lessonSectionStatus.LessonSectionId = lessonSection.LessonSectionId;

            lessonSectionStatus.LessonSectionName = lessonSection.LessonSectionName;

            lessonSectionStatus.LessonSectionCode = lessonSection.SectionType?.SectionTypeCode;

            lessonSectionStatus.LessonQuestionStatuses = lessonSection
                .Questions
                .Select(x => GetTeacherQuestionStatus(userId, x));

            lessonSectionStatus.LessonSectionStatus =
                GetTeacherLessonSectionStatus(lessonSectionStatus.LessonQuestionStatuses.Select(x => x.QuestionStatus));

            return lessonSectionStatus;
        }



        private TeacherLessonSectionStatusModel GetLessonSectionStatus(string userId, LessonSection lessonSection)
        {
            var lessonSectionStatus = new TeacherLessonSectionStatusModel();

            lessonSectionStatus.LessonSectionId = lessonSection.LessonSectionId;

            lessonSectionStatus.LessonSectionName = lessonSection.LessonSectionName;

            lessonSectionStatus.LessonSectionCode = lessonSection.SectionType?.SectionTypeCode;

            lessonSectionStatus.LessonQuestionStatuses = lessonSection
                .Questions
                .Select(x => GetQuestionStatus(userId, x));

            lessonSectionStatus.LessonSectionStatus =
                GetLessonSectionStatus(lessonSectionStatus.LessonQuestionStatuses.Select(x => x.QuestionStatus));

            return lessonSectionStatus;
        }

        
        private TeacherReportQuestionStatusModel GetTeacherQuestionStatus(string userId, Question question)
        {
            var questionStatus = new TeacherReportQuestionStatusModel();

            questionStatus.QuestionId = question.QuestionId;

            int status = question.TeacherResponseStatus
                ?.Where(x => string.Equals(userId, x.TeacherId, StringComparison.OrdinalIgnoreCase))
                ?.OrderByDescending(x => x.Created)
                ?.FirstOrDefault()?.ResponseState
                ?? 0;

            questionStatus.QuestionStatus = Enum.IsDefined(typeof(Enums.ResponseStatus), status)
                ? ((Enums.ResponseStatus)status).ToString() : Enums.ResponseStatus.TO_BE_DONE.ToString();

            return questionStatus;
        }


        private TeacherQuestionStatusModel GetQuestionStatus(string userId, Question question)
        {
            var questionStatus = new TeacherQuestionStatusModel();

            questionStatus.QuestionId = question.QuestionId;

            int status = question.TeacherResponseStatus
                ?.Where(x => string.Equals(userId, x.TeacherId, StringComparison.OrdinalIgnoreCase))
                ?.OrderByDescending(x => x.Created)
                ?.FirstOrDefault()?.ResponseState
                ?? 0;

            questionStatus.QuestionStatus = Enum.IsDefined(typeof(Enums.ResponseStatus), status)
                ? ((Enums.ResponseStatus)status).ToString() : Enums.ResponseStatus.TO_BE_DONE.ToString();

            return questionStatus;
        }

        
        private string GetLessonSectionStatus(IEnumerable<string> responseStates)
        {
            var sectionStatus = string.Empty;

            if (!responseStates.Any() || responseStates.All(x => x == Enums.ResponseStatus.TO_BE_DONE.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.TO_BE_DONE.ToString();
            }
            else if (responseStates.All(x => x == Enums.ResponseStatus.COMPLETED_AND_APPROVED.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.COMPLETED_AND_APPROVED.ToString();
            }
            else if (responseStates.All(x => x == Enums.ResponseStatus.SUBMITTED_FOR_REVIEW.ToString()
            || x == Enums.ResponseStatus.COMPLETED_AND_APPROVED.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.SUBMITTED_FOR_REVIEW.ToString();
            }
            else if (responseStates.Any(x => x == Enums.ResponseStatus.REDO_SUBMISSION.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.REDO_SUBMISSION.ToString();
            }           

            //else if (responseStates.Any(x => x == Enums.ResponseStatus.SUBMITTED_FOR_REVIEW.ToString()))
            //{
            //    sectionStatus = Enums.LessonSectionStatus.SUBMITTED_FOR_REVIEW.ToString();
            //}
            else
            {
                sectionStatus = Enums.LessonSectionStatus.COMPLETE_AND_SUBMIT.ToString();
            }

            return sectionStatus;
        }

        private string GetTeacherLessonSectionStatus(IEnumerable<string> responseStates)
        {
            var sectionStatus = string.Empty;

            if (!responseStates.Any() || responseStates.All(x => x == Enums.ResponseStatus.TO_BE_DONE.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.TO_BE_DONE.ToString();
            }
            else if (responseStates.All(x => x == Enums.ResponseStatus.COMPLETED_AND_APPROVED.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.COMPLETED_AND_APPROVED.ToString();
            }
            else if (responseStates.All(x => x == Enums.ResponseStatus.SUBMITTED_FOR_REVIEW.ToString()
            || x == Enums.ResponseStatus.COMPLETED_AND_APPROVED.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.SUBMITTED_FOR_REVIEW.ToString();
            }
            else if (responseStates.Any(x => x == Enums.ResponseStatus.REDO_SUBMISSION.ToString()))
            {
                sectionStatus = Enums.LessonSectionStatus.REDO_SUBMISSION.ToString();
            }

            //else if (responseStates.Any(x => x == Enums.ResponseStatus.SUBMITTED_FOR_REVIEW.ToString()))
            //{
            //    sectionStatus = Enums.LessonSectionStatus.SUBMITTED_FOR_REVIEW.ToString();
            //}
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
                sectionStatus.Any(x => x == Enums.LessonSectionStatus.REDO_SUBMISSION.ToString()))
            {
                lessonStatus = Enums.LessonStatus.SUBMITTED_FOR_REVIEW.ToString();
            }
            else
            {
                lessonStatus = Enums.LessonStatus.ONGOING.ToString();
            }

            return lessonStatus;
        }

        
        private string GetLessonTeacherStatus(IEnumerable<string> sectionStatus)
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
            else if (sectionStatus.All(x => x == Enums.LessonSectionStatus.SUBMITTED_FOR_REVIEW.ToString()))
            {
                lessonStatus = Enums.LessonStatus.SUBMITTED_FOR_REVIEW.ToString();
            }
            else
            {
                lessonStatus = Enums.LessonStatus.ONGOING.ToString();
            }

            return lessonStatus;
        }

        public TeacherDashboardModel GetTeacherDashboard(string userId)
        {
            var includeProperties = new StringBuilder(Constants.TeacherResponseStatusProperty);
            includeProperties.Append($",{Constants.QueriesProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.TeacherResponseStatusProperty}.{Constants.QuestionProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}");
            includeProperties.Append($",{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.SectionTypeProperty}");

            var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            return GetTeacherDashboard(teacher);
        }

        private TeacherDashboardModel GetTeacherDashboard(Teacher teacher)
        {
            return new TeacherDashboardModel
            {
                LevelId = teacher.ActiveLessonSet.LevelId,
                LevelCode = teacher.ActiveLessonSet.Level.LevelCode,
                TotalLessonsInCurrentSet = teacher.ActiveLessonSet.Lessons.Count(),
                TotalLessonsInLevel = teacher.ActiveLessonSet.Lessons.Count(),
                LessonSetId = teacher.ActiveLessonSetId,
                PendingQueriesCount = teacher.Queries.Where(x => x.QueryStatus == (int)Enums.QueryStatus.PENDING_WITH_TEACHER).Count(),
                PendingQueryIds = teacher.Queries.Where(x => x.QueryStatus == (int)Enums.QueryStatus.PENDING_WITH_TEACHER).Select(x => x.QueryId),
                Lessons = teacher.ActiveLessonSet
                    .Lessons
                    .OrderBy(x => x.LessonNumber)
                    .Select(x => new LessonModel
                    {
                        LessonId = x.LessonId,
                        LessonCode = x.LessonCode,
                        LessonNumber = x.LessonNumber,
                        LessonSections = x
                            .LessonSections
                            .OrderBy(x => x.SectionTypeId)
                            .Select(x => new LessonSectionModel
                            {
                                SectionId = x.LessonSectionId,
                                SectionCode = x.SectionType.SectionTypeCode,
                                SectionName = x.LessonSectionName,
                                QuestionResponses = teacher
                                    .TeacherResponseStatus
                                    .Where(y => y.Question.LessonSectionId == x.LessonSectionId)
                                    .OrderByDescending(x => x.Created)
                                    .GroupBy(x => x.QuestionId)
                                    .Select(x => x.First())
                                    .Select(x => new QuestionResponseModel
                                    {
                                        ResponseState = x.ResponseState
                                    }).ToList()
                            }).ToList()
                    }).ToList()
            };
        }

        //public InstructionModel GetLessonSectionInstructions(string lessonSectionId, int languageId)
        //{
        //    var includeProperties = new StringBuilder();
        //    includeProperties.Append(Constants.InstructionMediasProperty);
        //    includeProperties.Append($",{Constants.InstructionMediasProperty}.{Constants.MediaProperty}");

        //    var instruction = _unitOfWork.Repository<Instruction>()
        //        .Filter(filter: x => string.Equals(lessonSectionId, x.LessonSectionId, StringComparison.OrdinalIgnoreCase),
        //            includeProperties: includeProperties.ToString())
        //        .FirstOrDefault();

        //    if (instruction == null)
        //        return null;

        //    if (languageId == 12)
        //    {
        //        languageId = 12;
        //    }
        //    else
        //    {
        //        languageId = 1;
        //    }
        //    var instructionMedia = instruction.InstructionMedias.FirstOrDefault(x => x.LanguageId == languageId);

        //    if (instructionMedia == null && Constants.EnableLanguageFallback && languageId != Constants.FallbackLanguage)
        //    {
        //        instructionMedia = instruction.InstructionMedias.FirstOrDefault(x => x.LanguageId == Constants.FallbackLanguage);
        //    }



        //    return new InstructionModel
        //    {
        //        InstructionId = instruction.InstructionId,
        //        LanguageId = instructionMedia?.LanguageId ?? 0,
        //        MediaId = instructionMedia?.Media?.MediaId,
        //        MediaTypeId = instructionMedia?.Media?.MediaTypeId ?? 0,
        //        MediaSource = Util.GetMedia(instructionMedia?.Media?.MediaTypeId, instructionMedia?.Media?.MediaSource)
        //    };
        //}


        public SectionInstructionModel GetLessonSectionInstructions(int SectionTypeId,string RoleId, int languageId)
        {
            var checklang = _unitOfWork.Repository<SectionInstruction>()
                .Filter(filter: x =>string.Equals(RoleId, x.Role) && string.Equals(languageId, x.LanguageId))
                .FirstOrDefault();

            if (checklang == null)
            {
                languageId = 1;
            }            

            var sectioninstruction = _unitOfWork.Repository<SectionInstruction>()
                .Filter(filter: x => string.Equals(SectionTypeId, x.SectionTypeId) &&
                string.Equals(RoleId, x.Role) && string.Equals(languageId, x.LanguageId))
                .FirstOrDefault();

            if (sectioninstruction == null)
                return null;                    
           

            return new SectionInstructionModel
            {
              SectionTypeId=sectioninstruction.SectionTypeId,
                LanguageId = sectioninstruction.LanguageId,               
                MediaSource = sectioninstruction.MediaSource,
                Role=sectioninstruction.Role
            };
        }




        public IEnumerable<TeacherDashboardQuestionIdsModel> GetQuestions(string LessonSectionId)
        {
            var questionsIdList = _question
                .Filter(filter: x => string.Equals(x.LessonSectionId, LessonSectionId, StringComparison.OrdinalIgnoreCase), null)
                .Select(x => new TeacherDashboardQuestionIdsModel { QuestionId = x.QuestionId });


            if (questionsIdList == null || !questionsIdList.Any())
                return null;

            return questionsIdList;
        }

        public TeacherLessonSetModel GetQuestionResponseByLessonSetId(string lessonsetId, string userId)
        {
            var lookuptqr = new Dictionary<string, TeacherQuestionResponse>();

            var lookuptl = new Dictionary<int, TeacherLessonModel>();
            var questionList = lookuptqr.Values;
            List<TeacherLessonSectionModel> tqrlsm = new List<TeacherLessonSectionModel>();
            var questionList1 = lookuptl.Values;          
            _dbconnectionnew = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            List<TeacherLessonModel> tlst = new List<TeacherLessonModel>();
            DataTable _dt1 = new DataTable();

            _dt1.Load(_dbconnectionnew.ExecuteReader("select l.lessonid,lt.lessonsetorder from lesson l inner join  lessonset lt on lt.lessonsetid = l.lessonsetid  where l.lessonsetid = '" + lessonsetId + "'"));
            List<TeacherLessonSetModel> tlsetm = new List<TeacherLessonSetModel>();
            TeacherLessonSetModel tsl = new TeacherLessonSetModel();
            tsl.LessonSetId = lessonsetId;
            tsl.LessonSetNumber = Convert.ToInt32(_dt1.Rows[0]["lessonsetorder"]);          

            for (int j = 0; j <= _dt1.Rows.Count - 1; j++)
            {
                _dbconnectionnew1 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));

                DataTable _dt = new DataTable();

                _dt.Load(_dbconnectionnew1.ExecuteReader("select ls.LessonSectionId,l.LessonId,l.LessonCode,l.LessonNumber from lessonsection ls inner join lesson l on ls.lessonid = l.lessonid where ls.lessonid = '" + _dt1.Rows[j]["lessonid"].ToString() + "'"));

                List<TeacherLessonSectionModel> tlslst = new List<TeacherLessonSectionModel>();
              
                TeacherLessonModel tl = new TeacherLessonModel();
                tl.LessonId = _dt.Rows[0]["LessonId"].ToString();
                tl.LessonCode = _dt.Rows[0]["LessonCode"].ToString();
                tl.LessonNumber = Convert.ToInt32(_dt.Rows[0]["LessonNumber"].ToString());


                for (int i = 0; i <= _dt.Rows.Count - 1; i++)
                {
                    string lessonSectionId = _dt.Rows[i]["LessonSectionId"].ToString();

                    List<TeacherQuestionResponse> tqrlst1 = new List<TeacherQuestionResponse>();
                    TeacherLessonSectionModel tls = new TeacherLessonSectionModel();
                    _dbconnectionnew = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));

                    DataTable _dtLessonsection = new DataTable();
                    _dtLessonsection.Load(_dbconnectionnew.ExecuteReader("select * from lessonsection where LessonSectionId='" + lessonSectionId + "'"));


                    tls.SectionDescription = _dtLessonsection.Rows[0]["LessonSectionDescription"].ToString();
                    tls.SectionName = _dtLessonsection.Rows[0]["LessonSectionName"].ToString();
                    tls.SectionId = _dtLessonsection.Rows[0]["SectionTypeId"].ToString();
                    tls.LessonSectionId = lessonSectionId;

                    int Sectiontypeid = Convert.ToInt32(_dtLessonsection.Rows[0]["SectionTypeId"]);
                    if (Sectiontypeid == 1 || Sectiontypeid == 4 || Sectiontypeid == 5)
                    {
                        if (Sectiontypeid == 1)
                        {
                            tls.SectionCode = "LISTEN_KEENLY";

                        }
                        else if (Sectiontypeid == 4)
                        {
                            tls.SectionCode = "WRITE_RIGHT";
                        }
                        else if (Sectiontypeid == 5)
                        {
                            tls.SectionCode = "ASSESSMENT";
                        }


                        tls.TeacherQuestionResponses = getResultsWithSubQuestions1(lessonSectionId, userId);
                        // questionList = lookuptqr.Values; 
                        foreach (var item in tls.TeacherQuestionResponses)
                        {
                            tqrlst1.Add(item);
                        }
                        tlslst.Add(tls);
                    }
                    else
                    {
                        if (Sectiontypeid == 2)
                        {
                            tls.SectionCode = "SPEAK_WELL";
                        }
                        else if (Sectiontypeid == 3)
                        {
                            tls.SectionCode = "READ_ALOUD";
                        }
                        tls.TeacherQuestionResponses = getResultsWithoutSubQuestions1(lessonSectionId, userId);

                        foreach (var item in tls.TeacherQuestionResponses)
                        {
                            tqrlst1.Add(item);
                        }
                        tlslst.Add(tls);
                    }
                    tl.LessonSections = tlslst;
                    
                }
                tlst.Add(tl);
                _dbconnectionnew1.Close();
                _dbconnectionnew1 = null;
                
                tsl.Lessons = tlst;
            }
            return tsl;
        }


        public TeacherLessonModel GetQuestionResponseByLessonId(string lessonId, string userId)
        {
            var lookuptqr = new Dictionary<string, TeacherQuestionResponse>();
           
            var lookuptl = new Dictionary<int, TeacherLessonModel>();          
            var questionList = lookuptqr.Values;
            List<TeacherLessonSectionModel> tqrlsm = new List<TeacherLessonSectionModel>();

              
            var questionList1 = lookuptl.Values;

            _dbconnectionnew1 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));

            DataTable _dt = new DataTable();

            _dt.Load(_dbconnectionnew1.ExecuteReader("select ls.LessonSectionId,l.LessonId,l.LessonCode,l.LessonNumber from lessonsection ls inner join lesson l on ls.lessonid = l.lessonid where ls.lessonid = '"+lessonId+"'"));

           List<TeacherLessonSectionModel> tlslst = new List<TeacherLessonSectionModel>();
           
            TeacherLessonModel tl = new TeacherLessonModel();
            tl.LessonId = _dt.Rows[0]["LessonId"].ToString();
            tl.LessonCode= _dt.Rows[0]["LessonCode"].ToString();
            tl.LessonNumber= Convert.ToInt32(_dt.Rows[0]["LessonNumber"].ToString());

            
            for (int i=0;i<=_dt.Rows.Count-1;i++)
            {
                string lessonSectionId = _dt.Rows[i]["LessonSectionId"].ToString();

                List<TeacherQuestionResponse> tqrlst1 = new List<TeacherQuestionResponse>();
                TeacherLessonSectionModel tls = new TeacherLessonSectionModel();
                _dbconnectionnew = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));

                DataTable _dtLessonsection = new DataTable();
                _dtLessonsection.Load(_dbconnectionnew.ExecuteReader("select * from lessonsection where LessonSectionId='" + lessonSectionId + "'"));

               
                tls.SectionDescription = _dtLessonsection.Rows[0]["LessonSectionDescription"].ToString();
                tls.SectionName = _dtLessonsection.Rows[0]["LessonSectionName"].ToString();
                tls.SectionId= _dtLessonsection.Rows[0]["SectionTypeId"].ToString();
                tls.LessonSectionId = lessonSectionId;


                int Sectiontypeid = Convert.ToInt32(_dtLessonsection.Rows[0]["SectionTypeId"]);
                if (Sectiontypeid == 1 || Sectiontypeid == 4 || Sectiontypeid == 5)
                {
                    if (Sectiontypeid == 1)
                    {
                        tls.SectionCode = "LISTEN_KEENLY";

                    }
                    else if (Sectiontypeid == 4)
                    {
                        tls.SectionCode = "WRITE_RIGHT";
                    }
                    else if (Sectiontypeid == 5)
                    {
                        tls.SectionCode = "ASSESSMENT";
                    }


                    tls.TeacherQuestionResponses = getResultsWithSubQuestions1(lessonSectionId, userId);
                    // questionList = lookuptqr.Values; 
                    foreach (var item in tls.TeacherQuestionResponses)
                    {
                        tqrlst1.Add(item);
                    }
                    tlslst.Add(tls);
                }
                else
                {
                      if (Sectiontypeid == 2)
                    {
                        tls.SectionCode = "SPEAK_WELL";
                    }
                    else if (Sectiontypeid == 3)
                    {
                        tls.SectionCode = "READ_ALOUD";
                    }
                    tls.TeacherQuestionResponses = getResultsWithoutSubQuestions1(lessonSectionId, userId);
                    
                    foreach (var item in tls.TeacherQuestionResponses)
                    {
                        tqrlst1.Add(item);
                    }
                    tlslst.Add(tls);
                }
                tl.LessonSections = tlslst;

               

               // tqrlsm.AddRange(tqrlst1);


            }
           // tl.LessonSections = tqrlsm;
            return tl;
        }

        public IEnumerable<TeacherQuestionResponse> getResultsWithSubQuestions1(string lessonSectionId, string userId)
        {
            int _maxScoreCount = 0;
            int ASSMaxCount = 0;
           
            var lookuptqr = new Dictionary<string, TeacherQuestionResponse>();
            var lookuptrm = new Dictionary<int, TeacherResponseMedia>();
            var lookuptsqm = new Dictionary<int, TeacherSubQuestionModel>();
            var lookuptsqam = new Dictionary<int, TeacherSubQuestionAnswerModel>();
            var lookuptsqr = new Dictionary<int, TeacherSubQuestionResponse>();
            var lookupmqr = new Dictionary<string, MenterQuestionResponse>();
            this._dbconnection.Query<TeacherQuestionResponse>("GetQuestionListSQ", new[] {          
            typeof(TeacherQuestionResponse),
            typeof(TeacherResponseMedia),
            typeof(TeacherSubQuestionModel),
            typeof(TeacherSubQuestionAnswerModel),
            typeof(TeacherSubQuestionResponse),
            typeof(MenterQuestionResponse)
        }, obj =>
        {
           
            TeacherQuestionResponse tqr = obj[0] as TeacherQuestionResponse;
            TeacherResponseMedia trm = obj[1] as TeacherResponseMedia;
            TeacherSubQuestionModel tsqm = obj[2] as TeacherSubQuestionModel;
            TeacherSubQuestionAnswerModel tsqam = obj[3] as TeacherSubQuestionAnswerModel;
            TeacherSubQuestionResponse tsqr = obj[4] as TeacherSubQuestionResponse;
            MenterQuestionResponse mqr = obj[5] as MenterQuestionResponse;   

            TeacherQuestionResponse teacherQuestionResponse;

            if (!lookuptqr.TryGetValue(tqr.QuestionId, out teacherQuestionResponse))
            {
               
                lookuptqr.Add(tqr.QuestionId, teacherQuestionResponse = tqr);
                if (tqr.UserResponseState == "0")
                {
                    tqr.UserResponseState = "TO_BE_DONE";
                }
                if (tqr.UserResponseState == "1")
                {
                    tqr.UserResponseState = "COMPLETE_AND_SUBMIT";
                }
                if (tqr.UserResponseState == "2")
                {
                    tqr.UserResponseState = "SUBMITTED_FOR_REVIEW";
                }
                if (tqr.UserResponseState == "3")
                {
                    tqr.UserResponseState = "REDO_SUBMISSION";
                }
                if (tqr.UserResponseState == "4")
                {
                    tqr.UserResponseState = "COMPLETED_AND_APPROVED";
                }

            }

            TeacherResponseMedia teacherResponseMedia;

            if (!lookuptrm.TryGetValue(trm.TeacherResponseId, out teacherResponseMedia))
            {
                lookuptrm.Add(trm.TeacherResponseId, teacherResponseMedia = trm);
                if (teacherQuestionResponse.TeacherResponses == null)
                    teacherQuestionResponse.TeacherResponses = new List<TeacherResponseMedia>();

                trm.MediaSource = Util.GetMedia(trm.MediaTypeId, trm.MediaSource);
                teacherQuestionResponse.TeacherResponses.Add(trm);


            }

            MenterQuestionResponse menterQuestionResponse;          

            if (mqr != null)
            {
                if (!lookupmqr.TryGetValue(mqr.MentorResponseId, out menterQuestionResponse))
                {

                    lookupmqr.Add(mqr.MentorResponseId, menterQuestionResponse = mqr);
                    if (teacherResponseMedia.MenterQuestionResponses == null)
                        teacherResponseMedia.MenterQuestionResponses = new List<MenterQuestionResponse>();

                    teacherResponseMedia.MenterQuestionResponses.Add(mqr);
                }
            }           


            TeacherSubQuestionModel teacherSubQuestionModel;

            if (!lookuptsqm.TryGetValue(tsqm.SubQuestionId, out teacherSubQuestionModel))
            {

                lookuptsqm.Add(tsqm.SubQuestionId, teacherSubQuestionModel = tsqm);
                if (teacherResponseMedia.SubQuestionResponses == null)
                    teacherResponseMedia.SubQuestionResponses = new List<TeacherSubQuestionModel>();

                teacherResponseMedia.SubQuestionResponses.Add(tsqm);

            }

            TeacherSubQuestionAnswerModel teacherSubQuestionAnswerModel;

            if (tsqam != null)
            {
                if (!lookuptsqam.TryGetValue(tsqam.SubQuestionOptionId, out teacherSubQuestionAnswerModel))
                {
                     ASSMaxCount = lookuptsqam.Count;

                    lookuptsqam.Add(tsqam.SubQuestionOptionId, teacherSubQuestionAnswerModel = tsqam);
                    if (teacherSubQuestionModel.SubQuestionAnswers == null)
                        teacherSubQuestionModel.SubQuestionAnswers = new List<TeacherSubQuestionAnswerModel>();

                    teacherSubQuestionModel.SubQuestionAnswers.Add(tsqam);

                    if (tsqm.QuestionTypeId == 5)
                    {
                        trm.MaximumScore = tsqam.QtMS;
                    }
                    else if (tsqm.QuestionTypeId == 4)
                    {
                        tsqam.IsCorrect = true;
                        tsqam.CorrectAnswer = tsqam.OptionText;

                        trm.MaximumScore = (tsqam.QtMS) * (tsqam.MaximumScore1);
                    }
                    else
                    {
                        trm.MaximumScore = tsqam.MaximumScore1;
                    }
                    //if(tsqam.MaximumScore1==1)
                    //{
                    //    trm.MaximumScore = tsqam.QtMS;
                    //}
                    //if (tsqam.QtMS == 1)
                    //{
                    //    trm.MaximumScore = tsqam.MaximumScore1;
                    //}

                }
            }




            TeacherSubQuestionResponse teacherSubQuestionResponse;

            if (tsqr != null)
            {
                if (tsqr.SubQuestionOptionId != 0)
                {
                    if (!lookuptsqr.TryGetValue(tsqr.TeacherSubQuestionResponseId, out teacherSubQuestionResponse))
                    {

                        lookuptsqr.Add(tsqr.TeacherSubQuestionResponseId, teacherSubQuestionResponse = tsqr);

                        if (tsqm.QuestionTypeId == 2 || tsqm.QuestionTypeId == 1)
                        {                           
                            if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
                            {
                                tsqam.IsCorrect = true;
                            }
                            else
                            {
                                tsqam.IsCorrect = false;
                            }
                        }

                        if (tsqm.QuestionTypeId == 3)
                        {
                            //if (tsqam.SubQuestionOptionId == tsqom.SubQuestionOptionId)
                            //{
                            //    _maxScoreCount = _maxScoreCount + 1;
                            //    _sq.MaximumScore = _maxScoreCount;
                            //}
                                                      
                           trm.MaximumScore = tsqam.QtMS;
                           
                            tsqam.CorrectAnswer = tsqam.OptionText;
                            tsqam.IsCorrect = true;
                        }                      


                        if (tsqm.QuestionTypeId == 5)
                        {
                           
                            tsqam.CorrectAnswer = tsqam.OptionText;
                            tsqam.IsCorrect = true;
                        }
                    }
                }

            }          

            //if (tsqr != null)
            //{
            //        if (tsqr.SubQuestionOptionId != 0)
            //        {
            //            if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
            //            {
            //                _maxScoreCount = _maxScoreCount + 1;
            //                trm.MaximumScore = _maxScoreCount;
            //            }
            //        }
            //}               


            //splitOn: "SectionTypeId,LessonSectionId,QuestionId,TeacherResponseId,SubQuestionId,SubQuestionOptionId,TeacherSubQuestionResponseId"

            return teacherQuestionResponse; //SectionTypeId, LessonSectionId, QuestionId, TeacherResponseId, SubQuestionId, SubQuestionOptionId, TeacherSubQuestionResponseId,MentorResponseId
     // }, new { _lessonSectionId = lessonSectionId, _teacherid = userId }, splitOn: "LessonId,SectionId,SectionTypeId,LessonSectionId, QuestionId, TeacherResponseId, SubQuestionId, SubQuestionOptionId, TeacherSubQuestionResponseId,MentorResponseId", commandType: CommandType.StoredProcedure).AsQueryable();
        }, new { _lessonSectionId = lessonSectionId, _teacherid = userId }, splitOn: "SectionTypeId,LessonSectionId,LessonId,QuestionId,TeacherResponseId, SubQuestionId, SubQuestionOptionId, TeacherSubQuestionResponseId,MentorResponseId", commandType: CommandType.StoredProcedure).AsQueryable();

            var questionsList = lookuptqr.Values;
            return questionsList.AsEnumerable();
        }

        public IEnumerable<TeacherQuestionResponse> getResultsWithoutSubQuestions1(string lessonSectionId, string userId)
        {
                      int _maxScoreCount = 0;
                var lookuptqr = new Dictionary<string, TeacherQuestionResponse>();
                var lookuptrm = new Dictionary<int, TeacherResponseMedia>();
                //var lookuptsqm = new Dictionary<int, TeacherSubQuestionModel>();
                //var lookuptsqam = new Dictionary<int, TeacherSubQuestionAnswerModel>();
                //var lookuptsqr = new Dictionary<int, TeacherSubQuestionResponse>();
                var lookupmqr = new Dictionary<string, MenterQuestionResponse>();
                    this._dbconnection.Query<TeacherQuestionResponse>("GetQuestionList", new[] {
                    typeof(TeacherQuestionResponse),
                    typeof(TeacherResponseMedia),
                    //typeof(TeacherSubQuestionModel),
                    //typeof(TeacherSubQuestionAnswerModel),
                    // typeof(TeacherSubQuestionResponse),
                     typeof(MenterQuestionResponse)
            }, obj =>
                {
                    TeacherQuestionResponse tqr = obj[0] as TeacherQuestionResponse;
            TeacherResponseMedia trm = obj[1] as TeacherResponseMedia;
            //TeacherSubQuestionModel tsqm = obj[2] as TeacherSubQuestionModel;
            //TeacherSubQuestionAnswerModel tsqam = obj[3] as TeacherSubQuestionAnswerModel;
            //TeacherSubQuestionResponse tsqr = obj[4] as TeacherSubQuestionResponse;
            MenterQuestionResponse mqr = obj[2] as MenterQuestionResponse;

            TeacherQuestionResponse teacherQuestionResponse;

                    if (!lookuptqr.TryGetValue(tqr.QuestionId, out teacherQuestionResponse))
                    {
                        lookuptqr.Add(tqr.QuestionId, teacherQuestionResponse = tqr);
                        if (tqr.UserResponseState == "0")
                        {
                            tqr.UserResponseState = "TO_BE_DONE";
                        }
        if (tqr.UserResponseState == "1")
        {
            tqr.UserResponseState = "COMPLETE_AND_SUBMIT";
        }
        if (tqr.UserResponseState == "2")
        {
            tqr.UserResponseState = "SUBMITTED_FOR_REVIEW";
        }
        if (tqr.UserResponseState == "3")
        {
            tqr.UserResponseState = "REDO_SUBMISSION";
        }
        if (tqr.UserResponseState == "4")
        {
            tqr.UserResponseState = "COMPLETED_AND_APPROVED";
        }

                    }

          TeacherResponseMedia teacherResponseMedia;

        if (!lookuptrm.TryGetValue(trm.TeacherResponseId, out teacherResponseMedia))
        {
            lookuptrm.Add(trm.TeacherResponseId, teacherResponseMedia = trm);
            if (teacherQuestionResponse.TeacherResponses == null)
                teacherQuestionResponse.TeacherResponses = new List<TeacherResponseMedia>();

                        //qm.MediaSource = Util.GetMedia(qm.MediaTypeId, qm.MediaSource);
                        //_sq.Media.Add(qm);
                        trm.MediaSource= Util.GetMedia(trm.MediaTypeId, trm.MediaSource);
                        teacherQuestionResponse.TeacherResponses.Add(trm);

                    }
                  

                    MenterQuestionResponse menterQuestionResponse;

        if (mqr != null)
        {
            if (!lookupmqr.TryGetValue(mqr.MentorResponseId, out menterQuestionResponse))
            {

                lookupmqr.Add(mqr.MentorResponseId, menterQuestionResponse = mqr);
                if (teacherResponseMedia.MenterQuestionResponses == null)
                    teacherResponseMedia.MenterQuestionResponses = new List<MenterQuestionResponse>();

                teacherResponseMedia.MenterQuestionResponses.Add(mqr);
            }
        }


                    //TeacherSubQuestionModel teacherSubQuestionModel;

                    //if (!lookuptsqm.TryGetValue(tsqm.SubQuestionId, out teacherSubQuestionModel))
                    //{

                    //    lookuptsqm.Add(tsqm.SubQuestionId, teacherSubQuestionModel = tsqm);
                    //    if (teacherResponseMedia.SubQuestionResponses == null)
                    //        teacherResponseMedia.SubQuestionResponses = new List<TeacherSubQuestionModel>();

                    //    teacherResponseMedia.SubQuestionResponses.Add(tsqm);

                    //}

                    //TeacherSubQuestionAnswerModel teacherSubQuestionAnswerModel;

                    //if (tsqam != null)
                    //{
                    //    if (!lookuptsqam.TryGetValue(tsqam.SubQuestionOptionId, out teacherSubQuestionAnswerModel))
                    //    {

                    //        lookuptsqam.Add(tsqam.SubQuestionOptionId, teacherSubQuestionAnswerModel = tsqam);
                    //        if (teacherSubQuestionModel.SubQuestionAnswers == null)
                    //            teacherSubQuestionModel.SubQuestionAnswers = new List<TeacherSubQuestionAnswerModel>();

                    //        teacherSubQuestionModel.SubQuestionAnswers.Add(tsqam);

                    //        trm.MaximumScore = tsqam.MaximumScore1;
                    //    }
                    //}




                    //TeacherSubQuestionResponse teacherSubQuestionResponse;

                    //if (tsqr != null)
                    //{
                    //    if (tsqr.SubQuestionOptionId != 0)
                    //    {
                    //        if (!lookuptsqr.TryGetValue(tsqr.TeacherSubQuestionResponseId, out teacherSubQuestionResponse))
                    //        {

                    //            lookuptsqr.Add(tsqr.TeacherSubQuestionResponseId, teacherSubQuestionResponse = tsqr);

                    //            if (tsqm.QuestionTypeId == 2 || tsqm.QuestionTypeId == 1)
                    //            {
                    //                if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
                    //                {
                    //                    tsqam.IsCorrect = true;
                    //                }
                    //                else
                    //                {
                    //                    tsqam.IsCorrect = false;
                    //                }
                    //            }

                    //            if (tsqm.QuestionTypeId == 3)
                    //            {
                    //                tsqam.CorrectAnswer = tsqam.OptionText;
                    //                tsqam.IsCorrect = true;
                    //            }

                    //        }
                    //    }

                    //}


                    //if (tsqr != null)
                    //{
                    //        if (tsqr.SubQuestionOptionId != 0)
                    //        {
                    //            if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
                    //            {
                    //                _maxScoreCount = _maxScoreCount + 1;
                    //                trm.MaximumScore = _maxScoreCount;
                    //            }
                    //        }
                    //}               


                    //splitOn: "SectionTypeId,LessonSectionId,QuestionId,TeacherResponseId,SubQuestionId,SubQuestionOptionId,TeacherSubQuestionResponseId"

                    return teacherQuestionResponse;
                }, new { _lessonSectionId = lessonSectionId, _teacherid = userId }, splitOn: "QuestionId, TeacherResponseId,MentorResponseId", commandType: CommandType.StoredProcedure).AsQueryable();
        var questionsList = lookuptqr.Values;
        return questionsList.AsEnumerable();   
     }




        public IEnumerable<TeacherQuestionResponse> GetQuestionResponseByLessonSectionId(string lessonSectionId, string userId)
        {
            var lookuptqr = new Dictionary<string, TeacherQuestionResponse>();
            var questionList = lookuptqr.Values;
            _dbconnectionnew = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));

            this._dbconnectionnew.Query<int>("select SectionTypeId from lessonsection where LessonSectionId='" + lessonSectionId + "'", new[] {
                typeof(int),
            }, obj =>
            {

                int Sectiontypeid = int.Parse(obj[0].ToString());
                if (Sectiontypeid == 1 || Sectiontypeid == 4 || Sectiontypeid == 5)
                {
                    lookuptqr = getResultsWithSubQuestions(lessonSectionId, userId);
                    questionList = lookuptqr.Values;
                }
                else
                {
                    lookuptqr = getResultsWithoutSubQuestions(lessonSectionId, userId);
                    questionList = lookuptqr.Values;
                }
                return (Sectiontypeid);
            });
            _dbconnectionnew.Close();
            _dbconnectionnew = null;
            return questionList;
        }

        public Dictionary<string, TeacherQuestionResponse> getResultsWithSubQuestions(string lessonSectionId, string userId)
        {
            int _maxScoreCount = 0;
            int ASSMaxCount = 0;
            var lookuptqr = new Dictionary<string, TeacherQuestionResponse>();
            var lookuptrm = new Dictionary<int, TeacherResponseMedia>();
            var lookuptsqm = new Dictionary<int, TeacherSubQuestionModel>();
            var lookuptsqam = new Dictionary<int, TeacherSubQuestionAnswerModel>();
            var lookuptsqr = new Dictionary<int, TeacherSubQuestionResponse>();
            var lookupmqr = new Dictionary<string, MenterQuestionResponse>();
            this._dbconnection.Query<TeacherQuestionResponse>("GetQuestionListSQ", new[] {
            typeof(TeacherQuestionResponse),
            typeof(TeacherResponseMedia),
            typeof(TeacherSubQuestionModel),
            typeof(TeacherSubQuestionAnswerModel),
             typeof(TeacherSubQuestionResponse),
             typeof(MenterQuestionResponse)
        }, obj =>
        {
            TeacherQuestionResponse tqr = obj[0] as TeacherQuestionResponse;
            TeacherResponseMedia trm = obj[1] as TeacherResponseMedia;
            TeacherSubQuestionModel tsqm = obj[2] as TeacherSubQuestionModel;
            TeacherSubQuestionAnswerModel tsqam = obj[3] as TeacherSubQuestionAnswerModel;
            TeacherSubQuestionResponse tsqr = obj[4] as TeacherSubQuestionResponse;
            MenterQuestionResponse mqr = obj[5] as MenterQuestionResponse;

            TeacherQuestionResponse teacherQuestionResponse;

            if (!lookuptqr.TryGetValue(tqr.QuestionId, out teacherQuestionResponse))
            {

                lookuptqr.Add(tqr.QuestionId, teacherQuestionResponse = tqr);
                if (tqr.UserResponseState == "0")
                {
                    tqr.UserResponseState = "TO_BE_DONE";
                }
                if (tqr.UserResponseState == "1")
                {
                    tqr.UserResponseState = "COMPLETE_AND_SUBMIT";
                }
                if (tqr.UserResponseState == "2")
                {
                    tqr.UserResponseState = "SUBMITTED_FOR_REVIEW";
                }
                if (tqr.UserResponseState == "3")
                {
                    tqr.UserResponseState = "REDO_SUBMISSION";
                }
                if (tqr.UserResponseState == "4")
                {
                    tqr.UserResponseState = "COMPLETED_AND_APPROVED";
                }

            }

            TeacherResponseMedia teacherResponseMedia;

            if (!lookuptrm.TryGetValue(trm.TeacherResponseId, out teacherResponseMedia))
            {
                lookuptrm.Add(trm.TeacherResponseId, teacherResponseMedia = trm);
                if (teacherQuestionResponse.TeacherResponses == null)
                    teacherQuestionResponse.TeacherResponses = new List<TeacherResponseMedia>();

                trm.MediaSource = Util.GetMedia(trm.MediaTypeId, trm.MediaSource);
                teacherQuestionResponse.TeacherResponses.Add(trm);


            }

            MenterQuestionResponse menterQuestionResponse;

            if (mqr != null)
            {
                if (!lookupmqr.TryGetValue(mqr.MentorResponseId, out menterQuestionResponse))
                {

                    lookupmqr.Add(mqr.MentorResponseId, menterQuestionResponse = mqr);
                    if (teacherResponseMedia.MenterQuestionResponses == null)
                        teacherResponseMedia.MenterQuestionResponses = new List<MenterQuestionResponse>();

                    teacherResponseMedia.MenterQuestionResponses.Add(mqr);
                }
            }



            TeacherSubQuestionModel teacherSubQuestionModel;

            if (!lookuptsqm.TryGetValue(tsqm.SubQuestionId, out teacherSubQuestionModel))
            {

                lookuptsqm.Add(tsqm.SubQuestionId, teacherSubQuestionModel = tsqm);
                if (teacherResponseMedia.SubQuestionResponses == null)
                    teacherResponseMedia.SubQuestionResponses = new List<TeacherSubQuestionModel>();

                teacherResponseMedia.SubQuestionResponses.Add(tsqm);

            }

            TeacherSubQuestionAnswerModel teacherSubQuestionAnswerModel;

            if (tsqam != null)
            {
                if (!lookuptsqam.TryGetValue(tsqam.SubQuestionOptionId, out teacherSubQuestionAnswerModel))
                {
                    ASSMaxCount = lookuptsqam.Count;

                    lookuptsqam.Add(tsqam.SubQuestionOptionId, teacherSubQuestionAnswerModel = tsqam);
                    if (teacherSubQuestionModel.SubQuestionAnswers == null)
                        teacherSubQuestionModel.SubQuestionAnswers = new List<TeacherSubQuestionAnswerModel>();

                    teacherSubQuestionModel.SubQuestionAnswers.Add(tsqam);

                    if (tsqm.QuestionTypeId == 5)
                    {
                        trm.MaximumScore = tsqam.QtMS;
                    }
                    else if (tsqm.QuestionTypeId == 4)
                    {
                        tsqam.IsCorrect = true;
                        tsqam.CorrectAnswer = tsqam.OptionText;

                        trm.MaximumScore = (tsqam.QtMS) * (tsqam.MaximumScore1);
                    }
                    else
                    {
                        trm.MaximumScore = tsqam.MaximumScore1;
                    }
                    //if(tsqam.MaximumScore1==1)
                    //{
                    //    trm.MaximumScore = tsqam.QtMS;
                    //}
                    //if (tsqam.QtMS == 1)
                    //{
                    //    trm.MaximumScore = tsqam.MaximumScore1;
                    //}

                }
            }




            TeacherSubQuestionResponse teacherSubQuestionResponse;

            if (tsqr != null)
            {
                if (tsqr.SubQuestionOptionId != 0)
                {
                    if (!lookuptsqr.TryGetValue(tsqr.TeacherSubQuestionResponseId, out teacherSubQuestionResponse))
                    {

                        lookuptsqr.Add(tsqr.TeacherSubQuestionResponseId, teacherSubQuestionResponse = tsqr);

                        if (tsqm.QuestionTypeId == 2 || tsqm.QuestionTypeId == 1)
                        {
                            if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
                            {
                                tsqam.IsCorrect = true;
                            }
                            else
                            {
                                tsqam.IsCorrect = false;
                            }
                        }

                        if (tsqm.QuestionTypeId == 3)
                        {
                            //if (tsqam.SubQuestionOptionId == tsqom.SubQuestionOptionId)
                            //{
                            //    _maxScoreCount = _maxScoreCount + 1;
                            //    _sq.MaximumScore = _maxScoreCount;
                            //}

                            trm.MaximumScore = tsqam.QtMS;

                            tsqam.CorrectAnswer = tsqam.OptionText;
                            tsqam.IsCorrect = true;
                        }


                        if (tsqm.QuestionTypeId == 5)
                        {

                            tsqam.CorrectAnswer = tsqam.OptionText;
                            tsqam.IsCorrect = true;
                        }
                    }
                }

            }

            //if (tsqr != null)
            //{
            //        if (tsqr.SubQuestionOptionId != 0)
            //        {
            //            if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
            //            {
            //                _maxScoreCount = _maxScoreCount + 1;
            //                trm.MaximumScore = _maxScoreCount;
            //            }
            //        }
            //}               


            //splitOn: "SectionTypeId,LessonSectionId,QuestionId,TeacherResponseId,SubQuestionId,SubQuestionOptionId,TeacherSubQuestionResponseId"

            return teacherQuestionResponse;
        }, new { _lessonSectionId = lessonSectionId, _teacherid = userId }, splitOn: "SectionTypeId, LessonSectionId, QuestionId, TeacherResponseId, SubQuestionId, SubQuestionOptionId, TeacherSubQuestionResponseId,MentorResponseId", commandType: CommandType.StoredProcedure).AsQueryable();
            var questionsList = lookuptqr.Values;
            return lookuptqr;
        }

        public Dictionary<string, TeacherQuestionResponse> getResultsWithoutSubQuestions(string lessonSectionId, string userId)
        {
            int _maxScoreCount = 0;
            var lookuptqr = new Dictionary<string, TeacherQuestionResponse>();
            var lookuptrm = new Dictionary<int, TeacherResponseMedia>();
            //var lookuptsqm = new Dictionary<int, TeacherSubQuestionModel>();
            //var lookuptsqam = new Dictionary<int, TeacherSubQuestionAnswerModel>();
            //var lookuptsqr = new Dictionary<int, TeacherSubQuestionResponse>();
            var lookupmqr = new Dictionary<string, MenterQuestionResponse>();
            this._dbconnection.Query<TeacherQuestionResponse>("GetQuestionList", new[] {
                    typeof(TeacherQuestionResponse),
                    typeof(TeacherResponseMedia),
                    //typeof(TeacherSubQuestionModel),
                    //typeof(TeacherSubQuestionAnswerModel),
                    // typeof(TeacherSubQuestionResponse),
                     typeof(MenterQuestionResponse)
            }, obj =>
            {
                TeacherQuestionResponse tqr = obj[0] as TeacherQuestionResponse;
                TeacherResponseMedia trm = obj[1] as TeacherResponseMedia;
                //TeacherSubQuestionModel tsqm = obj[2] as TeacherSubQuestionModel;
                //TeacherSubQuestionAnswerModel tsqam = obj[3] as TeacherSubQuestionAnswerModel;
                //TeacherSubQuestionResponse tsqr = obj[4] as TeacherSubQuestionResponse;
                MenterQuestionResponse mqr = obj[2] as MenterQuestionResponse;

                TeacherQuestionResponse teacherQuestionResponse;

                if (!lookuptqr.TryGetValue(tqr.QuestionId, out teacherQuestionResponse))
                {
                    lookuptqr.Add(tqr.QuestionId, teacherQuestionResponse = tqr);
                    if (tqr.UserResponseState == "0")
                    {
                        tqr.UserResponseState = "TO_BE_DONE";
                    }
                    if (tqr.UserResponseState == "1")
                    {
                        tqr.UserResponseState = "COMPLETE_AND_SUBMIT";
                    }
                    if (tqr.UserResponseState == "2")
                    {
                        tqr.UserResponseState = "SUBMITTED_FOR_REVIEW";
                    }
                    if (tqr.UserResponseState == "3")
                    {
                        tqr.UserResponseState = "REDO_SUBMISSION";
                    }
                    if (tqr.UserResponseState == "4")
                    {
                        tqr.UserResponseState = "COMPLETED_AND_APPROVED";
                    }

                }

                TeacherResponseMedia teacherResponseMedia;

                if (!lookuptrm.TryGetValue(trm.TeacherResponseId, out teacherResponseMedia))
                {
                    lookuptrm.Add(trm.TeacherResponseId, teacherResponseMedia = trm);
                    if (teacherQuestionResponse.TeacherResponses == null)
                        teacherQuestionResponse.TeacherResponses = new List<TeacherResponseMedia>();

                    //qm.MediaSource = Util.GetMedia(qm.MediaTypeId, qm.MediaSource);
                    //_sq.Media.Add(qm);
                    trm.MediaSource = Util.GetMedia(trm.MediaTypeId, trm.MediaSource);
                    teacherQuestionResponse.TeacherResponses.Add(trm);

                }


                MenterQuestionResponse menterQuestionResponse;

                if (mqr != null)
                {
                    if (!lookupmqr.TryGetValue(mqr.MentorResponseId, out menterQuestionResponse))
                    {

                        lookupmqr.Add(mqr.MentorResponseId, menterQuestionResponse = mqr);
                        if (teacherResponseMedia.MenterQuestionResponses == null)
                            teacherResponseMedia.MenterQuestionResponses = new List<MenterQuestionResponse>();

                        teacherResponseMedia.MenterQuestionResponses.Add(mqr);
                    }
                }


                //TeacherSubQuestionModel teacherSubQuestionModel;

                //if (!lookuptsqm.TryGetValue(tsqm.SubQuestionId, out teacherSubQuestionModel))
                //{

                //    lookuptsqm.Add(tsqm.SubQuestionId, teacherSubQuestionModel = tsqm);
                //    if (teacherResponseMedia.SubQuestionResponses == null)
                //        teacherResponseMedia.SubQuestionResponses = new List<TeacherSubQuestionModel>();

                //    teacherResponseMedia.SubQuestionResponses.Add(tsqm);

                //}

                //TeacherSubQuestionAnswerModel teacherSubQuestionAnswerModel;

                //if (tsqam != null)
                //{
                //    if (!lookuptsqam.TryGetValue(tsqam.SubQuestionOptionId, out teacherSubQuestionAnswerModel))
                //    {

                //        lookuptsqam.Add(tsqam.SubQuestionOptionId, teacherSubQuestionAnswerModel = tsqam);
                //        if (teacherSubQuestionModel.SubQuestionAnswers == null)
                //            teacherSubQuestionModel.SubQuestionAnswers = new List<TeacherSubQuestionAnswerModel>();

                //        teacherSubQuestionModel.SubQuestionAnswers.Add(tsqam);

                //        trm.MaximumScore = tsqam.MaximumScore1;
                //    }
                //}




                //TeacherSubQuestionResponse teacherSubQuestionResponse;

                //if (tsqr != null)
                //{
                //    if (tsqr.SubQuestionOptionId != 0)
                //    {
                //        if (!lookuptsqr.TryGetValue(tsqr.TeacherSubQuestionResponseId, out teacherSubQuestionResponse))
                //        {

                //            lookuptsqr.Add(tsqr.TeacherSubQuestionResponseId, teacherSubQuestionResponse = tsqr);

                //            if (tsqm.QuestionTypeId == 2 || tsqm.QuestionTypeId == 1)
                //            {
                //                if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
                //                {
                //                    tsqam.IsCorrect = true;
                //                }
                //                else
                //                {
                //                    tsqam.IsCorrect = false;
                //                }
                //            }

                //            if (tsqm.QuestionTypeId == 3)
                //            {
                //                tsqam.CorrectAnswer = tsqam.OptionText;
                //                tsqam.IsCorrect = true;
                //            }

                //        }
                //    }

                //}


                //if (tsqr != null)
                //{
                //        if (tsqr.SubQuestionOptionId != 0)
                //        {
                //            if (tsqr.SubQuestionOptionId == tsqam.SubQuestionOptionId)
                //            {
                //                _maxScoreCount = _maxScoreCount + 1;
                //                trm.MaximumScore = _maxScoreCount;
                //            }
                //        }
                //}               


                //splitOn: "SectionTypeId,LessonSectionId,QuestionId,TeacherResponseId,SubQuestionId,SubQuestionOptionId,TeacherSubQuestionResponseId"

                return teacherQuestionResponse;
            }, new { _lessonSectionId = lessonSectionId, _teacherid = userId }, splitOn: "QuestionId, TeacherResponseId,MentorResponseId", commandType: CommandType.StoredProcedure).AsQueryable();
            var questionsList = lookuptqr.Values;
            return lookuptqr;
        }





        //public IEnumerable<TeacherQuestionResponse> GetQuestionResponseByLessonSectionId(string lessonSectionId, string userId)
        //{
        //    var includeProperties = new StringBuilder(Constants.SubQuestionsProperty);
        //    includeProperties.Append($",{Constants.TeacherResponsesProperty}");
        //    includeProperties.Append($",{Constants.TeacherResponseStatusProperty}");
        //    includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.MentorResponseProperty}");
        //    includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.SubQuestionAnswersProperty}");
        //    includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.TeacherSubQuestionResponsesProperty}");
        //    includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.MentorResponseProperty}.{Constants.MentorProperty}");
        //    includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.TeacherSubQuestionResponsesProperty}.{Constants.SubQuestionOptionProperty}");
        //    includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.MentorResponseProperty}.{Constants.MentorProperty}.{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}");
        //    includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.MediaProperty}");


        //    var questionsList = _question
        //         .Filter(filter: x => string.Equals(x.LessonSectionId, lessonSectionId, StringComparison.OrdinalIgnoreCase), includeProperties: includeProperties.ToString())

        //       // .Filter(filter: x => string.Equals(x.LessonSectionId, lessonSectionId, StringComparison.OrdinalIgnoreCase), includeProperties: includeProperties.ToString())
        //       .Select(x => new TeacherQuestionResponse
        //       {
        //           QuestionId = x.QuestionId,
        //           UserResponseState = GetUserResponseState(x.QuestionId, x.TeacherResponseStatus),
        //           TeacherResponses = x
        //           .TeacherResponses
        //           .Where(y => y.QuestionId == x.QuestionId && y.TeacherId == userId)
        //           .OrderByDescending(y => y.TeacherResponseId)
        //           .Take(2)
        //           .Select(z => new TeacherResponseMedia
        //           {
        //               TeacherResponseId = z.TeacherResponseId,
        //               RecommendedAttempts = x.RecommendedAttempts ?? 0,
        //               SecondaryAttempts = x.SecondaryAttempts ?? 0,
        //               Attempts = GetAttempts(x.RecommendedAttempts ?? 0, z.Attempts ?? 0),
        //               Score = z.Score ?? 0,
        //               MaximumScore = x.SubQuestions != null && x.SubQuestions.Any() ? GetMaxScore(x.SubQuestions) : 0,
        //               MediaSource = z.Media == null ? string.Empty : Util.GetMedia(Constants.MediaTypeId, z.Media.MediaSource),
        //               CreatedDate = z?.Created,
        //               ResponseText = z?.ResponseText,
        //               MenterQuestionResponses = x?
        //               .TeacherResponses
        //               .Where(x => x?.MentorResponse?.TeacherResponseId == z?.TeacherResponseId)
        //               .Select(z => new MenterQuestionResponse
        //               {
        //                   MentorComments = z?.MentorResponse?.MentorComments,
        //                   MentorId = z?.MentorResponse?.MentorId,
        //                   MentorName = z?.MentorResponse?.Mentor?.MentorNavigation?.UserProfile?.Name,
        //                   CreatedDate = z?.MentorResponse?.Created
        //               }),
        //               SubQuestionResponses = x.SubQuestions != null && z.TeacherSubQuestionResponses != null ?
        //                   GetSubQuestionResponses(x.SubQuestions, z.TeacherSubQuestionResponses) : null
        //           })
        //       });

        //    if (questionsList == null || questionsList.Count() < 1)
        //        return null;

        //    return questionsList;
        //}

        //private IEnumerable<TeacherSubQuestionModel> GetSubQuestionResponses(IEnumerable<SubQuestion> subQuestions, IEnumerable<TeacherSubQuestionResponse> subQuestionResponses)
        //    => subQuestions.Select(x => new TeacherSubQuestionModel
        //    {
        //        SubQuestionId = x.SubQuestionId,
        //        QuestionText = x.QuestionText,
        //        SubQuestionAnswers = GetSubQuestionAnswers(x.SubQuestionId, x.QuestionTypeId,
        //        x.SubQuestionAnswers.OrderBy(x => x.AnswerOrder).ToList(), subQuestionResponses)
        //    });

        private IEnumerable<TeacherSubQuestionAnswerModel> GetSubQuestionAnswers(int subQuestionId, int questionType, List<SubQuestionAnswer> answers, 
            IEnumerable<TeacherSubQuestionResponse> subQuestionResponses)
        {
            var subQuestionAnswers = new List<TeacherSubQuestionAnswerModel>();

            var userResponse = subQuestionResponses.Where(x => x.SubQuestionId == subQuestionId).OrderBy(x => x.AnswerOrder).ToList();

            switch(questionType)
            {
                case 1: 
                    subQuestionAnswers.Add(new TeacherSubQuestionAnswerModel
                    {
                        SubQuestionOptionId = userResponse?.FirstOrDefault()?.SubQuestionOptionId ?? 0,
                        OptionText = userResponse?.FirstOrDefault()?.SubQuestionOption?.OptionText ?? string.Empty,
                        IsCorrect = userResponse?.FirstOrDefault()?.SubQuestionOptionId == answers?.FirstOrDefault()?.SubQuestionOptionId,
                        CorrectAnswer = answers?.FirstOrDefault()?.SubQuestionOption?.OptionText
                    });
                    break;
                case 2:
                    subQuestionAnswers.AddRange(userResponse?.Select(x => new TeacherSubQuestionAnswerModel
                    {
                        SubQuestionOptionId = x?.SubQuestionOptionId ?? 0,
                        OptionText = x?.SubQuestionOption?.OptionText ?? string.Empty,
                        IsCorrect = answers?.Any(y => y?.SubQuestionOptionId == x?.SubQuestionOptionId) ?? false
                    }));
                    break;
                case 3:
                    var i = 0;

                    foreach (var answer in userResponse)
                    {
                        TeacherSubQuestionAnswerModel teacherSubQuestionAnswerModel = new TeacherSubQuestionAnswerModel
                        {
                            SubQuestionOptionId = answer?.SubQuestionOptionId ?? 0,
                            OptionText = answer?.SubQuestionOption?.OptionText ?? string.Empty,
                            CorrectAnswer = answers[i].SubQuestionOption.OptionText
                        };
                        teacherSubQuestionAnswerModel.IsCorrect = teacherSubQuestionAnswerModel.OptionText?.ToLowerInvariant()
                            == teacherSubQuestionAnswerModel.CorrectAnswer.ToLowerInvariant();
                        subQuestionAnswers.Add(teacherSubQuestionAnswerModel);
                        i++;
                    }
                    break;

                case 4:
                    var index = 0;

                    foreach (var answer in userResponse)
                    {
                        subQuestionAnswers.Add(new TeacherSubQuestionAnswerModel
                        {
                            SubQuestionOptionId = answer?.SubQuestionOptionId ?? 0,
                            OptionText = answer?.SubQuestionOption?.OptionText ?? string.Empty,
                            IsCorrect = userResponse[index].SubQuestionOptionId == answers[index].SubQuestionOptionId,
                            CorrectAnswer = answers[index].SubQuestionOption.OptionText
                        });

                        index++;
                    }
                    break;
            }

            return subQuestionAnswers;
        }

        private int GetMaxScore(IEnumerable<SubQuestion> subQuestions)
        {
            var maxScore = 0;

            foreach(var subQuestion in subQuestions)
            {
                switch(subQuestion.QuestionTypeId)
                {
                    case 1:
                    case 3:
                        maxScore++;
                        break;
                    default:
                        maxScore += subQuestion.SubQuestionAnswers.Count();
                        break;
                }
            }

            return maxScore;
        }

        public TeacherDashboardQuestionsListModel GetQuestion(string userId, string questionId, int languageId)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.QuestionHintsProperty);
            includeProperties.Append($",{Constants.SubQuestionsProperty}");
            includeProperties.Append($",{Constants.QuestionMediasProperty}");
            includeProperties.Append($",{Constants.TeacherResponsesProperty}");
            includeProperties.Append($",{Constants.QuestionHintsProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.QuestionMediasProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.SubQuestionOptionsProperty}");
            includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.TeacherSubQuestionResponsesProperty}");
            includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.SubQuestionAnswersProperty}");
            includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.SubQuestionAnswersProperty}.{Constants.SubQuestionOptionProperty}");

            var question = _unitOfWork.Repository<Question>()
                .Filter(filter: x => string.Equals(questionId, x.QuestionId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString()).OrderByDescending(x=>x.LastUpdated)
                .FirstOrDefault();

            if (question == null)
                return null;

            return new TeacherDashboardQuestionsListModel
            {
                QuestionText = question?.QuestionText,
                Hint = GetQuestionHints(question),
                MediaSource = GetQuestionMedias(languageId, question),
                SubQuestionDetails = GetSubQuestionDetails(userId, question)
            };
        }
        private IEnumerable<QuestionHintText> GetQuestionHints(Question question)
            => question
            .QuestionHints
            .Select(x => new QuestionHintText
            {
                QuestionHintId = x.QuestionHintId,
                HintText = x.HintText,
                MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
            });
        private string GetUserResponseState( string questionId,IEnumerable<TeacherResponseStatu> responseStatus)
        {
            var currentStatus = responseStatus.Where(x => x.QuestionId == questionId).OrderByDescending(x => x.Created).GroupBy(x => x.Question).Select(x => x.FirstOrDefault()).FirstOrDefault();
            return currentStatus == null ? string.Empty : ((Enums.ResponseStatus)currentStatus.ResponseState).ToString();
        }
        private IEnumerable<TeacherQuestionMedia> GetQuestionMedias(int languageId, Question question)
            => question
            .QuestionMedias
            .Where(x => x.LanguageId == languageId || x.LanguageId == Constants.EnglishLanguage)
            .Select(x => new TeacherQuestionMedia
            {
                LanguageId = x.LanguageId,
                MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
            });

        private SubQuestionDetails GetSubQuestionDetails(string userId, Question question)
        {
            if(question.SubQuestions == null && !question.SubQuestions.Any())
            {
                return null;
            }

            var subQuestionDetails = new SubQuestionDetails();

            if (!question
                .TeacherResponses
                .Any(x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase) && 
                    string.Equals(x.QuestionId, question.QuestionId, StringComparison.OrdinalIgnoreCase)))
            {
                subQuestionDetails.SubQuestions = GetSubQuestions(question);
            }
            else
            {
                var questionResponse = question
                .TeacherResponses
                .First(x => string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase) && 
                    string.Equals(x.QuestionId, question.QuestionId, StringComparison.OrdinalIgnoreCase));

                subQuestionDetails.RecommendedAttempts = question.RecommendedAttempts ?? 0;
                subQuestionDetails.SecondaryAttempts = question.SecondaryAttempts ?? 0;
                subQuestionDetails.Attempts = GetAttempts(subQuestionDetails.RecommendedAttempts, questionResponse.Attempts ?? 0);
                subQuestionDetails.Score = questionResponse.Score ?? 0;
                subQuestionDetails.TotalQuestion = question.SubQuestions.Count();
                subQuestionDetails.PerfectScore = IsPerfectScore(question.SubQuestions, subQuestionDetails.Score);
                //if(subQuestionDetails.PerfectScore)
                //{
                    subQuestionDetails.Results = GetSubQuestionResults(question,questionResponse.TeacherResponseId);
                //}
               // else
                //{
                    subQuestionDetails.SubQuestions = GetSubQuestions(question);
                //}
            }

            return subQuestionDetails;
        }

        private int GetAttempts(int recommendedAttempts, int numberOfAttempts)
            => recommendedAttempts > 0 && numberOfAttempts >= recommendedAttempts ? 
            numberOfAttempts % recommendedAttempts : numberOfAttempts;

        private IEnumerable<SubQuestionModel> GetSubQuestions(Question question)
            => question
            .SubQuestions
            .Select(x => new SubQuestionModel
            {
                SubQuestionId = x.SubQuestionId,
                QuestionTypeId = x.QuestionTypeId,
                SubQuestionOrder = x.QuestionOrder,
                SubQuestionText = x.QuestionText,
                TotalAnswer = x.SubQuestionAnswers.Count(),
                Options = x.SubQuestionOptions  
                .Select(x => new SubQuestionOptionModel
                {
                    SubQuestionOptionId = x.SubQuestionOptionId,
                    SubQuestionOptionOrder = x.OptionOrder,
                    SubQuestionOptionText = x.OptionText
                })
            });

        private IEnumerable<SubQuestionResultModel> GetSubQuestionResults(Question question, int teacherresponseid)
        {            
            IEnumerable<SubQuestionResultModel> res =null;
           res=question
            .SubQuestions
            .Select(x => new SubQuestionResultModel
            {
                Question = x.QuestionText,
               // Answer = string.Join(", ", x.SubQuestionAnswers.Select(x => x.SubQuestionOption.OptionText))
                //Answer = string.Join(", ", x.TeacherSubQuestionResponses.Where(x=>x.SubQuestionOptionId==x.SubQuestionOption.SubQuestionOptionId).Select(x => x.SubQuestionOption.OptionText).Select(x=>x.Last()))
                Answer = string.Join(", ", x.TeacherSubQuestionResponses.Where(x=>x.TeacherResponseId==teacherresponseid).Select(x => x.SubQuestionOption.OptionText))

            });
            return res;
        }
        public bool UpsertQuestionResponse(string userId, string lessonSectionId, string mediaFilePath, IEnumerable<QuestionResponseCommand> questionResponses,int Attempts,bool IsperfectScore)
        {
            bool insertNotification = false;
            string lesssectionid = "";
            int ResState = 0;
            bool resStatus = false;
            string path = @"C:\PYF\Logs\PYFTestLog.txt";
            int userAttempts = 0;
            bool perfectscoreval = false;
            var includeProperties = new StringBuilder(Constants.TeacherResponsesProperty);
            includeProperties.Append($",{Constants.SubQuestionsProperty}");
            includeProperties.Append($",{Constants.TeacherResponseStatusProperty}");
            includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.SubQuestionAnswersProperty}");
            includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.SubQuestionAnswersProperty}.{Constants.SubQuestionOptionProperty}");

            var questions = _unitOfWork.Repository<Question>()
                .Filter(filter: x => string.Equals(x.LessonSectionId, lessonSectionId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .Where(x => questionResponses.Any(y => string.Equals(y.QuestionId, x.QuestionId, StringComparison.OrdinalIgnoreCase)));



            System.DateTime newDate1 = DateTime.Now;
            string appendText1 = "Post Data"+ Environment.NewLine;
            appendText1=newDate1.ToString()+"  "+"TeacherId/"+ userId+ "  " + "Count---------"+questionResponses.Count()+Environment.NewLine;
            File.AppendAllText(path, appendText1);

            foreach (var response in questionResponses)
            {
                if (Attempts == 0)
                {
                    Attempts = 6;
                }
                else if(Attempts==6)
                    {
                    Attempts = 0;
                }

            var userResponse = questions
                    .SelectMany(x => x.TeacherResponses)
                    .FirstOrDefault(x => string.Equals(x.QuestionId, response.QuestionId, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase));

                if(userResponse == null)
                {
                    userResponse = AddQuestionResponseAndStatus(userId, mediaFilePath, response, questions.SelectMany(x => x.SubQuestions),Attempts);
                    userAttempts = userResponse.Attempts ?? 0;


                   string appendText = newDate1 + "  " + "TeacherId/" + userId + "/" + "QuestionId/"+response.QuestionId + Environment.NewLine;
                    File.AppendAllText(path, appendText);
                }
                else
                {
                    var questionStatus = questions
                        .SelectMany(x => x.TeacherResponseStatus)
                        .Where(x => string.Equals(x.QuestionId, response.QuestionId, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(x => x.LastUpdated)
                        .FirstOrDefault();

                    if (questionStatus == null || questionStatus.ResponseState == (int)Enums.ResponseStatus.COMPLETED_AND_APPROVED
                        || questionStatus.ResponseState == (int)Enums.ResponseStatus.SUBMITTED_FOR_REVIEW
                        || questionStatus.ResponseState == (int)Enums.ResponseStatus.TO_BE_DONE)
                            
                    {               

                        return false;
                    }
                    string lsid = "";
                    int rstid = 0;

                    var RSLessonsecId = _question
                  .Filter(filter: x => string.Equals(x.QuestionId,response.QuestionId, StringComparison.OrdinalIgnoreCase), null)
                  .Select(x => x.LessonSectionId);

                    foreach (var rslsid in RSLessonsecId)
                    {
                        lsid = rslsid;
                        lesssectionid = lsid;
                    }
                    var RSSecTypId = _lessonSection
                      .Filter(filter: x => string.Equals(x.LessonSectionId, lsid, StringComparison.OrdinalIgnoreCase), null)
                      .Select(x => x.SectionTypeId);

                    foreach (var stid in RSSecTypId)
                    {
                        rstid = stid;
                    }
                    ResState = questionStatus.ResponseState;
                    var updatedResponse = (questionStatus.ResponseState == (int)Enums.ResponseStatus.REDO_SUBMISSION ) && (rstid==2 || rstid==3)  ?
                        _unitOfWork.Repository<TeacherResponse>().Add(AddQuestionResponse(userId, mediaFilePath, true, response, questions.SelectMany(x => x.SubQuestions),Attempts))
                        : UpdateQuestionResponse(mediaFilePath, response, userResponse, questions.SelectMany(x => x.SubQuestions),Attempts);

                   //var updatedResponse =UpdateQuestionResponse(mediaFilePath, response, userResponse, questions.SelectMany(x => x.SubQuestions));

                    userAttempts = updatedResponse.Attempts ?? 0;                    
                }
               
                //auto submit
                
                if (UpdateResponseStatus(questions.SelectMany(x => x.SubQuestions).Any(), userAttempts,
                    questions.First(x => string.Equals(x.QuestionId, response.QuestionId, StringComparison.OrdinalIgnoreCase))))
                {
                    _unitOfWork
                        .Repository<TeacherResponseStatu>()
                        .Add(GetTeacherResponseStatus(userId, userResponse.QuestionId, (int)Enums.ResponseStatus.SUBMITTED_FOR_REVIEW, userId));
                   
                    resStatus = true;
                    insertNotification = true;
                }
                int score = Convert.ToInt32(userResponse.Score);
               perfectscoreval = IsPerfectScore(questions.SelectMany(x => x.SubQuestions),score);
                // if (userResponse.Score==6 && userAttempts<6) 
                if (resStatus == false)
                {
                    var QuesSecAtt = questions.First(x => string.Equals(x.QuestionId, response.QuestionId, StringComparison.OrdinalIgnoreCase));

                    if (perfectscoreval == true && userAttempts < QuesSecAtt.SecondaryAttempts)
                    {
                        _unitOfWork
                            .Repository<TeacherResponseStatu>()
                            .Add(GetTeacherResponseStatus(userId, userResponse.QuestionId, (int)Enums.ResponseStatus.SUBMITTED_FOR_REVIEW, userId));

                        insertNotification = true;

                    }
                    else if ((perfectscoreval == false || userAttempts < QuesSecAtt.RecommendedAttempts) && ResState != (int)Enums.ResponseStatus.REDO_SUBMISSION)
                    {
                        _unitOfWork
                               .Repository<TeacherResponseStatu>()
                               .Add(GetTeacherResponseStatus(userId, userResponse.QuestionId, (int)Enums.ResponseStatus.COMPLETE_AND_SUBMIT, userId));

                    }

                }
               
            }

            if (insertNotification == true)
            {
                var teacher = _teacher
                             .Find(x => (string.Equals(x.TeacherId, userId, StringComparison.OrdinalIgnoreCase)));
                var userProfile = _userProfile
                      .Find(x => (string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase)));

                int wrmsg = 0;
                var question = new Question();
                foreach (var res in questionResponses)
                {
                    question = _question.Find(x => (string.Equals(x.QuestionId, res.QuestionId)));
                    wrmsg = question.QuestionOrder;
                }
                var lessonsection = _lessonSection
                    .Find(x => (string.Equals(x.LessonSectionId, question.LessonSectionId, StringComparison.OrdinalIgnoreCase)));


                var lesson = _lesson
                   .Find(x => (string.Equals(x.LessonId, lessonsection.LessonId, StringComparison.OrdinalIgnoreCase)));

                var sectiontype = _sectionType
                   .Find(x => (string.Equals(x.SectionTypeId, lessonsection.SectionTypeId)));
               
               
                string from = userId;
                string to = teacher.MentorId;
                int roleid = 5;
                string message = "";
                if (sectiontype.SectionTypeDescription == "Write Right")
                {
                    message = userProfile.Name + "  has submitted the  " + lesson.LessonName + "  " + sectiontype.SectionTypeDescription + " section" + "  Component" + wrmsg + " for review";
                }
                else
                {
                    message = userProfile.Name + "  has submitted the  " + lesson.LessonName + "  " + sectiontype.SectionTypeDescription + " section  for review";
                }
                string created_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int status = 1;
                _unitOfWork
                    .Repository<Notifications>()
                    .Add(GetNotificationdetails(from, to, roleid, message, created_date, status));

            }
            return _unitOfWork.Commit() > 0;
        }
        private Notifications GetNotificationdetails(string from, string to, int roleid, string message, string created_date, int status)
           => new Notifications
           {
               msgfrom = from,
               msgto = to,
               roleid = roleid,
               message = message,
               created_date = created_date,
               status = status
           };
        private bool UpdateResponseStatus(bool hasSubQuestions, int userAttempts, Question question)
            => hasSubQuestions ?
            // userAttempts == question.RecommendedAttempts || userAttempts >= (question.RecommendedAttempts + question.SecondaryAttempts)

            // Comment :auto submit for every 6 attempts 
            ( userAttempts % question.RecommendedAttempts) == 0 && userAttempts<= (question.RecommendedAttempts + question.SecondaryAttempts)
            : true;

        private TeacherResponse AddQuestionResponseAndStatus(string userId, string mediaFilePath, QuestionResponseCommand userResponse, IEnumerable<SubQuestion> subQuestions,int Attempts)
        {
            //_unitOfWork
                //.Repository<TeacherResponseStatu>()
                //.Add(GetTeacherResponseStatus(userId,
                  //  userResponse.QuestionId,
                  //  subQuestions == null || !subQuestions.Any() ? (int)Enums.ResponseStatus.SUBMITTED_FOR_REVIEW : (int)Enums.ResponseStatus.COMPLETE_AND_SUBMIT,
                  //  userId));            

            return _unitOfWork
                .Repository<TeacherResponse>()
                .Add(AddQuestionResponse(userId, mediaFilePath, false, userResponse, subQuestions,Attempts));
        }

        private TeacherResponseStatu GetTeacherResponseStatus(string userId, string questionId, int responseState, string updatedBy)
            => new TeacherResponseStatu
            {
                TeacherId = userId,
                QuestionId = questionId,
                ResponseState = responseState,
                UpdatedBy = updatedBy,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };

        private TeacherResponse AddQuestionResponse(string userId, string mediaFilePath, bool isRevised, QuestionResponseCommand userResponse, IEnumerable<SubQuestion> subQuestions,int Attempts)
        {
            var questionResponse = new TeacherResponse();

            if (!string.IsNullOrEmpty(userResponse.MediaStream))
            {
                questionResponse.MediaId = Guid.NewGuid().ToString();
                questionResponse.Media = new Media
                {
                    MediaId = questionResponse.MediaId,
                    MediaTypeId = Constants.MediaTypeId,
                    MediaSource = Util.SaveMediaToFileSystem(userResponse.MediaStream, mediaFilePath, $"{userResponse.QuestionId}_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.mp3")
                };
            }
            questionResponse.QuestionId = userResponse.QuestionId;
            questionResponse.TeacherId = userId;
            questionResponse.ResponseText = userResponse.ResponseText;
            questionResponse.IsRevised = isRevised;
            if (subQuestions.Any() && userResponse.SubQuestions.Any())
            {
                questionResponse.Score = CalculateScore(subQuestions, userResponse.SubQuestions);
                questionResponse.Attempts = Attempts;
                //questionResponse.Attempts = 0;
                //questionResponse.Attempts = questionResponse.Attempts++;
                questionResponse.TeacherSubQuestionResponses = GetTeacherSubQuestionResponse(userResponse.SubQuestions);
            }
            questionResponse.Created = DateTime.Now;
            questionResponse.LastUpdated = DateTime.Now;

            return questionResponse;
        }

        private TeacherResponse UpdateQuestionResponse(string mediaFilePath, QuestionResponseCommand userResponse, TeacherResponse existingResponse, IEnumerable<SubQuestion> subQuestions,int Attempts)
        {
            existingResponse.IsRevised = true;
            existingResponse.LastUpdated = DateTime.Now;
            if (!string.IsNullOrEmpty(userResponse.MediaStream))
            {
                if (string.IsNullOrEmpty(existingResponse.MediaId))
                {
                    string mediaId = Guid.NewGuid().ToString();
                    existingResponse.Media = new Media
                    {
                        MediaId = mediaId,
                        MediaTypeId = Constants.MediaTypeId,
                        MediaSource = Util.SaveMediaToFileSystem(userResponse.MediaStream, mediaFilePath, $"{userResponse.QuestionId}.mp3")
                    };
                }
                else
                {
                    existingResponse.Media.MediaSource = Util.SaveMediaToFileSystem(userResponse.MediaStream, mediaFilePath, $"{userResponse.QuestionId}.mp3");
                }
            }
            existingResponse.ResponseText = userResponse.ResponseText;
            if (subQuestions.Any() && userResponse.SubQuestions.Any())
            {
                existingResponse.Score = CalculateScore(subQuestions, userResponse.SubQuestions);
                existingResponse.Attempts=Attempts;
                int existingscore = Convert.ToInt32(existingResponse.Score);
                //written by soujanya to change no.of attempts

                //bool perfectscoreval = IsPerfectScore(subQuestions, existingscore);
                //if (perfectscoreval == true)
                //{
                //    if ((existingResponse.Attempts % 6) != 0)
                //    {
                //        existingResponse.Attempts = existingResponse.Attempts - (existingResponse.Attempts % 6) + 6;
                //    }
                //}

                DeleteExistingSubQuestionResponses(existingResponse.TeacherResponseId);
                existingResponse.TeacherSubQuestionResponses = GetTeacherSubQuestionResponse(userResponse.SubQuestions);
            }

            _unitOfWork
                .Repository<TeacherResponse>()
                .Update(existingResponse);

            return existingResponse;
        }

        private List<TeacherSubQuestionResponse> GetTeacherSubQuestionResponse(IEnumerable<SubQuestionCommand> subQuestions)
        {
            var subQuestionResponses = new List<TeacherSubQuestionResponse>();

            foreach (var response in subQuestions)
            {
                var count = 1;

                subQuestionResponses.AddRange(response.Answers.Select(x => new TeacherSubQuestionResponse
                {
                    SubQuestionId = response.SubQuestionId,
                    SubQuestionOptionId = x,
                    AnswerOrder = count++
                }));
            }

            return subQuestionResponses;
        }

        private void DeleteExistingSubQuestionResponses(int teacherResponseId)
        {
            var teacherSubQuestionResponse = _unitOfWork.Repository<TeacherSubQuestionResponse>();
            var subQuestionResponses = teacherSubQuestionResponse.FindAll(x => x.TeacherResponseId == teacherResponseId);

            if(subQuestionResponses != null && subQuestionResponses.Any())
            {
                teacherSubQuestionResponse.Delete(subQuestionResponses);
            }
        }

        private int CalculateScore(IEnumerable<SubQuestion> subQuestions, IEnumerable<SubQuestionCommand> userSubQuestions)
        {
            var score = 0;

            foreach (var userSubQuestion in userSubQuestions)
            {
                var subQuestion = subQuestions
                        .FirstOrDefault(x => x.SubQuestionId == userSubQuestion.SubQuestionId);

                switch (subQuestion.QuestionTypeId)
                {
                    case 1: 
                        if(userSubQuestion.Answers.First() == subQuestion.SubQuestionAnswers.First().SubQuestionOptionId)
                        {
                            score++;
                        }
                        break;

                    case 2:
                        score = subQuestion.SubQuestionAnswers.Select(y => y.SubQuestionOptionId).Intersect(userSubQuestion.Answers).Count();
                        break;

                    case 3:

                        List<string> answersText = new List<string>();
                        foreach(int answer in userSubQuestion.Answers)
                        {
                            answersText.Add(subQuestion.SubQuestionAnswers
                                .FirstOrDefault(x => x.SubQuestionOptionId == answer)?.SubQuestionOption?.OptionText);
                        }

                        if (answersText != null && answersText.SequenceEqual(subQuestion.SubQuestionAnswers.OrderBy(x => x.AnswerOrder).Select(x => x.SubQuestionOption.OptionText)))
                        {
                            score++;
                        }
                        break;

                    case 4:
                        var answers = subQuestion.SubQuestionAnswers.OrderBy(x => x.AnswerOrder).Select(x => x.SubQuestionOptionId).ToList();
                        var userAnswers = userSubQuestion.Answers.ToList();

                        if (answers != null && answers.Any() && userAnswers != null && userAnswers.Any())
                        {
                            for (int i = 0; i < answers.Count; i++)
                            {
                                if (userAnswers.Count > i && answers[i] == userAnswers[i])
                                {
                                    score++;
                                }
                            }
                        }
                        break;

                    case 5:

                        List<string> answersText1 = new List<string>();
                        foreach (int answer in userSubQuestion.Answers)
                        {
                            answersText1.Add(subQuestion.SubQuestionAnswers
                                .FirstOrDefault(x => x.SubQuestionOptionId == answer)?.SubQuestionOption?.OptionText);
                        }

                        if (answersText1 != null && answersText1.SequenceEqual(subQuestion.SubQuestionAnswers.OrderBy(x => x.AnswerOrder).Select(x => x.SubQuestionOption.OptionText)))
                        {
                            score++;
                        }
                        break;

                    default:
                        break;
                }
            }

            return score;
        }

        private bool IsPerfectScore(IEnumerable<SubQuestion> subQuestions, int score)
            => subQuestions.Any(x => x.QuestionTypeId == 2)
                ? (subQuestions.Where(x => x.QuestionTypeId != 2).Count() + subQuestions.Where(x => x.QuestionTypeId == 2).Select(x => x.SubQuestionAnswers.Count).Sum()) == score
                : subQuestions.Count() == score;

        public bool AddResponseState(string teacherId, string mentorId, IEnumerable<QuestionStateCommand> questions)
        {
            var q1 = questions.Where(x => x.State == 3);

            if (q1 != null) 
            {             

                var Updateteacherresponse = new TeacherResponse();
                string reqlessecid = "";
                int reqsectypeid =0;
                foreach (var ques in q1)
                {
                    var LessonsecId = _question
                   .Filter(filter: x => string.Equals(x.QuestionId, ques.QuestionId, StringComparison.OrdinalIgnoreCase), null)
                   .Select(x => x.LessonSectionId);

                    foreach (var lesssecid in LessonsecId)
                    {
                        reqlessecid = lesssecid;
                    }

                    var SecTypId = _lessonSection
                   .Filter(filter: x => string.Equals(x.LessonSectionId, reqlessecid, StringComparison.OrdinalIgnoreCase), null)
                   .Select(x => x.SectionTypeId);

                    foreach (var SecTypeId in SecTypId)
                    {
                        reqsectypeid = SecTypeId;
                    }

                    Updateteacherresponse.TeacherId = teacherId;
                    Updateteacherresponse.QuestionId = ques.QuestionId;

                    //var teacherResponse = _teacherResponse
                    //    .Find(x => x.QuestionId == ques.QuestionId && x.TeacherId == teacherId).(x => x.First());

                    var teacherResponse = _teacherResponse
                      .Filter(orderBy: x => x.OrderByDescending(x => x.LastUpdated))
                      .Where(x => x.QuestionId == ques.QuestionId && x.TeacherId == teacherId)
                      .FirstOrDefault();

                    if (reqsectypeid == 1 || reqsectypeid == 4)
                    {                        
                        if (teacherResponse != null)
                        {
                            teacherResponse.Attempts = 0;

                            _teacherResponse.Update(teacherResponse);
                            _unitOfWork.Commit();
                        }
                    }
                    //else if (reqsectypeid == 2 || reqsectypeid == 3)
                    //{                        
                    //    if (teacherResponse != null)
                    //    {
                    //        teacherResponse.Attempts = 0;

                    //        _teacherResponse.Add(teacherResponse);
                    //        _unitOfWork.Commit();
                    //    }
                    //}
                }

            }

            _unitOfWork
                .Repository<TeacherResponseStatu>()
                .Add(questions
                    .Select(x => GetTeacherResponseStatus(teacherId, x.QuestionId, x.State, string.IsNullOrEmpty(mentorId) ? teacherId : mentorId)));

           
            if(questions
                .Any(x => x.TeacherResponseId > 0 && !string.IsNullOrWhiteSpace(x.Comments)) && !string.IsNullOrEmpty(mentorId))
            {
                _unitOfWork
                .Repository<MentorResponse>()
                .Add(questions
                    .Where(x => x.TeacherResponseId > 0 && !string.IsNullOrWhiteSpace(x.Comments))
                    .Select(x => new MentorResponse
                    {
                        MentorComments = x.Comments,
                        MentorId = mentorId,
                        TeacherResponseId = x.TeacherResponseId
                    }));
            }

            if (questions
               .Any(x =>x.State!=1))
            {
                string state = "";
               
                if (questions.Any(x => x.State == 3))
                {
                   state = "has asked to REDO the ";
                }
                else if(questions.Any(x => x.State == 4))
                {
                    state = "has APPROVED the "; 
                }

                var teacher = _teacher
                          .Find(x => (string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase)));
             
                foreach (var quest in questions)
                {
                    var question = _question
                   .Find(x => (string.Equals(x.QuestionId, quest.QuestionId, StringComparison.OrdinalIgnoreCase)));
                    var lessonsection = _lessonSection
                        .Find(x => (string.Equals(x.LessonSectionId, question.LessonSectionId, StringComparison.OrdinalIgnoreCase)));

                    var lesson = _lesson
                       .Find(x => (string.Equals(x.LessonId, lessonsection.LessonId, StringComparison.OrdinalIgnoreCase)));

                    var sectiontype = _sectionType
                       .Find(x => (string.Equals(x.SectionTypeId, lessonsection.SectionTypeId)));
                    string from = teacher.MentorId;
                    string to = teacherId;
                    int roleid = 4;
                    var userProfile = _userProfile
                   .Find(x => (string.Equals(x.UserId, teacher.MentorId, StringComparison.OrdinalIgnoreCase)));

                    string message = "";
                    if (sectiontype.SectionTypeDescription == "Speak Well")
                    {
                       message = userProfile.Name + " " + state + lesson.LessonName + "  " + sectiontype.SectionTypeDescription + " section"+"  Question"+question.QuestionOrder;

                    }
                    else if (sectiontype.SectionTypeDescription == "Write Right")
                    {
                       message = userProfile.Name + " " + state + lesson.LessonName + "  " + sectiontype.SectionTypeDescription + " section" + "  Component" + question.QuestionOrder;

                    }
                    else 
                    {
                      message = userProfile.Name + " " + state + lesson.LessonName + "  " + sectiontype.SectionTypeDescription + " section";
                    }
                    string created_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    int status = 1;
                    _unitOfWork
                        .Repository<Notifications>()
                        .Add(GetNotificationdetails(from, to, roleid, message, created_date, status));
                }
            }           
            return _unitOfWork.Commit() > 0;
        }

        public void UpdateLessonSetState(string teacherId)
        {
            var includeProperties = new StringBuilder(Constants.QuestionProperty);
            includeProperties.Append($",{Constants.TeacherProperty}");
            includeProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}");
            includeProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.LessonProperty}");

            var teacherResponse = _unitOfWork
                .Repository<TeacherResponseStatu>()
                .Filter(filter: x => string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase),
                    orderBy: x => x.OrderByDescending(x => x.Created),
                    includeProperties: includeProperties.ToString())
                .GroupBy(x => x.Question)
                .Select(x => x.First());

            var lessonSet = GetLessonSet(teacherResponse.First().Question.LessonSection.Lesson.LessonSetId);

            if(lessonSet
                .Lessons
                .SelectMany(x => x.LessonSections)
                .SelectMany(x => x.Questions)
                .Select(x => x.QuestionId)
                .All(teacherResponse
                    .Select(x => x.QuestionId)
                    .Contains)
                && teacherResponse
                .All(x => x.ResponseState == (int)Enums.ResponseStatus.COMPLETED_AND_APPROVED))
            {

            }
        }

        private LessonSet GetLessonSet(string lessonSetId)
        {
            var includeProperties = new StringBuilder(Constants.LessonsProperty);
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}");

            return _unitOfWork
                .Repository<LessonSet>()
                .Filter(filter: x => string.Equals(x.LessonSetId, lessonSetId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();
        }

        private void DeleteTeacherResponses(ICollection<TeacherResponse> responses, ICollection<TeacherResponseStatu> responseStates)
        {
            _unitOfWork
                .Repository<Media>()
                .Delete(responses
                .Where(x => x.Media != null)
                .Select(x => x.Media)
                .ToList());

            _unitOfWork
                .Repository<TeacherResponse>()
                .Delete(responses);

            _unitOfWork
                .Repository<TeacherResponseStatu>()
                .Delete(responseStates);
        }

        private void DeleteQueries(ICollection<Query> queries)
        {
            _unitOfWork
                .Repository<Media>()
                .Delete(queries
                .SelectMany(x => x.QueryDatas)
                .Where(x => x.Media != null)
                .Select(x => x.Media)
                .ToList());

            _unitOfWork
            .Repository<Query>()
            .Delete(queries);
        }
        private void DeleteNotifications(string teacherId)
        {
            var notifications = _notification
              .Filter(filter: x => string.Equals(x.msgfrom, teacherId, StringComparison.OrdinalIgnoreCase) || string.Equals(x.msgto, teacherId, StringComparison.OrdinalIgnoreCase));
                  
            _unitOfWork
                .Repository<Notifications>()
                .Delete(notifications
                .Where(x => x.msgto != null  && x.msgfrom != null)             
                .ToList());
                        
        }


        public QuestionScoreModel GetQuestionScore(string userId, string questionId)
        {
            TeacherResponse updateattempts = new TeacherResponse();
            //int userattempts = 0;
            var includeProperties = new StringBuilder(Constants.TeacherResponsesProperty);
            includeProperties.Append($",{Constants.SubQuestionsProperty}");
            includeProperties.Append($",{Constants.SubQuestionsProperty}.{Constants.SubQuestionAnswersProperty}");

            var question = _unitOfWork.Repository<Question>()
                .Filter(filter:  x => string.Equals(x.QuestionId, questionId, StringComparison.OrdinalIgnoreCase) 
                && x.TeacherResponses.Any(y => string.Equals(x.QuestionId, questionId, StringComparison.OrdinalIgnoreCase)), 
                includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (question == null || question.TeacherResponses == null || !question.TeacherResponses.Any())
                return null;

            var userResponse = question.TeacherResponses.First(y => string.Equals(y.TeacherId, userId, StringComparison.OrdinalIgnoreCase));

            var scoreModel = new QuestionScoreModel();
            scoreModel.RecommendedAttempts = question.RecommendedAttempts ?? 0;

            scoreModel.Attempts = GetAttempts(scoreModel.RecommendedAttempts, userResponse.Attempts ?? 0);
            scoreModel.Score = userResponse.Score ?? 0;
            scoreModel.TotalQuestion = question.SubQuestions.Count();
            scoreModel.PerfectScore = IsPerfectScore(question.SubQuestions, scoreModel.Score);
            //userattempts = scoreModel.Attempts;
            //if (scoreModel.PerfectScore == true)
            //{
            //    if ((userattempts % 6) != 0)
            //    {
            //        userattempts = userattempts - (userattempts % 6) + 6;
            //    }
            //    updateattempts.Attempts = userattempts;
            //    updateattempts.Created = userResponse.Created;
            //    updateattempts.LastUpdated = userResponse.LastUpdated;
            //    updateattempts.QuestionId = userResponse.QuestionId;
            //    updateattempts.Score = userResponse.Score;
            //    updateattempts.TeacherId = userResponse.TeacherId;
            //    updateattempts.TeacherResponseId = userResponse.TeacherResponseId;
            //    DeleteExistingSubQuestionResponses(updateattempts.TeacherResponseId);
            //    _unitOfWork
            //    .Repository<TeacherResponse>()
            //    .Update(updateattempts);
                               
            //        _unitOfWork
            //            .Repository<TeacherResponseStatu>()
            //            .Add(GetTeacherResponseStatus(userId, userResponse.QuestionId, (int)Enums.ResponseStatus.SUBMITTED_FOR_REVIEW, userId));
                

            //}
                return scoreModel;
        }

        public TeachersMentorModel GetTeachersMissingMentor()
        {
            var includeProperties = new StringBuilder(Constants.TeacherNavigationProperty);
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserLanguagesProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}.{Constants.StateProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}.{Constants.CountryProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}.{Constants.GenderProperty}");
            includeProperties.Append($",{Constants.TeacherNavigationProperty}.{Constants.UserLanguagesProperty}.{Constants.LanguageProperty}");

            var teachers = _teacher
                .Filter(filter: x => string.IsNullOrEmpty(x.MentorId) && x.TeacherNavigation.IsActive,
                    includeProperties: includeProperties.ToString())
                .Select(x => new TeacherModel
                {
                    Gender = x.TeacherNavigation.UserProfile.Gender?.GenderDescription,
                    GenderId = x.TeacherNavigation.UserProfile.GenderId ?? 0,
                    Language = x.TeacherNavigation.UserLanguages.FirstOrDefault()?.Language?.LanguageName,
                    LanguageId = x.TeacherNavigation.UserLanguages.FirstOrDefault()?.LanguageId ?? 0,
                    TeacherId = x.TeacherId,
                    TeacherName = x.TeacherNavigation.UserProfile.Name,
                    State = x.TeacherNavigation.UserProfile.State?.StateName,
                    StateId = x.TeacherNavigation.UserProfile.StateId ?? 0,
                    Country = x.TeacherNavigation.UserProfile.Country?.country_name,
                    countryid = x.TeacherNavigation.UserProfile.countryid ?? 0
                });

            return new TeachersMentorModel
            {
                Teachers = teachers
            };
        }

        public bool AssignMentor(string teacherId, string mentorId)
        {
            var teacher = _teacher
                .Find(x => string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase));

            var mentor = _unitOfWork.Repository<Mentor>()
                .Find(x => string.Equals(x.MentorId, mentorId, StringComparison.OrdinalIgnoreCase));

            if (teacher == null || mentor == null)
            {
                return false;
            }

            teacher.MentorId = mentorId;

            return _unitOfWork.Commit() > 0;
        }

        public bool UpdateActiveLessonSet(string teacherId, string lessonSetId)
        {
            var teacher = _teacher
                .Find(x => string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase));

            if (teacher == null || string.IsNullOrEmpty(lessonSetId))
            {
                return false;
            }

            teacher.ActiveLessonSetId = lessonSetId;
            teacher.LoadNextLessonSet = false;

            return _unitOfWork.Commit() > 0;
        }

        public bool DeleteLessonSetData(string teacherId)
        {
            var includeProperties = new StringBuilder(Constants.QueriesProperty);
            includeProperties.Append($",{Constants.TeacherResponsesProperty}");
            includeProperties.Append($",{Constants.TeacherResponseStatusProperty}");
            includeProperties.Append($",{Constants.QueriesProperty}.{Constants.QueryDatasProperty}");
            includeProperties.Append($",{Constants.TeacherResponsesProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.QueriesProperty}.{Constants.QueryDatasProperty}.{Constants.MediaProperty}");

            var teacher = _teacher
                .Filter(filter: x => string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (teacher == null)
            {
                return false;
            }

            Util.DeleteDirectory($@"{Constants.UserMediaFilePath}\\{teacherId}\\{teacher.ActiveLessonSetId}");

            DeleteTeacherResponses(teacher.TeacherResponses, teacher.TeacherResponseStatus);

            DeleteQueries(teacher.Queries);
            DeleteNotifications(teacherId);

            teacher.RedoActiveLessonSet = false;

            _unitOfWork.Commit();

            return true;
        }
    }
}
