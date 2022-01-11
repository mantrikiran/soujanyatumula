using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.DataAccess.Models
{
    public partial class Notifications
    {

        public int Id { get; set; }
        public string msgfrom { get; set; }
        public string msgto { get; set; }
        public int roleid { get; set; }
        public string message { get; set; }
        public string created_date { get; set; }
        public int status { get; set; }


    }
}
