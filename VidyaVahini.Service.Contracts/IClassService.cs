using System.Collections.Generic;
using VidyaVahini.Entities.Class;

namespace VidyaVahini.Service.Contracts
{
    public interface IClassService
    {
        /// <summary>
        /// Gets all the classes
        /// </summary>
        ClassModel GetClasses();

        /// <summary>
        /// Gets all the classes
        /// </summary>
        IEnumerable<ClassData> GetClassData();
    }
}
