using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Instruction
    {
        public Instruction()
        {
            InstructionMedias = new HashSet<InstructionMedia>();
        }

        public int InstructionId { get; set; }
        public string LessonSectionId { get; set; }
        public string InstructionDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual LessonSection LessonSection { get; set; }
        public virtual ICollection<InstructionMedia> InstructionMedias { get; set; }
    }
}
