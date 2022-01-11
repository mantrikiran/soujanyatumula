using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Query
    {
        public Query()
        {
            QueryDatas = new HashSet<QueryData>();
        }

        public string QueryId { get; set; }
        public string QuestionId { get; set; }
        public string TeacherId { get; set; }
        public int QueryStatus { get; set; }
        public int SubQuestionId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
       
        public virtual Question Question { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<QueryData> QueryDatas { get; set; }
    }
}
