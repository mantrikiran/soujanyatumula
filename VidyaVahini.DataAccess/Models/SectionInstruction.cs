using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.DataAccess.Models
{
   public partial class SectionInstruction
    {

        public int Id { get; set; }

        public int SectionTypeId { get; set; }

        public int LanguageId { get; set; }

        public string Sectiontext { get; set; }

        public string Role { get; set; }
        public string MediaSource { get; set; }
    }
}
