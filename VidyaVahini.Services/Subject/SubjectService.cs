using System.Collections.Generic;
using VidyaVahini.Entities.Subject;
using VidyaVahini.Infrastructure.Logger;
using VidyaVahini.Repository.Subject;

namespace VidyaVahini.Services.Subject
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
