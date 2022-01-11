using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Class;
using System.Collections.Generic;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ClassData> GetClassData()
            => _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.Class>()
                .GetAll()
                .Select(x => new ClassData()
                {
                    Id = x.ClassId,
                    Code = x.ClassCode,
                    Description = x.ClassDescription
                });
    }
}
