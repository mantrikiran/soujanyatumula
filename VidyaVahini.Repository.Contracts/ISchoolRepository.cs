using System.Collections.Generic;
using VidyaVahini.Entities.School;

namespace VidyaVahini.Repository.Contracts
{
    public interface ISchoolRepository
    {
        /// <summary>
        /// Gets the school details
        /// </summary>
        /// <param name="schoolId">Id</param>
        /// <returns>School details</returns>
        SchoolData GetSchoolData(int schoolId);

        /// <summary>
        /// Adds the school details
        /// </summary>
        /// <param name="schoolData">Id</param>
        /// <returns>Number of schools added</returns>
        int AddSchools(IEnumerable<SchoolData> schoolData);
    }

}
