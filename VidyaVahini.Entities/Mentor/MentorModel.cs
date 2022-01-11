using System.Collections.Generic;

namespace VidyaVahini.Entities.Mentor
{
    public class MentorModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public string Occupation { get; set; }
        public string Qualification { get; set; }
        public string OtherQualification { get; set; }
        public string EnglishTeachingExperience { get; set; }
        public bool WorkingInSssvv { get; set; }
        public string SssvvVolunteer { get; set; }
        public bool WorkingInSaiOrganization { get; set; }
        public string SaiOrganizationVolunteer { get; set; }
        public int TimeCapacity { get; set; }
        public int TeacherCapacity { get; set; }
        public bool IsActive { get; set; }
    }
}
