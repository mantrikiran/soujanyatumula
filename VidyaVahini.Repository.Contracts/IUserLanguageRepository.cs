using System.Collections.Generic;

namespace VidyaVahini.Repository.Contracts
{
    public interface IUserLanguageRepository
    {
        IEnumerable<int> GetUserLanguages(string userId);
        void UpdateUserLanguages(string userId, IEnumerable<int> languageIds, bool deleteExisting);
    }
}
