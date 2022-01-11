using System.ComponentModel;
using System.Runtime.Serialization;

namespace VidyaVahini.Core.Enum
{
    public static class Enums
    {
        public enum Error
        {
            GeneralErrorMessage = 1,
            TeacherNotFound,
            TokenInvalidOrExpired,
            InvalidCredentials,
            EmailAlreadyRegistered,
            UserAccountNotFound,
            ErrorSendingNotification,
            QuestionNotFound,
            SectionNotFound,
            MediaNotFound,
            NoMentorAvailable,
            NoTeacherMissingMentor,
            InvalidTeacherOrMentor,
            NoLessonsAvailable,
            QuestionOrUserResponseNotFound,
            InvalidState,
            MentorNotFoundOrHasNoMentees,
            InvalidLessonSet,
            UnableToFetchLessonSetsOrIsLastLessonSet,
            Lessonsetisalreadyinuse,
            Notaregistereduser,
            WithSameContactNumberAlreadyRegistered
        }

        public enum Role
        {
            Administrator = 1,
            CoreTeam,
            SeniorMentor,
            Mentor,
            Teacher,
            NA
        }

        public enum Level
        {
            BASIC = 1,
            INTERMEDIATE,
            EXPERT
        }

        public enum LessonStatus
        {
            [Description("To be done")]
            TO_BE_DONE = 1,
            [Description("Ongoing")]
            ONGOING,
            [Description("Submitted for review")]
            SUBMITTED_FOR_REVIEW,
            [Description("Completed and reviewed")]
            COMPLETED_AND_REVIEWED
        }

        public enum LessonSectionStatus
        {
            [Description("To be done")]
            TO_BE_DONE = 1,
            [Description("Complete and submit")]
            COMPLETE_AND_SUBMIT,
            [Description("Submitted for Review")]
            SUBMITTED_FOR_REVIEW,
            [Description("Redo submission")]
            REDO_SUBMISSION,
            [Description("Completed and Approved")]
            COMPLETED_AND_APPROVED
        }

        public enum ResponseStatus
        {
            [Description("To be done")]
            TO_BE_DONE,
            [Description("Complete and submit")]
            COMPLETE_AND_SUBMIT,
            [Description("Submitted for Review")]
            SUBMITTED_FOR_REVIEW,
            [Description("Redo Submission")]
            REDO_SUBMISSION,
            [Description("Completed and Approved")]
            COMPLETED_AND_APPROVED
        }

        public enum QueryStatus
        {
            [Description("Pending Mentor's response on the query")]
            PENDING_WITH_MENTOR = 1,
            [Description("Pending Teacher's response on the query")]
            PENDING_WITH_TEACHER
        }
    }
}
