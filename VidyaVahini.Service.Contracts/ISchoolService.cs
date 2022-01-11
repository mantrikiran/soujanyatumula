using System.IO;
using VidyaVahini.Entities.Response;

namespace VidyaVahini.Service.Contracts
{
    public interface ISchoolService
    {
        /// <summary>
        /// Adds the school details
        /// </summary>
        /// <param name="schoolData">Id</param>
        /// <returns>Number of schools added</returns>
        SchoolDataUploadModel AddSchools(Stream fileStream);
    }
}
