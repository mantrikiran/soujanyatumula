using System.Collections.Generic;
using VidyaVahini.Entities.Class;

namespace VidyaVahini.Services.Class
{
    public interface IClassService
    {
        ClassModel GetClasses();

        IEnumerable<ClassData> GetClassData();
    }
}