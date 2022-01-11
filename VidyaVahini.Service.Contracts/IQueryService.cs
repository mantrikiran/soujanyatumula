using System.Collections.Generic;
using VidyaVahini.Entities.Query;

namespace VidyaVahini.Service.Contracts
{
    public interface IQueryService
    {
        /// <summary>
        /// Creates a Query
        /// <param name="createQueryCommand">Query details</param>
        /// </summary>
        bool CreateQuery(CreateQueryCommand createQueryCommand);

        /// <summary>
        /// Gets the queries based on user id
        /// <param name="userId">User Id</param>
        /// <param name="lessonSectionId">Lesson Section Id</param>
        /// </summary>
        IEnumerable<QueryModel> GetQueries(string userId, string lessonSectionId,string QuestionId);
        IEnumerable<QueryModel> GetAllQueries(string userId, string lessonsetId);

        /// <summary>
        /// Edit query based on query id
        /// <param name="editQueryCommand">Query details</param>
        /// </summary>
        bool EditQuery(EditQueryCommand editQueryCommand);

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
