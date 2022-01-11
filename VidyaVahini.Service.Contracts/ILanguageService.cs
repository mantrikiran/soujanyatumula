using System.Collections.Generic;
using VidyaVahini.Entities.Language;

namespace VidyaVahini.Service.Contracts
{
    public interface ILanguageService
    {
        /// <summary>
        /// Gets all the languages
        /// <param name="includeEnglish">include english language</param>
        /// </summary>
        LanguageModel GetLanguages(bool includeEnglish);

        /// <summary>
        /// Gets all the languages
        /// <param name="includeEnglish">include english language</param>
        /// </summary>
        IEnumerable<LanguageData> GetLanguageData(bool includeEnglish);
    }
}
