using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher.Dashboard
{
    public class NotificationsModel
    {       
        public int Id { get; set; } 
        public string from { get; set; }
        public string to { get; set; }
        public int roleid { get; set; }
        public string message { get; set; }
        public string created_date { get; set; }
        public int status { get; set; }
    }
}
