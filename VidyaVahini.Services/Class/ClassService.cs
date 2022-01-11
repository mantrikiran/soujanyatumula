using System.Collections.Generic;
using VidyaVahini.Entities.Class;
using VidyaVahini.Infrastructure.Logger;
using VidyaVahini.Repository.Class;

namespace VidyaVahini.Services.Class
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ILogger _logger;

        public ClassService(ILogger logger, IClassRepository classRepository)
        {
            _logger = logger;
            _classRepository = classRepository;
        }

        public IEnumerable<ClassData> GetClassData()
            => _classRepository.GetClassData();

        public ClassModel GetClasses()
            => new ClassModel
            {
                Classes = GetClassData()
            };
    }
}
