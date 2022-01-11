using System;
using System.Collections.Generic;

namespace VidyaVahini.Entities.Teacher
{
    public class TeacherQuestionResponse
    {
       
        public string QuestionId { get; set; }
        public string UserResponseState { get; set; }
        public List<TeacherResponseMedia> TeacherResponses { get; set; }
    }

    public class TeacherResponseMedia
    {
        public int TeacherResponseId { get; set; }
        public string ResponseText { get; set; }
        public string MediaSource { get; set; }
        public int MediaTypeId { get; set; }

        public int RecommendedAttempts { get; set; }
        public int SecondaryAttempts { get; set; }
        public int Attempts { get; set; }
        public int Score { get; set; }
        public int MaximumScore { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<MenterQuestionResponse> MenterQuestionResponses { get; set; }
        public List<TeacherSubQuestionModel> SubQuestionResponses { get; set; }
    }

    public class MenterQuestionResponse
    {
        public string MentorResponseId { get; set; }
        public string MentorComments { get; set; }
        public string MentorId { get; set; }
        public string MentorName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class TeacherSubQuestionModel
    {
        public int QuestionTypeId { get; set; }
        public int SubQuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<TeacherSubQuestionAnswerModel> SubQuestionAnswers { get; set; }
     

    }

    public class TeacherSubQuestionAnswerModel
    {       
        public int SubQuestionOptionId { get; set; }
        public int SubQuesId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public string CorrectAnswer { get; set;}
        public int MaximumScore1 { get; set; }
        public int QtMS { get; set; }
        


    }
}
