using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.School;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccessRepository<School> _school;

        public SchoolRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _school = _unitOfWork.Repository<School>();
        }

        public SchoolData GetSchoolData(int schoolId)
        {
            var school = _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.School>()
                .Find(x => x.SchoolId == schoolId);

            return school == null ? null : new SchoolData
            {
                AddressLine1 = school.AddressLine1,
                AddressLine2 = school.AddressLine2,
                Area = school.Area,
                City = school.City,
                Code = school.SchoolCode,
                ContactNumber = school.ContactNumber,
                Email = school.Email,
                Id = school.SchoolId,
                Name = school.SchoolName,
                StateId = school.StateId
            };
        }

        public int AddSchools(IEnumerable<SchoolData> schoolData)
        {
            var dbSchools = _school.GetAll();

            var newSchoolData = schoolData.Where(p => !dbSchools.Select(q => q.SchoolCode).Contains(p.Code));

            foreach (SchoolData school in newSchoolData)
            {
                _school.Add(new School
                {
                    AddressLine1 = school.AddressLine1,
                    AddressLine2 = school.AddressLine2,
                    Area = school.Area,
                    City = school.City,
                    SchoolCode = school.Code,
                    ContactNumber = school.ContactNumber,
                    Email = school.Email,
                    SchoolName = school.Name,
                    StateId = school.StateId,
                });
            }

           return _unitOfWork.Commit();
        }
    }
}
