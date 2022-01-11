using System.Collections.Generic;
using VidyaVahini.Entities.Language;
using VidyaVahini.Infrastructure.Logger;
using VidyaVahini.Repository.Language;

namespace VidyaVahini.Services.Language
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

        public IEnumerable<LanguageData> GetLanguageData()
            => _languageRepository.GetLanguageData();

        public LanguageModel GetLanguages()
            => new LanguageModel
            {
                Languages = GetLanguageData()
            };
    }
}
