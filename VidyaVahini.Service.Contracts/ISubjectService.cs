using System.Collections.Generic;
using VidyaVahini.Entities.Subject;

namespace VidyaVahini.Service.Contracts
{
    public interface ISubjectService
    {
        /// <summary>
        /// Gets the subjects
        /// </summary>
        SubjectModel GetSubjects();

        /// <summary>
        /// Gets the subjects
        /// </summary>
        IEnumerable<SubjectData> GetSubjectData();
    }
}
