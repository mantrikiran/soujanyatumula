using System.Collections.Generic;
using System.IO;

namespace VidyaVahini.Entities.Notification
{
    public class Email
    {
        public IEnumerable<string> To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Template { get; set; }
        public Dictionary<string, string> Replacements { get; set; }        
        public List<string> ImagePath { get; set; }
    }
}
