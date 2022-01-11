using System;
using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class TeacherSubjectRepository : ITeacherSubjectRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherSubjectRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateTeacherSubjects(string teacherId, IEnumerable<int> subjectIds, bool deleteExisting)
        {
            if (subjectIds == null || !subjectIds.Any())
                return;

            if (deleteExisting)
            {
                var existingTeacherSubjects = _unitOfWork.Repository<VidyaVahini.DataAccess.Models.TeacherSubject>().FindAll(x => x.TeacherId == teacherId);
                if (existingTeacherSubjects != null && existingTeacherSubjects.Any())
                    _unitOfWork.Repository<VidyaVahini.DataAccess.Models.TeacherSubject>().Delete(existingTeacherSubjects);
            }

            var subjects = subjectIds.Select(x => new VidyaVahini.DataAccess.Models.TeacherSubject
            {
                TeacherId = teacherId,
                SubjectId = x,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            _unitOfWork.Repository<VidyaVahini.DataAccess.Models.TeacherSubject>().Add(subjects);
        }
    }
}
