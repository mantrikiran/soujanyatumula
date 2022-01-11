using System.Collections.Generic;
using VidyaVahini.Entities.Subject;

namespace VidyaVahini.Repository.Contracts
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Gets all the subjects
        /// </summary>
        IEnumerable<SubjectData> GetSubjectData();
    }
}
