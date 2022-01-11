using System.Collections.Generic;

namespace VidyaVahini.Repository.Contracts
{
    public interface ITeacherClassRepository
    {
        /// <summary>
        /// Adds classes assigned to the teacher
        /// </summary>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="classIds">Class Ids</param>
        /// <param name="deleteExisting">Delete existing classes assigned to the teacher</param>
        void UpdateTeacherClasses(string teacherId, IEnumerable<int> classIds, bool deleteExisting);


    }
}
