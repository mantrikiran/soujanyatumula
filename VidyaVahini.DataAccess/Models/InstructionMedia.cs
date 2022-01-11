using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class InstructionMedia
    {
        public int InstructionMediaId { get; set; }
        public int InstructionId { get; set; }
        public int LanguageId { get; set; }
        public string MediaId { get; set; }
        public string MediaDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Instruction Instruction { get; set; }
        public virtual Language Language { get; set; }
        public virtual Media Media { get; set; }
    }
}
