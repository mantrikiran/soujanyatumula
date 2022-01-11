using System.Collections.Generic;

namespace VidyaVahini.Entities.Mentor
{
    public class PreferredMentorModel
    {
        public int NumberOfMentorsMatchingGenderLanguageState { get; set; }
        public int NumberOfMentorsMatchingGenderLanguage { get; set; }
        public IEnumerable<PreferredMentor> PreferredMentors { get; set; }
        public IEnumerable<PreferredMentor> OtherMentors { get; set; }
    }

    public class PreferredMentor
    {
        public string MentorId { get; set; }
        public string MentorName { get; set; }
        public string Gender { get; set; }
        public int GenderId { get; set; }
        public IEnumerable<string> Language { get; set; }
        public int LanguageId { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public int CurrentLoad { get; set; }
        public int MaxLoadPreference { get; set; }
    }
}
