using System.Collections.Generic;
using VidyaVahini.Entities.Qualification;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class QualificationService : IQualificationService
    {
        private readonly IQualificationRepository _qualificationRepository;
        private readonly ILogger _logger;

        public QualificationService(ILogger logger, IQualificationRepository qualificationRepository)
        {
            _logger = logger;
            _qualificationRepository = qualificationRepository;
        }

        public IEnumerable<QualificationData> GetQualificationData()
            => _qualificationRepository.GetQualificationData();

        public QualificationModel GetQualifications()
            => new QualificationModel
            {
                Qualifications = GetQualificationData()
            };
    }
}
