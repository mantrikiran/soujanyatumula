using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Qualification;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class QualificationRepository : IQualificationRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public QualificationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<QualificationData> GetQualificationData()
            => _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.Qualification>()
                .GetAll()
                .Select(x => new QualificationData()
                {
                    Id = x.QualificationId,
                    Description = x.QualificationDescription
                });

        public QualificationModel GetQualifications()
            => new QualificationModel()
            {
                Qualifications = GetQualificationData()
            };
    }
}
