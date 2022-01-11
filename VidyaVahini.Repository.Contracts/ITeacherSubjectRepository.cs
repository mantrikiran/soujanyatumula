using System.Collections.Generic;

namespace VidyaVahini.Repository.Contracts
{
    public interface ITeacherSubjectRepository
    {
        /// <summary>
        /// Updates the subjects assigned to the teacher
        /// </summary>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="subjectIds">Subject Ids</param>
        /// <param name="deleteExisting">Delete existing subjects assigned to the teacher</param>
        void UpdateTeacherSubjects(string teacherId, IEnumerable<int> subjectIds, bool deleteExisting);
    }
}
