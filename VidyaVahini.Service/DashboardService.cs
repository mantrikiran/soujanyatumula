
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Enum;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Entities.Dashboard.LessonSection;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Infrastructure.Exception;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;




namespace VidyaVahini.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly ICache _cache;
        private readonly IErrorRepository _errorRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IConfiguration _configuration;  
        private MySqlConnection _dbconnectionnew;
        private MySqlConnection _dbconnection;
        private string Connectionstring = "VidyaVahiniDb";
        public DashboardService(
            ICache cache,
            IErrorRepository errorRepository,
            ILanguageRepository languageRepository,
            IDashboardRepository dashboardRepository,
            IConfiguration configuration)
        {
            _cache = cache;
            _errorRepository = errorRepository;
            _languageRepository = languageRepository;
            _dashboardRepository = dashboardRepository;
            _configuration = configuration;
        }

        public void DeleteLessonSet(LessonSetCommand lessonSet)
        {
            if (!_dashboardRepository.DeleteLessonSet(lessonSet.LessonSetId))
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidLessonSet));
            }
        }

        public DashboardModel GetDashboard(string lessonSetId, int languageId)
        {
            //var dashboard = _cache.Get<DashboardModel>(string.Format(Constants.DashboardCache, lessonSetId, languageId));

            //if (dashboard == null)
            //{
            var dashboard = _dashboardRepository.GetDashboard(lessonSetId, languageId);
            //    _cache.Set(string.Format(Constants.DashboardCache, lessonSetId, languageId), dashboard, TimeSpan.FromMinutes(Constants.DashboardCacheDuration));
            //}

            //if (dashboard == null)
            //    throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidLessonSet));

            return dashboard;
        }

        public DashboardModel GetLessons(string lessonSetId, int languageId)
        {
            //var lessons = _cache.Get<IEnumerable<LessonData>>(string.Format(Constants.DashboardCache, lessonSetId, languageId));

            //if (lessons == null)
            //{
            var lessons = _dashboardRepository.GetLessons(lessonSetId, languageId);
            //    _cache.Set(string.Format(Constants.DashboardCache, lessonSetId, languageId), lessons, TimeSpan.FromMinutes(Constants.DashboardCacheDuration));
            //}

            //if (lessons == null)
            //    throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidLessonSet));

            return lessons;
        }

        public DashboardLessonModel GetLessonSections(string lessonSetId, string lessonId, int languageId)
        {
            //var lessonSections = _cache.Get<LessonData>(string.Format(Constants.DashboardCache, lessonSetId, languageId));

            //if (lessonSections == null)
            //{
            var lessonSections = _dashboardRepository.GetLessonSections(lessonSetId, lessonId, languageId);
            //    _cache.Set(string.Format(Constants.DashboardCache, lessonSetId, languageId), lessonSections, TimeSpan.FromMinutes(Constants.DashboardCacheDuration));
            //}

            //if (lessonSections == null)
            //    throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidLessonSet));

            return lessonSections;
        }

        public DashboardSectionTextModel GetSectiontext(int Sectiontypeid, int languageId)
        {
            //var lessonSections = _cache.Get<LessonData>(string.Format(Constants.DashboardCache, lessonSetId, languageId));

            //if (lessonSections == null)
            //{   
            int UserlanguageId = 0;

            _dbconnection = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            _dbconnection = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));


            this._dbconnection.Query<int>("select count(*) from SectionInstruction where SectionTypeid = '" + Sectiontypeid + "'" + "and" + " "+"languageid='" + languageId + "'", new[] {
                typeof(int),
            }, obj =>
            {
                int Sections = int.Parse(obj[0].ToString());
                if (Sections == 0)
                {
                    UserlanguageId = 1;
                }
                else
                {
                    UserlanguageId = languageId;
                }

                return (Sections);


            });
            _dbconnection.Close();
            _dbconnection = null;
            var SectionInstruction = _dashboardRepository.GetSectiontext(Sectiontypeid, UserlanguageId);
            //    _cache.Set(string.Format(Constants.DashboardCache, lessonSetId, languageId), lessonSections, TimeSpan.FromMinutes(Constants.DashboardCacheDuration));
            //}

            //if (lessonSections == null)
            //    throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidLessonSet));

            return SectionInstruction;
        }

        public LessonSetData InsertDashoardData(InsertLessonSetCommand lessonSet)
        {

            // Code to check Lessonsetid is in use or not //
            var newLessonSet = new LessonSetData();

            _dbconnectionnew = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            _dbconnection = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            

            this._dbconnectionnew.Query<int>("select count(*) from lessonset where lessonsetid = '" + lessonSet.LessonSetId + "'", new[] {
                typeof(int),
            }, obj =>
            {

                int ActiveLessonSetId = int.Parse(obj[0].ToString());
                if (ActiveLessonSetId != 0)
                {
                    //throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.Lessonsetisalreadyinuse));

                    //        this._dbconnection.Query<int>("DeleteLessonset", new[] {
                    //         typeof(int),
                    //}, obj =>
                    //{
                    //    int value = int.Parse(obj[0].ToString());
                    //    newLessonSet = InsertLessonSet(new LessonSetData
                    //    {
                    //        LevelId = lessonSet.LevelId,
                    //        LessonSetId = lessonSet.LessonSetId,
                    //        LessonSetOrder = lessonSet.LessonSetOrder,
                    //    }, lessonSet.Lessons);

                    //    ProcessLessonSetMedia(newLessonSet);
                    //    return (value);

                    //}, new { _lessonsetid = lessonSet.LessonSetId }, splitOn: "LessonSetId", commandType: CommandType.StoredProcedure).AsQueryable();
                    //        _dbconnection.Close();
                    //        _dbconnection = null;

                    this._dbconnection.Execute("delete from media where MediaId in(select questionmedia.MediaId from questionmedia  inner join Question on questionmedia.QuestionId = question.QuestionId inner join lessonsection on lessonsection.LessonSectionId = question.LessonSectionId inner join lesson on lesson.LessonId = lessonsection.LessonId inner join lessonset on lessonset.LessonSetId = lesson.LessonSetId where lesson.LessonSetId ='" + lessonSet.LessonSetId+"'"+")"
                
            );
                    _dbconnection.Close();                  

                    _dbconnection.Open();
                    this._dbconnection.Execute("delete from Lesson where lesson.LessonSetId ='" + lessonSet.LessonSetId + "'"

           );
                    _dbconnection.Close();
                    _dbconnection = null;
                    //if (!_dashboardRepository.DeleteLessonSet(lessonSet.LessonSetId))
                    //{
                    //    throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.InvalidLessonSet));
                    //}
                    //newLessonSet = InsertLessonSet(new LessonSetData
                    //{
                    //    LevelId = lessonSet.LevelId,
                    //    LessonSetId = lessonSet.LessonSetId,
                    //    LessonSetOrder = lessonSet.LessonSetOrder,
                    //}, lessonSet.Lessons);

                    newLessonSet = InsertSameLessonSet(new LessonSetData
                    {
                        LevelId = lessonSet.LevelId,
                        LessonSetId = lessonSet.LessonSetId,
                        LessonSetOrder = lessonSet.LessonSetOrder,
                    }, lessonSet.Lessons);

                    ProcessLessonSetMedia(newLessonSet);

                }
                else
                {
                    newLessonSet = InsertLessonSet(new LessonSetData
                    {
                        LevelId = lessonSet.LevelId,
                        LessonSetId = lessonSet.LessonSetId,
                        LessonSetOrder = lessonSet.LessonSetOrder,
                    }, lessonSet.Lessons);

                    ProcessLessonSetMedia(newLessonSet);

                }
                return (ActiveLessonSetId);

            });
            _dbconnectionnew.Close();
            _dbconnectionnew = null;

            return newLessonSet;
            // Code to check Lessonsetid is in use or not //
        }

        public void ProcessLessonSetMedia(LessonSetData lessonSet)
        {
            var mediaDirectory = $"{Constants.DashboardMediaFilePath}\\{lessonSet.LessonSetOrder}";

            var languages = _languageRepository.GetLanguageData(includeEnglish: true);

            foreach (var lesson in lessonSet.Lessons)
            {
                var lessonMediaDirectory = $"{mediaDirectory}\\{lesson.LessonNumber}";

                foreach (var section in lesson.LessonSections.Where(x => x.SectionTypeId == 1 || x.SectionTypeId == 2 || x.SectionTypeId == 3))
                {
                    var sectionMediaDirectory = $"{lessonMediaDirectory}\\{section.SectionTypeId}";

                    if (Directory.Exists(sectionMediaDirectory))
                    {
                        if (section.SectionTypeId == 1 || section.SectionTypeId == 3)
                        {
                            var files = Directory.GetFiles(sectionMediaDirectory);

                            if (files != null && files.Any())
                            {
                                _dashboardRepository.InsertQuestionMedia(new QuestionMediaData
                                {
                                    LanguageId = Constants.EnglishLanguage,
                                    QuestionId = section.Questions.First().QuestionId,
                                    MediaSource = files.First()
                                });
                            }
                        }
                        else
                        {
                            foreach (var language in languages)
                            {
                                var languageMediaDirectory = $"{sectionMediaDirectory}\\{language.Id}";

                                if (Directory.Exists(languageMediaDirectory))
                                {
                                    var files = Directory.GetFiles(languageMediaDirectory);

                                    if (files != null && files.Any())
                                    {
                                        foreach (var question in section.Questions)
                                        {
                                            var file = files.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x).EndsWith(question.QuestionOrder.ToString()));

                                            if (!string.IsNullOrEmpty(file))
                                            {
                                                _dashboardRepository.InsertQuestionMedia(new QuestionMediaData
                                                {
                                                    LanguageId = language.Id,
                                                    QuestionId = question.QuestionId,
                                                    MediaSource = file
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private LessonSetData InsertLessonSet(LessonSetData lessonSet, IEnumerable<LessonCommand> lessons)
        {
            var newLessonSet = _dashboardRepository.InsertLessonSet(new LessonSetData
            {
                LevelId = lessonSet.LevelId,
                LessonSetId = lessonSet.LessonSetId,
                LessonSetOrder = lessonSet.LessonSetOrder
            });

            newLessonSet.Lessons = new List<LessonData>();

            for (int i = 1; i <= Constants.NumberOfLessonsInSet; i++)
            {
                var newLesson = lessons.Where(x => x.LessonNumber != Constants.AssessmentLessonNumber).Take(i).LastOrDefault();

                newLessonSet.Lessons.Add(InsertLesson(new LessonData
                {
                    LessonCode = GetLessonCode(lessonSet.LevelId, newLesson.LessonNumber),
                    LessonDescription = $"Lesson {newLesson.LessonNumber}",
                    LessonId = Guid.NewGuid().ToString(),
                    LessonName = $"Lesson {newLesson.LessonNumber}",
                    LessonNumber = newLesson.LessonNumber,
                    LessonSetId = newLessonSet.LessonSetId
                }, newLesson.LessonSections));
            }

            newLessonSet.Lessons.Add(InsertAssesment(new LessonData
            {
                LessonCode = GetLessonCode(4, lessonSet.LessonSetOrder),
                LessonDescription = $"Assessment {lessonSet.LessonSetOrder}",
                LessonId = Guid.NewGuid().ToString(),
                LessonName = $"Assessment {lessonSet.LessonSetOrder}",
                LessonNumber = Constants.AssessmentLessonNumber,
                LessonSetId = newLessonSet.LessonSetId
            }, lessons.FirstOrDefault(x => x.LessonNumber == Constants.AssessmentLessonNumber).LessonSections));

            return newLessonSet;
        }

        private LessonSetData InsertSameLessonSet(LessonSetData lessonSet, IEnumerable<LessonCommand> lessons)
        {
            //var newLessonSet = _dashboardRepository.InsertSameLessonSet(new LessonSetData
            //{
            //    LevelId = lessonSet.LevelId,
            //    LessonSetId = lessonSet.LessonSetId,
            //    LessonSetOrder = lessonSet.LessonSetOrder
            //});

            var newLessonSet = lessonSet;

            newLessonSet.Lessons = new List<LessonData>();

            for (int i = 1; i <= Constants.NumberOfLessonsInSet; i++)
            {
                var newLesson = lessons.Where(x => x.LessonNumber != Constants.AssessmentLessonNumber).Take(i).LastOrDefault();

                newLessonSet.Lessons.Add(InsertLesson(new LessonData
                {
                    LessonCode = GetLessonCode(lessonSet.LevelId, newLesson.LessonNumber),
                    LessonDescription = $"Lesson {newLesson.LessonNumber}",
                    LessonId = Guid.NewGuid().ToString(),
                    LessonName = $"Lesson {newLesson.LessonNumber}",
                    LessonNumber = newLesson.LessonNumber,
                    LessonSetId = newLessonSet.LessonSetId
                }, newLesson.LessonSections));
            }

            newLessonSet.Lessons.Add(InsertAssesment(new LessonData
            {
                LessonCode = GetLessonCode(4, lessonSet.LessonSetOrder),
                LessonDescription = $"Assessment {lessonSet.LessonSetOrder}",
                LessonId = Guid.NewGuid().ToString(),
                LessonName = $"Assessment {lessonSet.LessonSetOrder}",
                LessonNumber = Constants.AssessmentLessonNumber,
                LessonSetId = newLessonSet.LessonSetId
            }, lessons.FirstOrDefault(x => x.LessonNumber == Constants.AssessmentLessonNumber).LessonSections));

            return newLessonSet;
        }

        private LessonData InsertLesson(LessonData lesson, IEnumerable<LessonSectionCommand> lessonSections)
        {
            var newLesson = _dashboardRepository.InsertLesson(lesson);

            newLesson.LessonSections = new List<LessonSectionData>();

            newLesson.LessonSections.Add(InsertLessonSection(new LessonSectionData
            {
                LessonId = newLesson.LessonId,
                LessonSectionDescription = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ListenKeenlySectionIdentifier).LessonSectionName,
                LessonSectionName = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ListenKeenlySectionIdentifier).LessonSectionName,
                LessonSectionId = Guid.NewGuid().ToString(),
                SectionTypeId = 1,
                LessonSectionInstructions = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ListenKeenlySectionIdentifier).LessonSectionName + " Instructions"
            }, lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ListenKeenlySectionIdentifier).Questions));

            newLesson.LessonSections.Add(InsertLessonSection(new LessonSectionData
            {
                LessonId = newLesson.LessonId,
                LessonSectionDescription = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.SpeakWellSectionIdentifier).LessonSectionName,
                LessonSectionName = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.SpeakWellSectionIdentifier).LessonSectionName,
                LessonSectionId = Guid.NewGuid().ToString(),
                SectionTypeId = 2,
                LessonSectionInstructions = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.SpeakWellSectionIdentifier).LessonSectionName + " Instructions"
            }, lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.SpeakWellSectionIdentifier).Questions));

            newLesson.LessonSections.Add(InsertLessonSection(new LessonSectionData
            {
                LessonId = newLesson.LessonId,
                LessonSectionDescription = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ReadAloudSectionIdentifier).LessonSectionName,
                LessonSectionName = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ReadAloudSectionIdentifier).LessonSectionName,
                LessonSectionId = Guid.NewGuid().ToString(),
                SectionTypeId = 3,
                LessonSectionInstructions = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ReadAloudSectionIdentifier).LessonSectionName + " Instructions"
            }, lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.ReadAloudSectionIdentifier).Questions));

            newLesson.LessonSections.Add(InsertLessonSection(new LessonSectionData
            {
                LessonId = newLesson.LessonId,
                LessonSectionDescription = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.WriteRightSectionIdentifier).LessonSectionDescription,
                LessonSectionName = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.WriteRightSectionIdentifier).LessonSectionName,
                LessonSectionId = Guid.NewGuid().ToString(),
                SectionTypeId = 4,
                LessonSectionInstructions = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.WriteRightSectionIdentifier).LessonSectionName + " Instructions"
            }, lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.WriteRightSectionIdentifier).Questions));

            return newLesson;
        }

        private LessonData InsertAssesment(LessonData lesson, IEnumerable<LessonSectionCommand> lessonSections)
        {
            var newLesson = _dashboardRepository.InsertLesson(lesson);

            newLesson.LessonSections = new List<LessonSectionData>();

            newLesson.LessonSections.Add(InsertLessonSection(new LessonSectionData
            {
                LessonId = newLesson.LessonId,
                LessonSectionDescription = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.AssessmentIdentifier).LessonSectionName,
                LessonSectionName = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.AssessmentIdentifier).LessonSectionName,
                LessonSectionId = Guid.NewGuid().ToString(),
                SectionTypeId = 5,
                LessonSectionInstructions = lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.AssessmentIdentifier).LessonSectionName + " Instructions"
            }, lessonSections.FirstOrDefault(x => x.SectionIdentifier == Constants.AssessmentIdentifier).Questions));

            return newLesson;
        }

        private LessonSectionData InsertLessonSection(LessonSectionData lessonSection, IEnumerable<QuestionCommand> questions)
        {
            var newLessonSection = _dashboardRepository.InsertLessonSection(lessonSection);

            newLessonSection.Questions = new List<QuestionData>();

            foreach (var question in questions.OrderBy(x => x.QuestionOrder))
            {
                var newQuestion = InsertQuestion(newLessonSection.SectionTypeId, new QuestionData
                {
                    LessonSectionId = newLessonSection.LessonSectionId,
                    QuestionId = Guid.NewGuid().ToString(),
                    QuestionOrder = question.QuestionOrder,
                    QuestionText = question.QuestionText,
                    RecommendedAttempts = GetAttempts(lessonSection.SectionTypeId),
                    SecondaryAttempts = GetSecondaryAttempts(lessonSection.SectionTypeId)
                }, question.SubQuestions);

                newLessonSection.Questions.Add(newQuestion);
            }

            return newLessonSection;
        }

        private QuestionData InsertQuestion(int sectionTypeId, QuestionData question, IEnumerable<SubQuestionCommand> subQuestions)
        {
            var newQuestion = _dashboardRepository.InsertQuestion(question);

            if (subQuestions != null && subQuestions.Any())
            {
                newQuestion.SubQuestions = new List<SubQuestionData>();

                foreach (var subQuestion in subQuestions.OrderBy(x => x.QuestionOrder))
                {
                    var newSubQuestion = InsertSubQuestion(new SubQuestionData
                    {
                        QuestionId = newQuestion.QuestionId,
                        QuestionOrder = subQuestion.QuestionOrder,
                        QuestionText = subQuestion.QuestionText,
                        QuestionTypeId = GetQuestionTypeId(sectionTypeId, newQuestion.QuestionOrder),
                        SubQuestionOptions = subQuestion.SubQuestionOptions.OrderBy(x => x.OptionOrder).Select(x => new SubQuestionOptionData
                        {
                            OptionOrder = x.OptionOrder,
                            OptionText = x.OptionText,
                            IsAnswer = x.IsAnswer,
                            AnswerOrder = x.AnswerOrder
                        })
                    });

                    newQuestion.SubQuestions.Add(newSubQuestion);
                }
            }

            return newQuestion;
        }

        private SubQuestionData InsertSubQuestion(SubQuestionData subQuestion)
            => _dashboardRepository.InsertSubQuestion(subQuestion);

        private int GetQuestionTypeId(int sectionTypeId, int questionOrder)
        {
            switch (sectionTypeId)
            {
                case 1:
                    return 1;
                case 4:
                    return questionOrder == 1 ? 3 : questionOrder == 2 ? 5 : questionOrder == 3 ? 2 : 0;
                case 5:
                    return 4;
                default:
                    return 0;
            }
        }

        
        private int? GetSecondaryAttempts(int sectionTypeId)
        {
            switch (sectionTypeId)
            {
                case 1:
                case 4:
                    return 54;
                case 5:
                    return 1;
                default:
                    return null;
            }
        }

        private int? GetAttempts(int sectionTypeId)
        {
            switch (sectionTypeId)
            {
                case 1:
                case 4:
                    return 6;
                case 5:
                    return 1;
                default:
                    return null;
            }
        }

        private string GetLessonCode(int levelId, int lessonNumber)
        {
            switch (levelId)
            {
                case 1:
                    return $"BAS_LES_{lessonNumber}";
                case 2:
                    return $"INT_LES_{lessonNumber}";
                case 3:
                    return $"EXP_LES_{lessonNumber}";
                case 4:
                    return $"ASSESSMENT_{lessonNumber}";
                default:
                    return string.Empty;
            }
        }

        private IEnumerable<LessonSetData> GetAllLessonSets()
        {
            var lessonSets = _cache.Get<IEnumerable<LessonSetData>>(Constants.LessonSetCache);

            if (lessonSets == null)
            {
                lessonSets = _dashboardRepository.GetAllLessonSets();
                _cache.Set(Constants.LessonSetCache, lessonSets, TimeSpan.FromMinutes(Constants.LessonSetCacheDuration));
            }

            return lessonSets;
        }
    }
}
