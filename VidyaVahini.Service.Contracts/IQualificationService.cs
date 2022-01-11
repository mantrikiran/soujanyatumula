using System.Collections.Generic;
using VidyaVahini.Entities.Qualification;

namespace VidyaVahini.Service.Contracts
{
    public interface IQualificationService
    {
        /// <summary>
        /// Gets the qualification data
        /// </summary>
        QualificationModel GetQualifications();

        /// <summary>
        /// Gets the qualification data
        /// </summary>
        IEnumerable<QualificationData> GetQualificationData();
    }
}
