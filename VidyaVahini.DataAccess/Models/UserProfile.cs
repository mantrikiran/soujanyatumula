using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class UserProfile
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int? GenderId { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int? QualificationId { get; set; }
        public string OtherQualification { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public int? countryid { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
