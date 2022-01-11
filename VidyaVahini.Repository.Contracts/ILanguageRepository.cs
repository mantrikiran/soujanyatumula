using System.Collections.Generic;
using VidyaVahini.Entities.Language;

namespace VidyaVahini.Repository.Contracts
{
    public interface ILanguageRepository
    {
        /// <summary>
        /// Gets all the languages from the database
        /// </summary>
        /// <param name="includeEnglish">include english language</param>
        /// <returns>languages</returns>
        IEnumerable<LanguageData> GetLanguageData(bool includeEnglish);
    }
}
