using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher
{
   public class SectionInstructionModel
    {
        public int SectionTypeId { get; set; }
        public int LanguageId { get; set; }
        public string MediaSource { get; set; }
        public string Role { get; set; }
    }

}
