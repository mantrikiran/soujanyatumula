using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Sectiontext
{
   public class SectionInstructionModel
    {

        public int Id { get; set; }

        public int SectionTypeId { get; set; }

        public int LangauageId { get; set; }

        public string Sectiontext { get; set; }
    }
}
