using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class CreateTeacherAccountCommand
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int SchoolId { get; set; }
    }
}
