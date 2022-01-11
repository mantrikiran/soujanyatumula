using System.Collections.Generic;
using VidyaVahini.Entities.Subject;

namespace VidyaVahini.Services.Subject
{
    public interface ISubjectService
    {
        SubjectModel GetSubjects();

        IEnumerable<SubjectData> GetSubjectData();
    }
}