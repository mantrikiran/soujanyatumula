using System.Collections.Generic;
using VidyaVahini.Entities.Query;

namespace VidyaVahini.Repository.Contracts
{
    public interface IQueryRepository
    {
        /// <summary>
        /// Get queries based on user and lesson section
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="lessonSectionId">Lesson Section Id</param>
        /// <returns>List of queries based on user and lesson section</returns>
        IEnumerable<QueryModel> GetQueries(string userId, string lessonSectionId);
        IEnumerable<QueryModel> GetAllQueries(string userId, string lessonsetId);
        IEnumerable<QueryModel> GetQueriesbyQID(string userId, string lessonSectionId, string QuestionId);

        /// <summary>
        /// Creates Query 
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="questionId">Question Id</param>
        /// <param name="queryText">Query Text</param>
        /// <param name="mediaTypeId">MediaType Id</param>
        /// <param name="mediaBase64String">Media Stream</param>
        /// <param name="mediaFileName">Media File Name</param>
        /// <param name="lessonSetId">Lesson Set Id</param>
        /// <returns>If query is created</returns>
        bool CreateQuery(string userId, string teacherId, string questionId,int subQuestionId, string queryText, 
            int mediaTypeId, string mediaBase64String, string mediaFileName, string lessonSetId);

        /// <summary>
        /// Edit Query 
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="QueryText">Query Text</param>
        /// <param name="mediaTypeId">MediaType Id</param>
        /// <param name="mediaBase64String">Media Stream</param>
        /// <param name="mediaFileName">Media File Name</param>
        /// <param name="lessonSetId">Lesson Set Id</param>
        /// <param name="queryDataId">Query Data Id</param>
        /// <returns>If query is edited</returns>
        public bool EditQuery(string userId, string queryText, int mediaTypeId, string mediaBase64String,
            string mediaFileName, string lessonSetId, int queryDataId);

        /// <summary>
        /// Get Queries by Query Ids 
        /// </summary>
        /// <param name="queryIds">Query Ids </param>
        /// <returns>List of queries based on query ids</returns>
        public IEnumerable<QueryModel> GetQueriesByQueryIds(IEnumerable<string> queryIds);

        /// <summary>
        /// Delete Query by Query Id
        /// </summary>
        /// <param name="queryId">Query Ids </param>
        /// <returns>If query is deleted</returns>
        public bool DeleteQuery(string queryId);
    }
}
