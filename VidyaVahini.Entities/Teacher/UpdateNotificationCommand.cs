using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.Teacher
{
    public class UpdateNotificationCommand
    {
        public string  userId { get; set; }
        public int status { get; set; }
    }
}
