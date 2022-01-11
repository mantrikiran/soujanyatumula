using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Gender;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class GenderRepository : IGenderRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenderRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GenderData> GetGenderData()
            => _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.Gender>()
                .GetAll()
                .Select(x => new GenderData()
                {
                    Id = x.GenderId,
                    Code = x.GenderCode,
                    Description = x.GenderDescription
                });
    }
}
