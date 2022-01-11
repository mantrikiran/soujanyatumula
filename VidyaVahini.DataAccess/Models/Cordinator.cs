using System;
using System.Collections.Generic;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Cordinator
    {
        public int CordinatorId { get; set; }
        public string UserId { get; set; }
        public bool WorkingInSssvv { get; set; }
        public string SssvvvoluteerName { get; set; }
        public bool WorkingInSaiOrganization { get; set; }
        public string SaiOrganizationVoluteerName { get; set; }
        public string Occupation { get; set; }
        public int TimeCapacity { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual UserAccount User { get; set; }
    }
}
