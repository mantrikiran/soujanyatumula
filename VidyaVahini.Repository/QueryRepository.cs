using Dapper;
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
using VidyaVahini.Entities.Query;
using VidyaVahini.Entities.Teacher;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class QueryRepository : IQueryRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IDataAccessRepository<Query> _query;
        private readonly IDataAccessRepository<UserProfile> _userProfile;
        private readonly IDataAccessRepository<Teacher> _teacher;
        private readonly IDataAccessRepository<QueryData> _queryData;
        private readonly IDataAccessRepository<Media> _mediaRepository;
        private readonly IDataAccessRepository<UserLanguage> _userlanguage;
        private readonly IDataAccessRepository<Question> _question;
        private readonly IDataAccessRepository<Lesson> _lesson;
        private readonly IDataAccessRepository<SubQuestion> _subQuestion;
        private readonly IDataAccessRepository<LessonSection> _lessonSection;
        private readonly IDataAccessRepository<SectionType> _sectionType;
        
        private MySqlConnection _dbconnection2;
        private string Connectionstring = "VidyaVahiniDb";

        public QueryRepository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _query = _unitOfWork.Repository<Query>();
            _userProfile = _unitOfWork.Repository<UserProfile>();
            _teacher = _unitOfWork.Repository<Teacher>();
            _queryData = _unitOfWork.Repository<QueryData>();
            _mediaRepository = _unitOfWork.Repository<Media>();
            _userlanguage = _unitOfWork.Repository<UserLanguage>();
            _question = _unitOfWork.Repository<Question>();
            _lesson = _unitOfWork.Repository<Lesson>();
            _subQuestion = _unitOfWork.Repository<SubQuestion>();
            _lessonSection = _unitOfWork.Repository<LessonSection>();
            _sectionType = _unitOfWork.Repository<SectionType>();
            _configuration = configuration;
            _dbconnection2 = new MySqlConnection(_configuration.GetConnectionString(Connectionstring));
        }

        public bool CreateQuery(string userId, string teacherId, string questionId, int subQuestionId,  string queryText,
            int mediaTypeId, string mediaBase64String, string mediaFileName, string lessonSetId)
        {
            var query = _query
                        .Find(x => string.Equals(x.QuestionId, questionId, StringComparison.OrdinalIgnoreCase)
                            && (string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase)) && x.SubQuestionId == subQuestionId);

            if (query == null)
            {
                query = _query
                    .Add(GetQueryBySubQID(Guid.NewGuid().ToString(), questionId, subQuestionId, teacherId, (int)Enums.QueryStatus.PENDING_WITH_MENTOR));
            }
            else
            {
                query.QueryStatus = (userId == teacherId) ? (int)Enums.QueryStatus.PENDING_WITH_MENTOR : (int)Enums.QueryStatus.PENDING_WITH_TEACHER;
                _query.Update(query);
            }

            string queryMediaFilePath = $@"{ Constants.UserMediaFilePath }\\{userId}\\{lessonSetId}\\{Constants.Queries}\\{query.QueryId}";

            _queryData.Add(GetQueryData(query.QueryId, userId, queryText, queryMediaFilePath, mediaTypeId, mediaBase64String, mediaFileName));


            if (userId == teacherId)
            {

                var teacher = _teacher
                   .Find(x => (string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase)));
                var userProfile = _userProfile
                      .Find(x => (string.Equals(x.UserId, teacherId, StringComparison.OrdinalIgnoreCase)));
                var question = _question
                    .Find(x => (string.Equals(x.QuestionId, questionId, StringComparison.OrdinalIgnoreCase)));
                var lessonsection = _lessonSection
                    .Find(x => (string.Equals(x.LessonSectionId, question.LessonSectionId, StringComparison.OrdinalIgnoreCase)));

                var lesson = _lesson
                   .Find(x => (string.Equals(x.LessonId, lessonsection.LessonId, StringComparison.OrdinalIgnoreCase)));

                var sectiontype = _sectionType
                   .Find(x => (string.Equals(x.SectionTypeId, lessonsection.SectionTypeId)));
                string from = teacherId;
                string to = teacher.MentorId;
                int roleid = 5;
                string message = userProfile.Name + " has  submitted a query  for the " + lesson.LessonName + "  " + sectiontype.SectionTypeDescription + " section";
                string created_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int status = 1;
                _unitOfWork
                   .Repository<Notifications>()
                   .Add(GetNotificationdetails(from, to, roleid, message, created_date, status));
                //string insertquery = "insert into notifications (`from`,`to`,roleid,message,created_date,status) values ('" + from + "','" + to + "'," + roleid + ",'" + message + "' ,'" + created_date + "' ," + status + ")";
                //_dbconnection2.Execute(insertquery);
                //_dbconnection2.Close();
                //_dbconnection2 = null;
            }
            else
            {
                var teacher = _teacher
                       .Find(x => (string.Equals(x.TeacherId, teacherId, StringComparison.OrdinalIgnoreCase)));
                var userProfile = _userProfile
                      .Find(x => (string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase)));
                var question = _question
                    .Find(x => (string.Equals(x.QuestionId, questionId, StringComparison.OrdinalIgnoreCase)));
                var lessonsection = _lessonSection
                    .Find(x => (string.Equals(x.LessonSectionId, question.LessonSectionId, StringComparison.OrdinalIgnoreCase)));

                var lesson = _lesson
                   .Find(x => (string.Equals(x.LessonId, lessonsection.LessonId, StringComparison.OrdinalIgnoreCase)));

                var sectiontype = _sectionType
                   .Find(x => (string.Equals(x.SectionTypeId, lessonsection.SectionTypeId)));
                string from = teacher.MentorId;
                string to = teacherId;
                int roleid = 4;
                string message = userProfile.Name + "  has replied for the " + lesson.LessonName + "  " + sectiontype.SectionTypeDescription + " section query";

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
        private Query GetQueryBySubQID(string id, string questionId, int subQuestionId, string teacherId, int status)
            => new Query
            {
                QueryId = id,
                QueryStatus = status,
                QuestionId = questionId,
                TeacherId = teacherId,
                SubQuestionId = subQuestionId,
            };

        private Query GetQuery(string id, string questionId, string teacherId, int status)
            => new Query
            {
                QueryId = id,
                QueryStatus = status,
                QuestionId = questionId,
                TeacherId = teacherId,
            };

        private QueryData GetQueryData(string queryId, string userId, string queryText, string queryMediaFilePath, int mediaTypeId, string mediaBase64String, string mediaFileName)
        {
            var queryData = new QueryData
            {
                UserId = userId,
                QueryId = queryId,
                QueryText = queryText,
                Created = Util.GetIstDateTime(),
                LastUpdated = Util.GetIstDateTime()
            };

            if (!string.IsNullOrWhiteSpace(mediaBase64String) && !string.IsNullOrWhiteSpace(mediaFileName))
            {
                string mediaId = Guid.NewGuid().ToString();
                queryData.MediaId = mediaId;
                queryData.Media = new Media
                {
                    MediaId = mediaId,
                    MediaTypeId = mediaTypeId,
                    MediaSource = Util.SaveMediaToFileSystem(mediaBase64String, queryMediaFilePath, mediaFileName),
                    Created = Util.GetIstDateTime(),
                    LastUpdated = Util.GetIstDateTime()
                };
            }

            return queryData;
        }


        public IEnumerable<QueryModel> GetQueries(string userId, string lessonSectionId)
        {
            List<QueryModel> queryModels = new List<QueryModel>();
            var includeQueryProperties = new StringBuilder(Constants.QueryDatasProperty);
            includeQueryProperties.Append($",{Constants.QuestionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}.{Constants.MediaProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.MediaProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.SectionTypeProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.LessonProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}.{Constants.UserProfileProperty}");

            var queries = _query.Filter(x => x.TeacherId == userId && x.Question.LessonSectionId == lessonSectionId,
                includeProperties: includeQueryProperties.ToString());

            if (queries == null)
                return queryModels;

            foreach (var query in queries)
            {
              
                queryModels.Add(new QueryModel
                {
                    QueryId  = query.QueryId,
                    QueryStatus = query.QueryStatus,
                    QuestionText = query.Question?.QuestionText,
                    QuestionId = query.QuestionId,                  
                    LessonSectionId = query.Question?.LessonSection?.LessonSectionId,
                    LessonSectionCode = query.Question?.LessonSection?.SectionType?.SectionTypeCode,
                    LessonSectionName = query.Question?.LessonSection?.LessonSectionName,
                    LessonId = query.Question?.LessonSection?.LessonId,
                    LessonCode = query.Question?.LessonSection?.Lesson?.LessonCode,
                    LessonNumber = query.Question?.LessonSection?.Lesson?.LessonNumber.ToString(),
                    LessonSetId = query.Question?.LessonSection?.Lesson?.LessonSetId,
                    QueryQuestionMedia = query.Question?.QuestionMedias?.Select(x => new TeacherQuestionMedia
                    {
                        LanguageId = x.LanguageId,
                        MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
                    }),
                    Responses = query.QueryDatas.Select(x => new Response
                    {
                        UserId = x.UserId,
                        UserName = x.User?.UserProfile?.Name,
                        QueryCreatedDate = x.Created,
                        QueryText = x.QueryText,
                        QueryDataId = x.QueryDataId,
                        MediaStream = Util.GetMedia(x.Media?.MediaTypeId, x.Media?.MediaSource),
                        FileName = Path.GetFileName(x.Media?.MediaSource)
                    }).OrderBy(x => x.QueryCreatedDate)
                });
            }

            return queryModels;
        }
        public IEnumerable<QueryModel> GetAllQueries(string userId, string lessonsetid)
        {
            
            var includeQueryProperties = new StringBuilder(Constants.QueryDatasProperty);
            includeQueryProperties.Append($",{Constants.QuestionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}.{Constants.MediaProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.MediaProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.SectionTypeProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.LessonProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}.{Constants.UserProfileProperty}");


            DataTable _dt1 = new DataTable();            
            _dt1.Load(_dbconnection2.ExecuteReader("select * from lessonsection where lessonid  in (select lessonid from lesson where lessonsetid = '" + lessonsetid + "')"));
            List<QueryModel> crrtans = new List<QueryModel>();
            for (int j = 0; j <= _dt1.Rows.Count - 1; j++)
            {
                List<QueryModel> queryModels = new List<QueryModel>();

                string lsid = _dt1.Rows[j]["LessonSectionId"].ToString();
                 var queries = _query.Filter(x => x.TeacherId == userId 
                && x.Question.LessonSectionId == lsid,
                includeProperties: includeQueryProperties.ToString());

                if (queries == null)
                    return queryModels;

                foreach (var query in queries)
                {
                    var userlanguage = _userlanguage.Filter(x => x.UserId == userId);
                    var sq = _subQuestion.Filter(x => x.SubQuestionId == query.SubQuestionId);
                    string QuestionText = "";
                    foreach (var sq1 in sq)
                    {
                        QuestionText = sq1.QuestionText;
                    }
                    foreach (var ul in userlanguage)
                    {
                        queryModels.Add(new QueryModel
                        {
                            QueryId = query.QueryId,
                            QueryStatus = query.QueryStatus,
                            QuestionText = query.Question?.QuestionText,
                            QuestionId = query.QuestionId,
                            SubQuestionId = query.SubQuestionId,
                            SubQuestionText = QuestionText,
                            LessonSectionId = query.Question?.LessonSection?.LessonSectionId,
                            LessonSectionCode = query.Question?.LessonSection?.SectionType?.SectionTypeCode,
                            LessonSectionName = query.Question?.LessonSection?.LessonSectionName,
                            LessonId = query.Question?.LessonSection?.LessonId,
                            LessonCode = query.Question?.LessonSection?.Lesson?.LessonCode,
                            LessonNumber = query.Question?.LessonSection?.Lesson?.LessonNumber.ToString(),
                            LessonSetId = query.Question?.LessonSection?.Lesson?.LessonSetId,
                            QueryQuestionMedia = query.Question?.QuestionMedias? .Where(x => x.LanguageId == ul.LanguageId || x.LanguageId ==1).Select(x => new TeacherQuestionMedia
                            {
                                //LanguageId = ul.LanguageId,
                                LanguageId = x.LanguageId,
                                MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
                            }),
                            Responses = query.QueryDatas.Select(x => new Response
                            {
                                UserId = x.UserId,
                                UserName = x.User?.UserProfile?.Name,
                                QueryCreatedDate = x.Created,
                                QueryText = x.QueryText,
                                QueryDataId = x.QueryDataId,
                                MediaStream = Util.GetMedia(x.Media?.MediaTypeId, x.Media?.MediaSource),
                                FileName = Path.GetFileName(x.Media?.MediaSource)
                            }).OrderBy(x => x.QueryCreatedDate)
                        });
                   }
                }
                crrtans.AddRange(queryModels);
            }
            _dbconnection2.Close();
            _dbconnection2.Open();
            return crrtans;
        }


        public IEnumerable<QueryModel> GetQueriesbyQID(string userId, string lessonSectionId, string QuestionId)
        {
            List<QueryModel> queryModels = new List<QueryModel>();
            var includeQueryProperties = new StringBuilder(Constants.QueryDatasProperty);
            includeQueryProperties.Append($",{Constants.QuestionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}.{Constants.MediaProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.MediaProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.SectionTypeProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.LessonProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}.{Constants.UserProfileProperty}");

            var queries = _query.Filter(x => x.TeacherId == userId && x.QuestionId == QuestionId && x.Question.LessonSectionId == lessonSectionId,
                includeProperties: includeQueryProperties.ToString());

            if (queries == null)
                return queryModels;

            foreach (var query in queries)
            {
                var sq = _subQuestion.Filter(x => x.SubQuestionId == query.SubQuestionId);
                string QuestionText = "";
                foreach (var sq1 in sq)
                {
                    QuestionText = sq1.QuestionText;
                }
                queryModels.Add(new QueryModel
                {
                    QueryId = query.QueryId,
                    QueryStatus = query.QueryStatus,
                    QuestionText = query.Question?.QuestionText,
                    QuestionId = query.QuestionId,
                    SubQuestionId = query.SubQuestionId,
                    SubQuestionText=QuestionText,
                    LessonSectionId = query.Question?.LessonSection?.LessonSectionId,
                    LessonSectionCode = query.Question?.LessonSection?.SectionType?.SectionTypeCode,
                    LessonSectionName = query.Question?.LessonSection?.LessonSectionName,
                    LessonId = query.Question?.LessonSection?.LessonId,
                    LessonCode = query.Question?.LessonSection?.Lesson?.LessonCode,
                    LessonNumber = query.Question?.LessonSection?.Lesson?.LessonNumber.ToString(),
                    LessonSetId = query.Question?.LessonSection?.Lesson?.LessonSetId,
                    QueryQuestionMedia = query.Question?.QuestionMedias?.Select(x => new TeacherQuestionMedia
                    {
                        LanguageId = x.LanguageId,
                        MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
                    }),
                    Responses = query.QueryDatas.Select(x => new Response
                    {
                        UserId = x.UserId,
                        UserName = x.User?.UserProfile?.Name,
                        QueryCreatedDate = x.Created,
                        QueryText = x.QueryText,
                        QueryDataId = x.QueryDataId,
                        MediaStream = Util.GetMedia(x.Media?.MediaTypeId, x.Media?.MediaSource),
                        FileName = Path.GetFileName(x.Media?.MediaSource)
                    }).OrderBy(x => x.QueryCreatedDate)
                });
            }

            return queryModels;
        }


        public bool EditQuery(string userId, string queryText, int mediaTypeId,
            string mediaBase64String, string mediaFileName, string lessonSetId, int queryDataId)
        {
            var queryData = _queryData.Filter(x => x.QueryDataId == queryDataId,
                includeProperties: Constants.MediaProperty)?.FirstOrDefault();
            Media mediaRecord = null;

            if (queryData != null)
            {
                queryData.Created = Util.GetIstDateTime();
                queryData.LastUpdated = Util.GetIstDateTime();
                if (!string.IsNullOrWhiteSpace(queryText))
                {
                    queryData.QueryText = queryText;
                    if (queryData.Media != null)
                    {
                        mediaRecord = queryData.Media;
                        Util.DeleteMediaFromFileSystem(queryData.Media.MediaSource);
                        queryData.Media = null;
                    }
                }

                if (!string.IsNullOrWhiteSpace(mediaBase64String) && !string.IsNullOrWhiteSpace(mediaFileName))
                {
                    string queryMediaFilePath = $@"{ Constants.UserMediaFilePath }\\{userId}\\{lessonSetId}\\{Constants.Queries}\\{queryData.QueryId}";

                    queryData.QueryText = string.Empty;

                    if (queryData.Media != null)
                    {
                        Util.DeleteMediaFromFileSystem(queryData.Media.MediaSource);
                    }

                    if (queryData.Media == null)
                    {
                        queryData.Media = new Media
                        {
                            MediaId = Guid.NewGuid().ToString()
                        };
                    }
                    queryData.Media.MediaTypeId = mediaTypeId;
                    queryData.Media.MediaSource = Util.SaveMediaToFileSystem(mediaBase64String, queryMediaFilePath, mediaFileName);
                    queryData.Media.Created = Util.GetIstDateTime();
                    queryData.Media.LastUpdated = Util.GetIstDateTime();
                }
                _queryData.Update(queryData);
                if (mediaRecord != null)
                {
                    _mediaRepository.Delete(mediaRecord);
                }
                return _unitOfWork.Commit() > 0;
            }
            else
            {
                return false;
            }

        }

        public IEnumerable<QueryModel> GetQueriesByQueryIds(IEnumerable<string> queryIds)
        {
            IEnumerable<Query> q1 =null;
            List<Query> lq = new List<Query>();
            List<QueryModel> queryModels = new List<QueryModel>();
            var includeQueryProperties = new StringBuilder(Constants.QueryDatasProperty);
            includeQueryProperties.Append($",{Constants.QuestionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.QuestionMediasProperty}.{Constants.MediaProperty}");
             includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.MediaProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.SectionTypeProperty}");
            includeQueryProperties.Append($",{Constants.QuestionProperty}.{Constants.LessonSectionProperty}.{Constants.LessonProperty}");
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.UserAccountProperty}.{Constants.UserProfileProperty}");
            

            foreach (var qid in queryIds)
            {
                var queries1 = _query.Filter(x => x.QueryId==qid.ToString(),
                   includeProperties: includeQueryProperties.ToString());
                List<Query> lq1 = new List<Query>();

                foreach (var q11 in queries1)
                {
                    lq.Add(q11);
                }                
            }

            var queries = lq.AsEnumerable();



            if (queries == null)
                return queryModels;

            foreach (var query in queries)
            {
                var sq = _subQuestion.Filter(x => x.SubQuestionId == query.SubQuestionId);
                string QuestionText="";
                foreach (var sq1 in sq)
                {
                    QuestionText = sq1.QuestionText;
                }
             
                queryModels.Add(new QueryModel
                {
                    QueryId = query.QueryId,
                    QueryStatus   = query.QueryStatus,
                    QuestionText = query.Question?.QuestionText,
                    QuestionId = query.QuestionId,
                    SubQuestionId=query.SubQuestionId,
                    SubQuestionText= QuestionText,
                    LessonSectionId = query.Question?.LessonSection?.LessonSectionId,
                    LessonSectionCode = query.Question?.LessonSection?.SectionType?.SectionTypeCode,
                    LessonSectionName = query.Question?.LessonSection?.LessonSectionName,
                    LessonId = query.Question?.LessonSection?.LessonId,
                    LessonCode = query.Question?.LessonSection?.Lesson?.LessonCode,
                    LessonNumber = query.Question?.LessonSection?.Lesson?.LessonNumber.ToString(),
                    LessonSetId = query.Question?.LessonSection?.Lesson?.LessonSetId,
                    QueryQuestionMedia = query.Question?.QuestionMedias?.Select(x => new TeacherQuestionMedia
                    {
                        LanguageId = x.LanguageId,
                        MediaSource = Util.GetMedia(x?.Media?.MediaTypeId, x?.Media?.MediaSource)
                    }),
                    Responses = query.QueryDatas.Select(x => new Response
                    {
                        UserId = x.UserId,
                        UserName = x.User?.UserProfile?.Name,
                        QueryCreatedDate = x.Created,
                        QueryText = x.QueryText,
                        QueryDataId = x.QueryDataId,
                        MediaStream = Util.GetMedia(x.Media?.MediaTypeId, x.Media?.MediaSource),
                        FileName = Path.GetFileName(x.Media?.MediaSource)
                    }).OrderBy(x => x.QueryCreatedDate)
                });
            }

            return queryModels;
        }

        public bool DeleteQuery(string queryId)
        {
            var includeQueryProperties = new StringBuilder(Constants.QueryDatasProperty);
            includeQueryProperties.Append($",{Constants.QueryDatasProperty}.{Constants.MediaProperty}");
            var query = _query.Filter(x => x.QueryId == queryId,
                includeProperties: includeQueryProperties.ToString())?.FirstOrDefault();

            var queryDatas = query?.QueryDatas;

            var queryMedias = queryDatas?.Select(x => x.Media);

            if (query != null)
            {
                _query.Delete(query);
            }
            foreach (var queryData in queryDatas)
            {
                if (queryData != null)
                {
                    _queryData.Delete(queryData);
                }
            }

            foreach (var media in queryMedias)
            {
                if (media != null)
                {
                    Util.DeleteMediaFromFileSystem(media.MediaSource);
                    _mediaRepository.Delete(media);
                }
            }

            return _unitOfWork.Commit() > 0;
        }
    }
}

