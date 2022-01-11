using System.Collections.Generic;
using VidyaVahini.Entities.Class;

namespace VidyaVahini.Repository.Contracts
{
    public interface IClassRepository
    {
        /// <summary>
        /// Gets all the classes from the database
        /// </summary>
        IEnumerable<ClassData> GetClassData();
    }
}
