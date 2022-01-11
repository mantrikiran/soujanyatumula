using System.Collections.Generic;
using System.Data;
using VidyaVahini.Core.Enum;
using VidyaVahini.Entities.Query;
using VidyaVahini.Infrastructure.Exception;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IErrorRepository _errorRepository;

        public QueryService(
            IQueryRepository queryRepository,
            IErrorRepository errorRepository)
        {
            _queryRepository = queryRepository;
            _errorRepository = errorRepository;
        }

        public IEnumerable<QueryModel> GetQueries(string userId, string lessonSectionId, string QuestionId)
        {
            if (QuestionId == "Null" || QuestionId == null || QuestionId == "")
            {
                var queryModels = _queryRepository.GetQueries(userId, lessonSectionId);
                foreach (var query in queryModels)
                {
                    foreach (var response in query.Responses)
                    {
                        if (!string.IsNullOrWhiteSpace(response.FileName) && string.IsNullOrWhiteSpace(response.MediaStream))
                        {
                            var errorDetails = _errorRepository.GetError((int)Enums.Error.MediaNotFound);
                            errorDetails.Message += $" for {response.FileName}";
                            throw new VidyaVahiniException(errorDetails);
                        }
                    }
                }
                return queryModels;
            }
            else
            {
                var queryModels = _queryRepository.GetQueriesbyQID(userId, lessonSectionId, QuestionId);
                foreach (var query in queryModels)
                {
                    foreach (var response in query.Responses)
                    {
                        if (!string.IsNullOrWhiteSpace(response.FileName) && string.IsNullOrWhiteSpace(response.MediaStream))
                        {
                            var errorDetails = _errorRepository.GetError((int)Enums.Error.MediaNotFound);
                            errorDetails.Message += $" for {response.FileName}";
                            throw new VidyaVahiniException(errorDetails);
                        }
                    }
                }
                return queryModels;
            }
        }
        public IEnumerable<QueryModel> GetAllQueries(string userId, string lessonsetId)
        {
            var queryModels = _queryRepository.GetAllQueries(userId, lessonsetId);
            foreach (var query in queryModels)
            {
                foreach (var response in query.Responses)
                {
                    if (!string.IsNullOrWhiteSpace(response.FileName) && string.IsNullOrWhiteSpace(response.MediaStream))
                    {
                        var errorDetails = _errorRepository.GetError((int)Enums.Error.MediaNotFound);
                        errorDetails.Message += $" for {response.FileName}";
                        throw new VidyaVahiniException(errorDetails);
                    }
                }
            }
            return queryModels;


        }

        public bool CreateQuery(CreateQueryCommand createQueryCommand)
        {
            return _queryRepository.CreateQuery(createQueryCommand.UserId, createQueryCommand.TeacherId, createQueryCommand.QuestionId, createQueryCommand.subQuestionId,
                createQueryCommand.QueryText, createQueryCommand.MediaTypeId, createQueryCommand.MediaBase64String, createQueryCommand.MediaFileName, createQueryCommand.LessonSetId);
        }

        public bool EditQuery(EditQueryCommand editQueryCommand)
        { 
            return _queryRepository.EditQuery(editQueryCommand.UserId, editQueryCommand.QueryText, editQueryCommand.MediaTypeId,
                editQueryCommand.MediaBase64String, editQueryCommand.MediaFileName, editQueryCommand.LessonSetId, editQueryCommand.QueryDataId);
        }

        public IEnumerable<QueryModel> GetQueriesByQueryIds(IEnumerable<string> queryIds)
        {
            return _queryRepository.GetQueriesByQueryIds(queryIds);
        }

        public bool DeleteQuery(string queryId)
        {
            return _queryRepository.DeleteQuery(queryId);
        }
    }
}
