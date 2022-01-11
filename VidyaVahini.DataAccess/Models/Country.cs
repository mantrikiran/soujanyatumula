using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Country
    {

        public Country()
        {
            Schools = new HashSet<School>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int countryid { get; set; }
        public int phone_code { get; set; }
        public string country_code { get; set; }

        public string country_name { get; set; }


        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
