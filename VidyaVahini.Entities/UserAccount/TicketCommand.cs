using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.UserAccount
{
    public class TicketCommand
    {
        public string Description { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> file { get; set; }
    }
}
