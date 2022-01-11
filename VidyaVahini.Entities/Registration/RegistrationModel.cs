using System.Collections.Generic;
using VidyaVahini.Entities.Class;
using VidyaVahini.Entities.Country;
using VidyaVahini.Entities.Gender;
using VidyaVahini.Entities.Language;
using VidyaVahini.Entities.Qualification;
using VidyaVahini.Entities.State;
using VidyaVahini.Entities.Subject;

namespace VidyaVahini.Entities.Registration
{
    public class RegistrationModel
    {
        public IEnumerable<ClassData> Classes { get; set; }
        public IEnumerable<GenderData> Genders { get; set; }
        public IEnumerable<LanguageData> Languages { get; set; }
        public IEnumerable<QualificationData> Qualifications { get; set; }
        public IEnumerable<StateData> States { get; set; }
        public IEnumerable<CountryData> Countries { get; set; }
        public IEnumerable<SubjectData> Subjects { get; set; }
    }
}
