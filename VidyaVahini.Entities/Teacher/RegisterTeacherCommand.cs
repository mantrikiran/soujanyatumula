using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class RegisterTeacherCommand
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public int GenderId { get; set; }

        public string City { get; set; }

        public int StateId { get; set; }

        public int countryid { get; set; }

        public int QualificationId { get; set; }

        public string OtherQualification { get; set; }

        public IEnumerable<int> Languages { get; set; }

        public IEnumerable<int> Subjects { get; set; }

        public IEnumerable<int> Classes { get; set; }

        public string Password { get; set; }
    }
}
