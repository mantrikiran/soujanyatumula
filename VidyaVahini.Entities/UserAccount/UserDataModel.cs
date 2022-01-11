using System.Collections.Generic;
using VidyaVahini.Entities.Language;

namespace VidyaVahini.Entities.UserAccount
{
    public class UserDataModel
    {
        public string ActiveLessonSetId { get; set; }
        public IEnumerable<LanguageData> UserLanguages { get; set; }
    }
}
