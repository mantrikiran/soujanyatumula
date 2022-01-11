using Dapper;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Utilities;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.Dashboard;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Entities.Dashboard.SubQuestion;
using VidyaVahini.Entities.Dashboard.LessonSection;

namespace VidyaVahini.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private MySqlConnection _dbconnection;
        private MySqlConnection _dbconnection1;
        private MySqlConnection _dbconnection2;
        private string Connectionstring = "VidyaVahiniDb";

        public DashboardRepository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _dbconnection = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            _dbconnection1 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            _dbconnection2 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
        }

        public bool DeleteLessonSet(string lessonSetId)
        {
            var lessonSet = _unitOfWork.Repository<LessonSet>()
                .Find(x => string.Equals(x.LessonSetId, lessonSetId, StringComparison.OrdinalIgnoreCase));

            if (lessonSet == null)
            {
                return false;
            }

            _unitOfWork.Repository<LessonSet>().Delete(lessonSet);

            return _unitOfWork.Commit() > 0;
        }

        public IEnumerable<LessonSetData> GetAllLessonSets()
            => _unitOfWork.Repository<LessonSet>()
            .Filter(orderBy: x => x.OrderBy(x => x.LevelId).ThenBy(x => x.LessonSetOrder), includeProperties: Constants.LessonsProperty)
            .Select(x => new LessonSetData
            {
                LessonSetId = x.LessonSetId,
                LevelId = x.LevelId,
                LessonSetOrder = x.LessonSetOrder,
                Lessons = x.Lessons.Select(y => new LessonData
                {
                    LessonCode = y.LessonCode,
                    LessonDescription = y.LessonDescription,
                    LessonId = y.LessonId,
                    LessonName = y.LessonName,
                    LessonNumber = y.LessonNumber
                })?.ToList()
            });

        public DashboardModel GetDashboard(string lessonSetId, int languageId)
        {
            var includeProperties = new StringBuilder(Constants.LevelProperty);
            includeProperties.Append($",{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.LevelProperty}.{Constants.LessonSetsProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}");
            includeProperties.Append($",{Constants.LevelProperty}.{Constants.LessonSetsProperty}.{Constants.LessonsProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.InstructionProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.SectionTypeProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.SubQuestionsProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.QuestionHintsProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.QuestionMediasProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.InstructionProperty}.{Constants.InstructionMediasProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.QuestionHintsProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.QuestionMediasProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.InstructionProperty}.{Constants.InstructionMediasProperty}.{Constants.MediaProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.SubQuestionsProperty}.{Constants.SubQuestionOptionsProperty}");
            includeProperties.Append($",{Constants.LessonsProperty}.{Constants.LessonSectionsProperty}.{Constants.QuestionsProperty}.{Constants.SubQuestionsProperty}.{Constants.SubQuestionAnswersProperty}");

            var lessonSet = _unitOfWork.Repository<LessonSet>()
              .Filter(filter: x => string.Equals(x.LessonSetId, lessonSetId, StringComparison.OrdinalIgnoreCase),
                      includeProperties: includeProperties.ToString())
                      .FirstOrDefault();

            if (lessonSet == null)
                return null;

            return GetDashboard(lessonSet, languageId);
        }

        public DashboardModel GetLessons(string lessonSetId, int languageId)
        {
            var lookup = new Dictionary<int, DashboardModel>();
            this._dbconnection.Query<DashboardModel, DashboardLessonModel, DashboardModel>("GetLessons", (c, p) =>
            {
                DashboardModel dashboardModel;
                if (!lookup.TryGetValue(c.LevelId, out dashboardModel))
                {
                    lookup.Add(c.LevelId, dashboardModel = c);
                }
                if (dashboardModel.Lessons == null)
                    dashboardModel.Lessons = new List<DashboardLessonModel>();
                dashboardModel.Lessons.Add(p);
                return dashboardModel;
            }, new { _lessonSetId = lessonSetId }, splitOn: "LessonSetId,LevelId,LessonId", commandType: CommandType.StoredProcedure).AsQueryable();
            var resultList = lookup.Values;
            return resultList.FirstOrDefault();
        }
        public DashboardLessonModel GetLessonSections(string lessonSetId, string lessonId, int languageId)
        {            
            int _totalAnswer = 0;
            List<int> SubAns3 = new List<int>();
            int _maxScoreCount = 0;
            int _maxScoreCount1 = 0;
            var lookup = new Dictionary<int, DashboardLessonModel>();
            var lookupls = new Dictionary<string, DashboardLessonSectionModel>();
            var lookupq = new Dictionary<string, DashboardQuestionModel>();
            var lookupqm = new Dictionary<int, DashboardQuestionMediaModel>();
            var lookupsq = new Dictionary<int, DashboardSubQuestionModel>();
            var lookupsqo = new Dictionary<int, DashboardSubQuestionOptionModel>();
            var lookupsqa = new Dictionary<int, DashboardSubQuestionAnswerModel>();
            this._dbconnection.Query<DashboardLessonModel>("GetLessonSections", new[] {
        typeof(DashboardLessonModel),
        typeof(DashboardLessonSectionModel),
         typeof(DashboardSectionInstructionModel),
        typeof(DashboardQuestionModel),
        typeof(DashboardQuestionMediaModel),
        typeof(DashboardSubQuestionModel),
        typeof(DashboardSubQuestionOptionModel),
        typeof(DashboardSubQuestionAnswerModel)
    }, obj =>
    {

        DashboardLessonModel c = obj[0] as DashboardLessonModel;
        DashboardLessonSectionModel i = obj[1] as DashboardLessonSectionModel;
        DashboardSectionInstructionModel p = obj[2] as DashboardSectionInstructionModel;
        DashboardQuestionModel q = obj[3] as DashboardQuestionModel;
        DashboardQuestionMediaModel qm = obj[4] as DashboardQuestionMediaModel;
        DashboardSubQuestionModel sq = obj[5] as DashboardSubQuestionModel;
        DashboardSubQuestionOptionModel sqo = obj[6] as DashboardSubQuestionOptionModel;
        DashboardSubQuestionAnswerModel sqa = obj[7] as DashboardSubQuestionAnswerModel;

        
        DashboardLessonModel dashboardLessonModel;
        if (!lookup.TryGetValue(c.LessonNumber, out dashboardLessonModel))
        {
            lookup.Add(c.LessonNumber, dashboardLessonModel = c);
        }
        DashboardLessonSectionModel dashboardLessonSectionModel;
        if (!lookupls.TryGetValue(i.SectionId, out dashboardLessonSectionModel))
        {
            lookupls.Add(i.SectionId, dashboardLessonSectionModel = i);           
            if (dashboardLessonModel.LessonSections == null)
                dashboardLessonModel.LessonSections = new List<DashboardLessonSectionModel>();
            dashboardLessonModel.LessonSections.Add(i);
        }
        var item = dashboardLessonModel.LessonSections.Last();       
        if (item.Instructions == null)
        {
            item.Instructions = new DashboardSectionInstructionModel();
            item.Instructions = p;
        }
        DashboardQuestionModel dashboardQuestionModel;
        if (item.Questions == null)
            item.Questions = new List<DashboardQuestionModel>();
        if (!lookupq.TryGetValue(q.QuestionId, out dashboardQuestionModel))
        {            
            lookupsqa.Clear();
            _totalAnswer = 0;          

            lookupq.Add(q.QuestionId, dashboardQuestionModel = q);
            q.Hints = new List<DashboardQuestionHintModel>();
            //item.Questions = item.Questions.OrderBy(x => x.QuestionOrder).ToList();
            item.Questions.Add(q);            
        }
        var _sq = item.Questions.Last();
        DashboardQuestionMediaModel dashboardQuestionMediaModel;
        if (_sq.Media == null)
            _sq.Media = new List<DashboardQuestionMediaModel>();
        if (qm != null)
        {
            if (!lookupqm.TryGetValue(qm.QuestionMediaId, out dashboardQuestionMediaModel))
            {
                lookupqm.Add(qm.QuestionMediaId, dashboardQuestionMediaModel = qm);
                qm.MediaSource = Util.GetMedia(qm.MediaTypeId, qm.MediaSource);
                _sq.Media.Add(qm);
            }
        }
        DashboardSubQuestionModel dashboardSubQuestionModel;
        List<int> SubQuesAns = new List<int>();      
        
        if (_sq.SubQuestions == null)
            _sq.SubQuestions = new List<DashboardSubQuestionModel>();
        if (sq != null)
        {
            if (!lookupsq.TryGetValue(sq.SubQuestionId, out dashboardSubQuestionModel))
            {
                lookupsq.Add(sq.SubQuestionId, dashboardSubQuestionModel = sq);
                _sq.SubQuestions.Add(sq);
                DataTable _dt1 = new DataTable();
                _dt1.Load(_dbconnection2.ExecuteReader("select * from subquestionanswer where subquestionid=" + sq.SubQuestionId));
                List<int> crrtans = new List<int>();
                for (int j = 0; j <= _dt1.Rows.Count - 1; j++)
                {
                    crrtans.Add(Convert.ToInt32(_dt1.Rows[j]["SubQuestionoptionId"]));
                }
                sq.SubQuestionAnswer = crrtans.AsEnumerable();
                _maxScoreCount1 = sq.SubQuestionAnswer.Count();
                _dbconnection2.Close();
                _dbconnection2.Open();
            }
            
        }
        DashboardSubQuestionAnswerModel dashboardSubQuestionAnswerModel;
        
        if (_sq.SubQuestions.Count > 0)
        {
            if (sqa != null)
            {
                if (!lookupsqa.TryGetValue(sqa.SubQuestionId, out dashboardSubQuestionAnswerModel))
                {
                    lookupsqa.Add(sqa.SubQuestionId, dashboardSubQuestionAnswerModel = sqa); 
                }     
            }
            if (sq.QuestionTypeId == 1 || sq.QuestionTypeId == 3)
            {
                _sq.MaximumScore = lookupsqa.Count;
            }
            else if (sq.QuestionTypeId == 2)
            {
                _sq.MaximumScore = _maxScoreCount1;
            }
            else
            {
                if (sqa.SubQuestionOptionId == sqo.SubQuestionOptionId)
                {
                    _maxScoreCount = _maxScoreCount + 1;
                    _sq.MaximumScore = _maxScoreCount;
                }

            }
              
           // _sq.MaximumScore = GetMaxScore(_sq.SubQuestions);
        }
        DashboardSubQuestionOptionModel dashboardSubQuestionOptionModel;
        if (_sq.SubQuestions.Count > 0)
        {
            var _sqo = _sq.SubQuestions.Last();
           
            if (_sqo.Options == null)
                _sqo.Options = new List<DashboardSubQuestionOptionModel>();
            if (!lookupsqo.TryGetValue(sqo.SubQuestionOptionId, out dashboardSubQuestionOptionModel))
            {
                lookupsqo.Add(sqo.SubQuestionOptionId, dashboardSubQuestionOptionModel = sqo);
                _sqo.Options.Add(sqo);
            }
           
            if(sq.QuestionTypeId == 4 || sq.QuestionTypeId == 3)
            {
                _sqo.TotalAnswer = _sqo.Options.Count;                 
            }
            
                if ( sq.QuestionTypeId == 2)
                {                
                    if (sqo.SubQuestionOptionId == sqa.SubQuestionOptionId)
                    {
                        _totalAnswer = _totalAnswer + 1;                  
                    }
                    _sqo.TotalAnswer = _totalAnswer;
                    
                }           
               
            if (sq.QuestionTypeId == 1)
               {
                if (sqo.SubQuestionOptionId == sqa.SubQuestionOptionId)
                {
                    _totalAnswer = 0;
                    _totalAnswer = _totalAnswer + 1;
                }
                _sqo.TotalAnswer = _totalAnswer;
               }
        }
        return dashboardLessonModel;         

    } 
    , new { _lessonSetId = lessonSetId, _lessonId = lessonId,_languageId = languageId}, splitOn: "LessonId,SectionId,InstructionId,QuestionId,QuestionMediaId,SubQuestionId,subQuestionOptionId,SubQuestionId", commandType: CommandType.StoredProcedure).AsQueryable();
            var resultList = lookup.Values;
            return resultList.FirstOrDefault();
        }


        public DashboardSectionTextModel GetSectiontext(int SectionTypeId,int languageId)
        {
            var SectionInstruction = new DashboardSectionTextModel();

            _dbconnection1 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
            _dbconnection1 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));


            //this._dbconnection1.Query<DataTable>("select * from sectioninstruction where SectionTypeid = '" + SectionTypeId + "'" + "and" +" " + "languageid='" + languageId + "'", new[] {
            //    typeof(DataTable),
            //}, obj =>
            //{
            //    DataTable Sections = new DataTable();


            //    return (Sections);


            //});
            //_dbconnection1.Close();
            //_dbconnection1 = null;
            DataTable _dt = new DataTable();
            _dt.Load(_dbconnection1.ExecuteReader("select * from sectioninstruction where SectionTypeid = '" + SectionTypeId + "'" + "and" + " " + "languageid='" + languageId + "'"));

            SectionInstruction.SectionTypeId = int.Parse(_dt.Rows[0]["SectionTypeId"].ToString());

            SectionInstruction.LangauageId = int.Parse(_dt.Rows[0]["LanguageId"].ToString());

            SectionInstruction.Sectiontext = _dt.Rows[0]["SectionText"].ToString();

            SectionInstruction.SectionHeader = _dt.Rows[0]["SectionHeader"].ToString();


            return SectionInstruction;

        }
        public LessonSetData InsertLessonSet(LessonSetData lessonSet)
        {
            _unitOfWork.Repository<LessonSet>()
                .Add(new LessonSet
                {
                    LessonSetId = lessonSet.LessonSetId,
                    LessonSetOrder = lessonSet.LessonSetOrder,
                    LevelId = lessonSet.LevelId
                });

            _unitOfWork.Commit();

            return lessonSet;
        }

        public LessonSetData InsertSameLessonSet(LessonSetData lessonSet)
        {
            _unitOfWork.Repository<LessonSet>()
                .Update(new LessonSet
                {
                    LessonSetId = lessonSet.LessonSetId,
                    LessonSetOrder = lessonSet.LessonSetOrder,
                    LevelId = lessonSet.LevelId
                });

            _unitOfWork.Commit();

            return lessonSet;
        }
        public LessonData InsertLesson(LessonData lesson)
        {
            _unitOfWork.Repository<Lesson>()
                .Add(new Lesson
                {
                    LessonCode = lesson.LessonCode,
                    LessonDescription = lesson.LessonDescription,
                    LessonId = lesson.LessonId,
                    LessonName = lesson.LessonName,
                    LessonNumber = lesson.LessonNumber,
                    LessonSetId = lesson.LessonSetId
                });

            _unitOfWork.Commit();

            return lesson;
        }

        public LessonSectionData InsertLessonSection(LessonSectionData lessonSection)
        {
            _unitOfWork.Repository<LessonSection>()
                .Add(new LessonSection
                {
                    LessonId = lessonSection.LessonId,
                    LessonSectionDescription = lessonSection.LessonSectionDescription,
                    LessonSectionId = lessonSection.LessonSectionId,
                    LessonSectionName = lessonSection.LessonSectionName,
                    SectionTypeId = lessonSection.SectionTypeId,
                    Instruction = new Instruction
                    {
                        InstructionDescription = lessonSection.LessonSectionInstructions,
                        LessonSectionId = lessonSection.LessonSectionId,
                        InstructionMedias = new List<InstructionMedia>()
                        {
                            new InstructionMedia()
                            {
                                LanguageId = Constants.FallbackLanguage,
                                MediaDescription = lessonSection.LessonSectionDescription,
                                Media = new Media()
                                {
                                    MediaSource = "https://www.youtube.com/embed/zTTmU58JWLk",
                                    MediaTypeId = 2,
                                    MediaId = Guid.NewGuid().ToString()
                                }
                            }
                        }
                    }
                });

            _unitOfWork.Commit();

            return lessonSection;
        }

        public QuestionData InsertQuestion(QuestionData questionData)
        {
            _unitOfWork.Repository<Question>()
                .Add(new Question
                {
                    LessonSectionId = questionData.LessonSectionId,
                    QuestionId = questionData.QuestionId,
                    QuestionOrder = questionData.QuestionOrder,
                    QuestionText = questionData.QuestionText,
                    RecommendedAttempts = questionData.RecommendedAttempts,
                    SecondaryAttempts = questionData.SecondaryAttempts
                });

            _unitOfWork.Commit();

            return questionData;
        }

        public QuestionMediaData InsertQuestionMedia(QuestionMediaData questionMedia)
        {
            _unitOfWork.Repository<QuestionMedia>()
                .Add(new QuestionMedia
                {
                    QuestionId = questionMedia.QuestionId,
                    LanguageId = questionMedia.LanguageId,
                    Media = new Media()
                    {
                        MediaSource = questionMedia.MediaSource,
                        MediaTypeId = 1,
                        MediaId = Guid.NewGuid().ToString()
                    }
                });

            _unitOfWork.Commit();

            return questionMedia;
        }

        public SubQuestionData InsertSubQuestion(SubQuestionData subQuestion)
        {
            var newSubQuestion = _unitOfWork.Repository<SubQuestion>()
                .Add(new SubQuestion
                {
                    QuestionId = subQuestion.QuestionId,
                    QuestionOrder = subQuestion.QuestionOrder,
                    QuestionText = subQuestion.QuestionText,
                    QuestionTypeId = subQuestion.QuestionTypeId
                });

            _unitOfWork.Commit();

            newSubQuestion.SubQuestionOptions = subQuestion
                .SubQuestionOptions
                .Select(x => new SubQuestionOption
                {
                    SubQuestionId = newSubQuestion.SubQuestionId,
                    OptionOrder = x.OptionOrder,
                    OptionText = x.OptionText,
                    OptionHidden = false,
                    SubQuestionAnswers = x.IsAnswer ? new List<SubQuestionAnswer>()
                    {
                        new SubQuestionAnswer()
                        {
                            SubQuestionId = newSubQuestion.SubQuestionId,
                            AnswerOrder = x.AnswerOrder
                        }
                    } : null
                }).ToList();

            _unitOfWork.Commit();

            return subQuestion;
        }

        private DashboardModel GetDashboard(LessonSet lessonSet, int languageId)
        => new DashboardModel
        {
            LevelId = lessonSet.LevelId,
            LevelCode = lessonSet.Level.LevelCode,
            TotalLessonsInCurrentSet = lessonSet.Lessons.Where(x => x.LessonNumber != Constants.AssessmentLessonNumber).Count(),
            TotalLessonsInLevel = lessonSet.Level.LessonSets.SelectMany(x => x.Lessons).Where(x => x.LessonNumber != Constants.AssessmentLessonNumber).Count(),
            LessonSetId = lessonSet.LessonSetId
            //,
            //Lessons = lessonSet
            //    .Lessons
            //    //.Where(x=> x.LessonId ==lessonid)
            //    .OrderBy(x => x.LessonNumber)
            //    .Select(x => new DashboardLessonModel
            //    {
            //        LessonId = x.LessonId,
            //        LessonCode = x.LessonCode,
            //        LessonNumber = x.LessonNumber,
            //        LessonSections = x
            //            .LessonSections
            //            .OrderBy(x => x.SectionTypeId)
            //            .Select(x => new DashboardLessonSectionModel
            //            {
            //                SectionId = x.LessonSectionId,
            //                SectionCode = x.SectionType.SectionTypeCode,
            //                SectionName = x.LessonSectionName,
            //                SectionDescription = x.LessonSectionDescription,
            //                Instructions = GetSectionInstructions(x.Instruction, languageId),
            //                Questions = x
            //                    .Questions
            //                    .OrderBy(x => x.QuestionOrder)
            //                    .Select(x => new DashboardQuestionModel
            //                    {
            //                        QuestionId = x.QuestionId,
            //                        QuestionText = x.QuestionText,
            //                        RecommendedAttempts = x.RecommendedAttempts ?? 0,
            //                        SecondaryAttempts = x.SecondaryAttempts ?? 0,
            //                        MaximumScore = GetMaxScore(x.SubQuestions),
            //                        Hints = GetQuestionHints(x.QuestionHints),
            //                        Media = GetQuestionMedia(x.QuestionMedias, languageId),
            //                        SubQuestions = GetSubQuestions(x.SubQuestions)
            //                    })
            //            })
            //    })
        };

        private DashboardSectionInstructionModel GetSectionInstructions(Instruction instruction, int languageId)
        {
            if (instruction == null)
                return null;

            var instructionMedia = instruction.InstructionMedias.FirstOrDefault(x => x.LanguageId == languageId);

            if (instructionMedia == null && Constants.EnableLanguageFallback && languageId != Constants.FallbackLanguage)
            {
                instructionMedia = instruction.InstructionMedias.FirstOrDefault(x => x.LanguageId == Constants.FallbackLanguage);
            }

            return new DashboardSectionInstructionModel
            {
                InstructionId = instruction.InstructionId,
                LanguageId = instructionMedia?.LanguageId ?? 0,
                MediaId = instructionMedia?.Media?.MediaId,
                MediaTypeId = instructionMedia?.Media?.MediaTypeId ?? 0,
                MediaSource = Util.GetMedia(instructionMedia?.Media?.MediaTypeId, instructionMedia?.Media?.MediaSource)
            };
        }

        private IEnumerable<DashboardQuestionHintModel> GetQuestionHints(IEnumerable<QuestionHint> hints)
            => hints == null ? null : hints
            .Select(x => new DashboardQuestionHintModel
            {
                QuestionHintId = x.QuestionHintId,
                HintText = x.HintText,
                MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
            });

        private IEnumerable<DashboardQuestionMediaModel> GetQuestionMedia(IEnumerable<QuestionMedia> questionMedia, int languageId)
            => questionMedia == null || !questionMedia.Any() ? null : questionMedia
            .Where(x => x.LanguageId == languageId || x.LanguageId == Constants.EnglishLanguage)
            .Select(x => new DashboardQuestionMediaModel
            {
                LanguageId = x.LanguageId,
                MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
            });

        private IEnumerable<DashboardSubQuestionModel> GetSubQuestions(IEnumerable<SubQuestion> subQuestions)
            => subQuestions == null || !subQuestions.Any() ? null : subQuestions
            .Select(x => new DashboardSubQuestionModel
            {
                SubQuestionId = x.SubQuestionId,
                QuestionTypeId = x.QuestionTypeId,
                SubQuestionOrder = x.QuestionOrder,
                SubQuestionText = x.QuestionText,
                TotalAnswer = x.SubQuestionAnswers.Count()
                //,
                //Options = x.SubQuestionOptions
                //.Select(x => new DashboardSubQuestionOptionModel
                //{
                //    SubQuestionOptionId = x.SubQuestionOptionId,
                //    SubQuestionOptionOrder = x.OptionOrder,
                //    SubQuestionOptionText = x.OptionText
                //})
            });

        private int GetMaxScore(IEnumerable<SubQuestion> subQuestions)
        {
            var maxScore = 0;

            if (subQuestions != null)
            {
                foreach (var subQuestion in subQuestions)
                {
                    switch (subQuestion.QuestionTypeId)
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
            }

            return maxScore;
        }
    }
}
