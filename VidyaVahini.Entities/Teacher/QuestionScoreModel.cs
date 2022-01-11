namespace VidyaVahini.Entities.Teacher
{
    public class QuestionScoreModel
    {
        public int Score { get; set; }
        public int TotalQuestion { get; set; }
        public int Attempts { get; set; }
        public int RecommendedAttempts { get; set; }
        public int SecondaryAttempts { get; set; }
        public bool PerfectScore { get; set; }
    }
}
