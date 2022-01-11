using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VidyaVahini.Core.Constant;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.Language;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccessRepository<Language> _language;

        public LanguageRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _language = _unitOfWork.Repository<Language>();
        }

        public IEnumerable<LanguageData> GetLanguageData(bool includeEnglish)
        {
            var languages = _language
                .GetAll()
                .Select(x => new LanguageData()
                {
                    Id = x.LanguageId,
                    Name = x.LanguageName
                });

            //if(!includeEnglish)
            //{
            //    languages = languages.Where(x => x.Id != Constants.EnglishLanguage);
            //}

            return languages;
        }
    }

}
