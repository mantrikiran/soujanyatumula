using System;
using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class UserLanguageRepository : IUserLanguageRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserLanguageRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<int> GetUserLanguages(string userId)
            => _unitOfWork.Repository<VidyaVahini.DataAccess.Models.UserLanguage>().FindAll(x => x.UserId == userId).Select(x => x.LanguageId);

        public void UpdateUserLanguages(string userId, IEnumerable<int> languageIds, bool deleteExisting)
        {
            if (languageIds == null || !languageIds.Any())
                return;

            if (deleteExisting)
            {
                var existingUserLanguages = _unitOfWork.Repository<VidyaVahini.DataAccess.Models.UserLanguage>().FindAll(x => x.UserId == userId);
                if (existingUserLanguages != null && existingUserLanguages.Any())
                    _unitOfWork.Repository<VidyaVahini.DataAccess.Models.UserLanguage>().Delete(existingUserLanguages);
            }

            var languages = languageIds.Select(x => new VidyaVahini.DataAccess.Models.UserLanguage
            {
                UserId = userId,
                LanguageId = x,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            _unitOfWork.Repository<VidyaVahini.DataAccess.Models.UserLanguage>().Add(languages);
        }
    }
}
