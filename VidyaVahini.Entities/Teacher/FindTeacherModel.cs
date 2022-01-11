using VidyaVahini.Entities.School;

namespace VidyaVahini.Entities.Teacher
{
    public class FindTeacherModel
    {
        public BaseSchoolData School { get; set; }

        public BaseTeacherData Teacher { get; set; }

        public bool MaxResendActivationEmailLimitReached { get; set; }

        public bool RegistrationDataAvailable { get; set; }
    }
}
