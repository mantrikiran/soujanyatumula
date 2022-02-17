using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using VidyaVahini.Core.Constant;
using VidyaVahini.Entities.Query;
using VidyaVahini.Entities.Response;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class QueryController : BaseController
    {
        private readonly IQueryService _queryService;

        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet("{userId}/section/{lessonSectionId}/QuestionId/{QuestionId}")]
        public Response<IEnumerable<QueryModel>> GetQueries(string userId, string lessonSectionId, string QuestionId)
           => GetResponse(_queryService
               .GetQueries(userId, lessonSectionId,QuestionId));

        [HttpGet("{userId}/LessonSetId/{lessonsetid}")]
        public Response<IEnumerable<QueryModel>> GetAllQueries(string userId, string lessonsetId)
           => GetResponse(_queryService
               .GetAllQueries(userId, lessonsetId));

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> CreateQuery([FromBody] CreateQueryCommand createQueryCommand)
        {
            if (string.IsNullOrWhiteSpace(createQueryCommand.QueryText) && (string.IsNullOrWhiteSpace(createQueryCommand.MediaBase64String)
                || string.IsNullOrWhiteSpace(createQueryCommand.MediaFileName)))
            {
                throw new ArgumentException(Constants.InvalidArgument);
            }

            bool response = _queryService.CreateQuery(createQueryCommand);
            return GetResponse(new SuccessModel
            {
                Success = response
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> EditQuery([FromBody] EditQueryCommand editQueryCommand)
        {
            if (string.IsNullOrWhiteSpace(editQueryCommand.QueryText) && (string.IsNullOrWhiteSpace(editQueryCommand.MediaBase64String)
                || string.IsNullOrWhiteSpace(editQueryCommand.MediaFileName)))
            {
                throw new ArgumentException(Constants.InvalidArgument);
            }

            bool response = _queryService.EditQuery(editQueryCommand);
            return GetResponse(new SuccessModel
            {
                Success = response
            });
        }

       
        [HttpPost("QueryId")]
        public Response<IEnumerable<QueryModel>> GetQueriesByQueryIds([FromBody] IEnumerable<string> queryId)
          => GetResponse(_queryService
              .GetQueriesByQueryIds(queryId));

        [HttpDelete("{queryId}")]
        public Response<SuccessModel> DeleteQuery(string queryId)
        {
            return GetResponse(new SuccessModel
            {
                Success = _queryService.DeleteQuery(queryId)
            });
        }
    }
}