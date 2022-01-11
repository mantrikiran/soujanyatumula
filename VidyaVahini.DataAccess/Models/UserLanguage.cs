using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class UserLanguage
    {
        public int UserLanguageId { get; set; }
        public string UserId { get; set; }
        public int LanguageId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Language Language { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
