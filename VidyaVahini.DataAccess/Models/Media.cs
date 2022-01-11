using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Media
    {
        public Media()
        {
            InstructionMedias = new HashSet<InstructionMedia>();
            QueryDatas = new HashSet<QueryData>();
            QuestionHints = new HashSet<QuestionHint>();
            QuestionMedias = new HashSet<QuestionMedia>();
            Questions = new HashSet<Question>();
            TeacherResponses = new HashSet<TeacherResponse>();
        }

        public string MediaId { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaSource { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<InstructionMedia> InstructionMedias { get; set; }
        public virtual ICollection<QueryData> QueryDatas { get; set; }
        public virtual ICollection<QuestionHint> QuestionHints { get; set; }
        public virtual ICollection<QuestionMedia> QuestionMedias { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TeacherResponse> TeacherResponses { get; set; }
    }
}
