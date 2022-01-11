using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Enum;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.Mentor;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class MentorRespository : IMentorRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IDataAccessRepository<Mentor> _mentor;
        private readonly IDataAccessRepository<UserProfile> _userprofile;
        private MySqlConnection _dbconnection;
        private MySqlConnection _dbconnectionnew;
        private string Connectionstring = "VidyaVahiniDb";

        private readonly IDataAccessRepository<LessonSection> _lessonSection;

        public MentorRespository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mentor = _unitOfWork.Repository<Mentor>();
            _userprofile = _unitOfWork.Repository<UserProfile>();
            _lessonSection = _unitOfWork.Repository<LessonSection>();
            _dbconnection = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            _dbconnectionnew = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));

        }

        public void AddMentor(MentorData mentorData)
        {
            var mentor = new Mentor
            {
                Created = DateTime.Now,
                MentorId = mentorData.UserId
            };

            mentor = UpdateMentor(
                mentor: mentor,
                workingInSssvv: mentorData.WorkingInSssvv,
                sssvvVolunteer: mentorData.SssvvVolunteer,
                workingInSaiOrganization: mentorData.WorkingInSaiOrganization,
                saiVolunteer: mentorData.SaiVolunteer,
                englishTeachingExperience: mentorData.EnglishTeachingExperience,
                occupation: mentorData.Occupation,
                timeCapacity: mentorData.TimeCapacity,
                teacherCapacity: mentorData.TeacherCapacity);

            _mentor.Add(mentor);
        }

        public void DeleteMentor(string userId)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.MentorNavigationProperty);

            var mentor = _mentor
                .Filter(filter: x => string.Equals(x.MentorId, userId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: Constants.MentorNavigationProperty)
                .FirstOrDefault();

            if (mentor == null)
                return;

            _mentor.Delete(mentor);

            _unitOfWork.Repository<UserAccount>().Delete(mentor.MentorNavigation);

            _unitOfWork.Commit();
        }

        public IEnumerable<MentorBasicDetails> GetAllMentor()
        {
            var mentors = _mentor.Filter(
                includeProperties: $"{Constants.MentorNavigationProperty},{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}");

            return mentors.Select(x => new MentorBasicDetails
            {
                IsActive = x.MentorNavigation.IsActive,
                Name = x.MentorNavigation.UserProfile.Name,
                UserId = x.MentorId
            });
        }

        public MentorModel GetMentor(string userId)
        {
            var includeProperties = new StringBuilder();
            includeProperties.Append(Constants.MentorNavigationProperty);
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserLanguagesProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}.{Constants.StateProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}.{Constants.CountryProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}.{Constants.GenderProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserLanguagesProperty}.{Constants.LanguageProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}.{Constants.QualificationProperty}");

            var mentor = _mentor
                .Filter(filter: x => string.Equals(x.MentorId, userId, StringComparison.OrdinalIgnoreCase),
                    includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (mentor == null)
                return null;

            int? Qualification = mentor.MentorNavigation?.UserProfile?.Qualification?.QualificationId;


            return new MentorModel
            {
                City = mentor.MentorNavigation?.UserProfile?.City,
                Email = mentor.MentorNavigation?.UserProfile?.Email,
                EnglishTeachingExperience = mentor.EnglishTeachingExperience,
                Gender = mentor.MentorNavigation?.UserProfile?.Gender?.GenderDescription,
                Languages = mentor.MentorNavigation?.UserLanguages?.Select(x => x?.Language?.LanguageName),
                MobileNumber = mentor.MentorNavigation?.UserProfile?.MobileNumber,
                Name = mentor.MentorNavigation?.UserProfile?.Name,
                Occupation = mentor.Occupation,
                Qualification = mentor.MentorNavigation?.UserProfile?.Qualification?.QualificationDescription,
                SaiOrganizationVolunteer = mentor.SaiOrganizationVoluteerName,
                SssvvVolunteer = mentor.SssvvvoluteerName,
                State = mentor.MentorNavigation?.UserProfile?.State?.StateName,
                Country = mentor.MentorNavigation?.UserProfile?.Country?.country_name,
                TeacherCapacity = mentor.TeachersCapacity,
                TimeCapacity = mentor.TimeCapacity,
                UserId = mentor.MentorId,
                WorkingInSaiOrganization = mentor.WorkingInSaiOrganization,
                WorkingInSssvv = mentor.WorkingInSssvv,
                IsActive = mentor.MentorNavigation.IsActive
            };
        }

        public MentorData GetMentorByUsername(string username)
        {
            var mentor = _mentor.Find(x => string.Equals(x.MentorNavigation.Username, username, StringComparison.OrdinalIgnoreCase));

            if (mentor == null)
                return null;

            return GetMentorData(mentor);
        }

        public bool GetUser(string mobileNumber)
        {
            var userprofile = _userprofile.Find(x => string.Equals(x.MobileNumber, mobileNumber));

            if (userprofile == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MentorData GetMentorData(Mentor mentor)
            => new MentorData
            {
                EnglishTeachingExperience = mentor.EnglishTeachingExperience,
                Occupation = mentor.Occupation,
                SaiVolunteer = mentor.SaiOrganizationVoluteerName,
                SssvvVolunteer = mentor.SssvvvoluteerName,
                TeacherCapacity = mentor.TeachersCapacity,
                TimeCapacity = mentor.TimeCapacity,
                UserId = mentor.MentorId,
                WorkingInSaiOrganization = mentor.WorkingInSaiOrganization,
                WorkingInSssvv = mentor.WorkingInSssvv
            };

        private Mentor UpdateMentor(Mentor mentor, bool workingInSssvv, string sssvvVolunteer, bool workingInSaiOrganization, 
            string saiVolunteer, string englishTeachingExperience, string occupation, int timeCapacity, int teacherCapacity)
        {
            mentor.EnglishTeachingExperience = englishTeachingExperience;
            mentor.LastUpdated = DateTime.Now;
            mentor.Occupation = occupation;
            mentor.SaiOrganizationVoluteerName = saiVolunteer;
            mentor.SssvvvoluteerName = sssvvVolunteer;
            mentor.TeachersCapacity = teacherCapacity;
            mentor.TimeCapacity = timeCapacity;
            mentor.WorkingInSaiOrganization = workingInSaiOrganization;
            mentor.WorkingInSssvv = workingInSssvv;

            return mentor;
        }

        public PreferredMentorModel GetAvailableMentors(int teacherGenderId, int teacherLanguageId, int teacherStateId)
        {
            var includeProperties = new StringBuilder(Constants.MentorNavigationProperty);
            includeProperties.Append($",{Constants.TeachersProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserLanguagesProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}.{Constants.StateProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserProfileProperty}.{Constants.GenderProperty}");
            includeProperties.Append($",{Constants.MentorNavigationProperty}.{Constants.UserLanguagesProperty}.{Constants.LanguageProperty}");

            var mentors = _mentor
                .Filter(filter: x => x.TeachersCapacity > x.Teachers.Count() 
                && x.MentorNavigation.IsActive 
                && x.MentorNavigation.UserProfile.GenderId == teacherGenderId
                && x.MentorNavigation.UserLanguages.Any(y => y.LanguageId == teacherLanguageId),
                    includeProperties: includeProperties.ToString())
                .Select(x => new PreferredMentor
                {
                    CurrentLoad = x.Teachers.Count(),
                    Gender = x.MentorNavigation.UserProfile.Gender?.GenderDescription,
                    GenderId = x.MentorNavigation.UserProfile.GenderId ?? 0,
                  // Language = x.MentorNavigation.UserLanguages.FirstOrDefault()?.Language?.LanguageName,
                   Language = x.MentorNavigation?.UserLanguages?.Select(x => x?.Language?.LanguageName),                  
                    LanguageId = x.MentorNavigation.UserLanguages.FirstOrDefault()?.LanguageId ?? 0,
                    MaxLoadPreference = x.TeachersCapacity,
                    MentorId = x.MentorId,
                    MentorName = x.MentorNavigation.UserProfile.Name,
                    State = x.MentorNavigation.UserProfile.State?.StateName,
                    StateId = x.MentorNavigation.UserProfile.StateId ?? 0
                });

            var mentorsWithSameGenderLanguage = mentors.Where(x => x.LanguageId == teacherLanguageId);

            var mentorsWithSameGenderLanguageState = mentorsWithSameGenderLanguage.Where(x => x.StateId == teacherStateId);

            return new PreferredMentorModel
            {
                PreferredMentors = mentorsWithSameGenderLanguageState
                .Union(mentorsWithSameGenderLanguage.Where(x => !mentorsWithSameGenderLanguageState.Any(y => y.MentorId == x.MentorId))),
                OtherMentors = mentors.Where(x => !mentorsWithSameGenderLanguage.Any(y => y.MentorId == x.MentorId)),
                NumberOfMentorsMatchingGenderLanguage = mentorsWithSameGenderLanguage.Count(),
                NumberOfMentorsMatchingGenderLanguageState = mentorsWithSameGenderLanguageState.Count()
            };
        }

        public MentorDashboardModel GetMentorDashboard(string userId)
        {
            var includeProperties = new StringBuilder(Constants.TeachersProperty);
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.QueriesProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.TeacherNavigationProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.TeacherResponseStatusProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.TeacherNavigationProperty}.{Constants.UserProfileProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.TeacherResponseStatusProperty}.{Constants.QuestionProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.TeacherNavigationProperty}.{Constants.UserLanguagesProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.ActiveLessonSetProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.ActiveLessonSetProperty}.{Constants.LevelProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}");
            includeProperties.Append($",{Constants.TeachersProperty}.{Constants.ActiveLessonSetProperty}.{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.SectionTypeProperty}");

            var mentor = _mentor
                .Filter(filter: x => string.Equals(x.MentorId, userId, StringComparison.OrdinalIgnoreCase) && x.MentorNavigation.IsActive,
                includeProperties: includeProperties.ToString())
                .FirstOrDefault();

            if (mentor == null || mentor.Teachers == null || !mentor.Teachers.Any(x => x.TeacherNavigation.IsActive))
            {
                return null;
            }

            var mentees = GetMenteeProgress(mentor, mentor.Teachers.Where(x => x.TeacherNavigation.IsActive));

            return new MentorDashboardModel
            {
                Mentees = mentees,
                PendingQueryActions = mentees.Select(x => x.PendingQueryActionCount).Sum(),
                PendingSubmissionActions = mentees.Select(x => x.PendingSubmissionActionCount).Sum()
            };
        }

        public bool LoadNextLessonSet(string teacherId)
        {
            var teacherRespository = _unitOfWork.Repository<Teacher>();

            var teacher = teacherRespository
                .Find(x => string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase));

            if (teacher == null)
            {
                return false;
            }

            teacher.LoadNextLessonSet = true;

            teacher.RedoActiveLessonSet = false;

            teacherRespository.Update(teacher);

            return _unitOfWork.Commit() > 0;
        }

        public bool RedoActiveLessonSet(string teacherId)
        {
            var teacherRespository = _unitOfWork.Repository<Teacher>();

            var teacher = teacherRespository
                .Find(x => string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase));

            if (teacher == null)
            {
                return false;
            }

            teacher.RedoActiveLessonSet = true;

            teacher.LoadNextLessonSet = false;

            teacherRespository.Update(teacher);

            return _unitOfWork.Commit() > 0;
        }

        private IEnumerable<MenteeProgressModel> GetMenteeProgress(Mentor mentor, IEnumerable<Teacher> teachers)
        {
            var menteesProgress = new List<MenteeProgressModel>();

            foreach (var teacher in teachers)
            {
                var menteeProgress = new MenteeProgressModel 
                { 
                    TeacherId = teacher.TeacherId,
                    TeacherName = teacher.TeacherNavigation.UserProfile.Name,
                    TeacherLanguageId = teacher.TeacherNavigation.UserLanguages.FirstOrDefault()?.LanguageId ?? 0
                };

                menteeProgress.ActiveLessonSetId = teacher.ActiveLessonSetId;
                menteeProgress.LevelId = teacher.ActiveLessonSet.LevelId;
                menteeProgress.Level = teacher.ActiveLessonSet.Level.LevelCode;
                menteeProgress.LessonStartNumber = teacher.ActiveLessonSet.Lessons?.Where(x => x.LessonNumber != Constants.AssessmentLessonNumber)?.OrderBy(x => x.LessonNumber).First()?.LessonNumber ?? 0;
                menteeProgress.LessonEndNumber = teacher.ActiveLessonSet.Lessons?.OrderBy(x => x.LessonNumber)?.Last()?.LessonNumber ?? 0;

                menteeProgress.PendingQueryIds = GetPendingQueryIds(teacher);

                menteeProgress.PendingQueryActionCount = menteeProgress.PendingQueryIds.Count();
        
                menteeProgress.SubmittedLessons = GetSubmittedLessons(teacher.ActiveLessonSet, GetSubmittedLessonSectionIds(teacher));
                menteeProgress.PendingSubmissionActionCount = menteeProgress.SubmittedLessons.SelectMany(x => x.SubmittedLessonSections).Count();
                menteeProgress.LessonSetStatus = teacher.TeacherResponseStatus.Any() ?
                    Enums.LessonStatus.ONGOING.ToString() : Enums.LessonStatus.TO_BE_DONE.ToString();
                
                menteesProgress.Add(menteeProgress);
            }

            return menteesProgress;
        }

        private IEnumerable<string> GetPendingQueryIds(Teacher teacher)
            => teacher
            .Queries
            .Where(x => x.QueryStatus == (int)Enums.QueryStatus.PENDING_WITH_MENTOR)
            .Select(x => x.QueryId);

        private IEnumerable<string> GetSubmittedLessonSectionIds(Teacher teacher)
        {
            IEnumerable<string> response1 =null;          
            List<string> res1 = new List<string>();
            List<string> res2 = new List<string>();
            int secid = 0;
            
            response1 = teacher
             .TeacherResponseStatus
             .OrderByDescending(x => x.Created)
             .GroupBy(x => x.QuestionId)
             .Select(x => x.First())
             .Where(x => x.ResponseState == (int)Enums.ResponseStatus.SUBMITTED_FOR_REVIEW)
             .Select(x => x.Question.LessonSectionId);

            foreach (var res in response1)
            {
                var seccodeid = _lessonSection
                       .Filter(orderBy: x => x.OrderByDescending(x => x.LastUpdated))
                       .Where(x => x.LessonSectionId == res)
                       .Select(x => x.SectionTypeId);
                foreach (var sid in seccodeid)
                {
                    secid = sid;
                }

                if (secid == 4)
                {
                    this._dbconnection.Query<int>("select count(*) from teacherresponsestatus where TeacherId ='" + teacher.TeacherId + "' and QuestionId in (select QuestionId from question where lessonsectionid ='" + res + "')  and ResponseState = '2'"
                    , new[] {
                    typeof(int),
                }, obj =>
                {
                    int count = int.Parse(obj[0].ToString());
                    if (count < 3)
                    {
                        res1.Add(res);
                    }
                    return (count);
                }
                );
                    _dbconnection.Close();
                    _dbconnection.Open();
                }
                else if (secid == 2)
                {
                    this._dbconnectionnew.Query<int>("select count(*) from teacherresponsestatus where TeacherId ='" + teacher.TeacherId + "' and QuestionId in (select QuestionId from question where lessonsectionid ='" + res + "')  and ResponseState = '2'"
                      , new[] {
                    typeof(int),
                  }, obj =>
                  {
                      int count1 = int.Parse(obj[0].ToString());
                      if (count1 < 5)
                      {
                          res1.Add(res);
                      }
                      return (count1);
                  }
                  );
                    _dbconnectionnew.Close();
                    _dbconnectionnew.Open();
                }

                if (res1.Any(str => res.Contains(str)))
                {
                    Console.WriteLine("success!");
                }
                else
                {
                    res2.Add(res);
                }

            }
                  response1 = res2.AsEnumerable();                 
                  return response1;
        }
          
        
        private IEnumerable<SubmittedLessonModel> GetSubmittedLessons(LessonSet currentLessonSet, IEnumerable<string> submittedLessonSectionIds)
            => submittedLessonSectionIds == null ? new List<SubmittedLessonModel>() :
            currentLessonSet
            .Lessons
            .SelectMany(x => x.LessonSections)
            .Where(x => submittedLessonSectionIds.Any(y => string.Equals(y, x.LessonSectionId)))
           
            .GroupBy(x => x.Lesson)
            .Select(x => new SubmittedLessonModel
            {
                LessonId = x.Key.LessonId,
                LessonNumber = x.Key.LessonNumber,
                SubmittedLessonSections = x.Select(y => new SubmittedLessonSectionModel
                {
                    SectionCode = y.SectionType.SectionTypeCode,
                    SectionId = y.LessonSectionId
                })
            });
    }
}
