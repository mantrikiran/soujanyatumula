using System;
using System.Collections.Generic;
using System.Text;

namespace VidyaVahini.Entities.UserAccount
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
