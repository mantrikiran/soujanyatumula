using System.Collections.Generic;
using VidyaVahini.Entities.Qualification;

namespace VidyaVahini.Repository.Contracts
{
    public interface IQualificationRepository
    {
        /// <summary>
        /// Gets all the qualification data from the database
        /// </summary>
        IEnumerable<QualificationData> GetQualificationData();
    }
}
