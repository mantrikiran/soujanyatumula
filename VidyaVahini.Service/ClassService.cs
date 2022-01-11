using System.Collections.Generic;
using VidyaVahini.Entities.Class;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
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
