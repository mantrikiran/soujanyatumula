using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Media = new HashSet<Media>();
        }

        public int MediaTypeId { get; set; }
        public string MediaTypeDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<Media> Media { get; set; }
    }
}
