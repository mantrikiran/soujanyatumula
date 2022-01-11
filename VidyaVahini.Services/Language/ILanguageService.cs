using System.Collections.Generic;
using VidyaVahini.Entities.Language;

namespace VidyaVahini.Services.Language
{
    public interface ILanguageService
    {
        LanguageModel GetLanguages();

        IEnumerable<LanguageData> GetLanguageData();
    }
}