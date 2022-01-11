using System.Collections.Generic;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Mentor;
using VidyaVahini.Entities.Role;

namespace VidyaVahini.Entities.UserAccount
{
    public class UserModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }  

        public UserDataModel UserData { get; set; }
      
        public IEnumerable<RoleData> UserRoles { get; set; }       
    }
}
