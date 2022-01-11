using System;
using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class TeacherClassRepository : ITeacherClassRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherClassRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateTeacherClasses(string teacherId, IEnumerable<int> classIds, bool deleteExisting)
        {
            if (classIds == null || !classIds.Any())
                return;

            if (deleteExisting)
            {
                var existingTeacherClasses = _unitOfWork.Repository<VidyaVahini.DataAccess.Models.TeacherClass>().FindAll(x => x.TeacherId == teacherId);
                if (existingTeacherClasses != null && existingTeacherClasses.Any())
                    _unitOfWork.Repository<VidyaVahini.DataAccess.Models.TeacherClass>().Delete(existingTeacherClasses);
            }

            var classes = classIds.Select(x => new VidyaVahini.DataAccess.Models.TeacherClass
            {
                TeacherId = teacherId,
                ClassId = x,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            _unitOfWork.Repository<VidyaVahini.DataAccess.Models.TeacherClass>().Add(classes);
        }
    }
}
