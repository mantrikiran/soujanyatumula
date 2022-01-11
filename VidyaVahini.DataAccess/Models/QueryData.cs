using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class QueryData
    {
        public int QueryDataId { get; set; }
        public string QueryId { get; set; }
        public string UserId { get; set; }
        public string QueryText { get; set; }
        public string MediaId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Media Media { get; set; }
        public virtual Query Query { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
