using System.Collections.Generic;
using VidyaVahini.Entities.Qualification;
using VidyaVahini.Infrastructure.Logger;
using VidyaVahini.Repository.Qualification;

namespace VidyaVahini.Services.Qualification
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
