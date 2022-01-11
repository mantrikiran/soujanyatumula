using System.Collections.Generic;
using VidyaVahini.Entities.Subject;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger _logger;

        public SubjectService(ILogger logger, ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        public IEnumerable<SubjectData> GetSubjectData()
            => _subjectRepository.GetSubjectData();

        public SubjectModel GetSubjects()
            => new SubjectModel
            {
                Subjects = GetSubjectData()
            };
    }
}
