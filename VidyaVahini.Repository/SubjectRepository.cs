using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Subject;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SubjectData> GetSubjectData()
            => _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.Subject>()
                .GetAll()
                .Select(x => new SubjectData()
                {
                    Id = x.SubjectId,
                    Code = x.SubjectCode,
                    Name = x.SubjectName
                });
    }
}
