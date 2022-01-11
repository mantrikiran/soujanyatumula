using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class School
    {
        public School()
        {
            Teachers = new HashSet<Teacher>();
        }

        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public int StateId { get; set; }
        public int countryid { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
