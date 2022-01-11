using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Language
    {
        public Language()
        {
            InstructionMedias = new HashSet<InstructionMedia>();
            QuestionMedias = new HashSet<QuestionMedia>();
            UserLanguages = new HashSet<UserLanguage>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<InstructionMedia> InstructionMedias { get; set; }
        public virtual ICollection<QuestionMedia> QuestionMedias { get; set; }
        public virtual ICollection<UserLanguage> UserLanguages { get; set; }
    }
}
