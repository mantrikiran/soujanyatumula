using System.Collections.Generic;

namespace VidyaVahini.Entities.Mentor
{
    public class MentorCommand
    {
        public string Name { get; set; }

        public int GenderId { get; set; }

        public string ContactNumber { get; set; }

        public string City { get; set; }

        public int StateId { get; set; }
        public int countryid { get; set; }

        public IEnumerable<int> Languages { get; set; }

        public bool WorkingInSssvv { get; set; }

        public string SssvvVolunteer { get; set; }

        public bool WorkingInSaiOrganization { get; set; }

        public string SaiOrganizationVolunteer { get; set; }

        public int QualificationId { get; set; }

        public string OtherQualification { get; set; }

        public string PastExperience { get; set; }

        public string Occupation { get; set; }

        public int TimeCapacity { get; set; }

        public int TeacherCapacity { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
