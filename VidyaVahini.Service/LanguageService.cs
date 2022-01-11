using System.Collections.Generic;
using VidyaVahini.Entities.Language;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger _logger;

        public LanguageService(ILogger logger, ILanguageRepository languageRepository)
        {
            _logger = logger;
            _languageRepository = languageRepository;
        }

        public IEnumerable<LanguageData> GetLanguageData(bool includeEnglish)
            => _languageRepository.GetLanguageData(includeEnglish);

        public LanguageModel GetLanguages(bool includeEnglish)
            => new LanguageModel
            {
                Languages = GetLanguageData(includeEnglish)
            };
    }
}
