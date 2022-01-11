namespace VidyaVahini.Core.Constant
{
    public static class Constants
    {
        #region Application Constants
        public const bool EnableLanguageFallback = true;
        public const int FallbackLanguage = 1;
        public const int EnglishLanguage = 1;
        #endregion

        #region Authentication Constants
        //In minutes
        public const int AuthTokenExpiry = 120;
        public const string NameClaim = "name";
        public const string EmailClaim = "email";
        public const string UserDataClaim = "userData";
        public const string RoleIdClaim = "roleId";
        #endregion

        #region Notification Replacements
        public const string HostReplacement = "[[host]]";
        public const string AppHostReplacement = "[[apphost]]";
        public const string ApiVersionReplacement = "[[version]]";
        public const string EmailReplacement = "[[email]]";
        public const string TokenReplacement = "[[token]]";
        public const string TokenExpiryReplacement = "[[tokenexpiry]]";
        public const string NameReplacement = "[[name]]";
        public const string Description = "[[Description]]";
        public const string Date = "[[date]]";
        #endregion

        #region Notification Templates
        public const string EmailTemplatesPathPrefix = @"Assets/EmailTemplates/";
        public const string AccountActivationEmailTemplate = EmailTemplatesPathPrefix + "AccountActivationEmail.html";
        public const string NewMentorRegistrationEmailTemplate = EmailTemplatesPathPrefix + "NewMentorRegistrationEmail.html";
        public const string ForgotPasswordEmailTemplate = EmailTemplatesPathPrefix + "ForgotPasswordEmail.html";
        public const string MentorAccountActivationEmailTemplate = EmailTemplatesPathPrefix + "MentorAccountActivationEmail.html";
        public const string TicketEmailTemplate = EmailTemplatesPathPrefix + "TicketEmail.html";
        public const string SupportEmailTemplate = EmailTemplatesPathPrefix + "SupportMail.html";
        public const string UserEmailTemplate = EmailTemplatesPathPrefix + "UserMail.html";
        #endregion

        #region Notification Email Subjects
        public const string EmailSubjectPrefix = "Vidya Vahini | ";
        public const string AccountActivationEmailSubject = EmailSubjectPrefix + "Account Verification";
        public const string NewMentorRegistrationEmailSubject = EmailSubjectPrefix + "New Mentor Registration";
        public const string ForgotPasswordEmailSubject = EmailSubjectPrefix + "Reset Password";
        public const string MentorAccountActivationEmailSubject = EmailSubjectPrefix + "Registration Approved";
        public const string TicketEmailSubject = EmailSubjectPrefix + "Ticket Email";
        public const string SupportEmailSubject = EmailSubjectPrefix + "Support Query Received";
        public const string UserEmailSubject = EmailSubjectPrefix + "Support Query Submitted";
        #endregion

        #region User Account
        public const int TokenExpiry = 7;
        public const int MaxResendActivationEmailLimit = 3;
        #endregion

        #region Include Properties
        public const string UserAccountProperty = "User";
        public const string UserProfileProperty = "UserProfile";
        public const string UserLanguagesProperty = "UserLanguages";
        public const string UserRolesProperty = "UserRoles";
        public const string GenderProperty = "Gender";
        public const string QualificationProperty = "Qualification";
        public const string StateProperty = "State";
        public const string CountryProperty = "Country";
        public const string LanguageProperty = "Language";
        public const string SchoolProperty = "School";
        public const string TeacherProperty = "Teacher";
        public const string TeachersProperty = "Teachers";
        public const string TeacherClassProperty = "TeacherClasses";
        public const string TeacherSubjectProperty = "TeacherSubjects";
        public const string RoleProperty = "Role";
        public const string TeacherResponsesProperty = "TeacherResponses";
        public const string QuestionProperty = "Question";
        public const string QuestionsProperty = "Questions";
        public const string LessonSectionProperty = "LessonSection";
        public const string LessonSectionsProperty = "LessonSections";
        public const string LessonProperty = "Lesson";
        public const string LessonsProperty = "Lessons";
        public const string LevelProperty = "Level";
        public const string InstructionProperty = "Instruction";
        public const string InstructionMediasProperty = "InstructionMedias";
        public const string MediaProperty = "Media";
        public const string QuestionHintsProperty = "QuestionHints";
        public const string QuestionMediasProperty = "QuestionMedias";
        public const string QueriesProperty = "Queries";
        public const string MentorNavigationProperty = "MentorNavigation";
        public const string TeacherNavigationProperty = "TeacherNavigation";
        public const string LessonSetProperty = "LessonSet";
        public const string LessonSetsProperty = "LessonSets";
        public const string SectionTypeProperty = "SectionType";
        public const string SubQuestionsProperty = "SubQuestions";
        public const string SubQuestionOptionProperty = "SubQuestionOption";
        public const string SubQuestionOptionsProperty = "SubQuestionOptions";
        public const string SubQuestionAnswersProperty = "SubQuestionAnswers";
        public const string TeacherResponseStatusProperty = "TeacherResponseStatus";
        public const string MentorResponseProperty = "MentorResponse";
        public const string MentorProperty = "Mentor";
        public const string QueryDatasProperty = "QueryDatas";
        public const string ActiveLessonSetProperty = "ActiveLessonSet";
        public const string TeacherSubQuestionResponsesProperty = "TeacherSubQuestionResponses";
        #endregion

        #region Teacher Dashboard
        public const int NumberOfLessonsInSet = 5;
        public const int AssessmentLessonNumber = -1;
        public const string QuestionResponseFolderName = "QuestionResponse";
        public const int MediaTypeId = 1;
        #endregion

        #region Media Constants
        public const string UserMediaFilePath = "C:\\PYF\\Media\\Users";
        public const string DashboardMediaFilePath = "C:\\PYF\\Media\\Dashboard";
        #endregion

        #region Query Constants
        public const string Queries = "Queries";
        #endregion

        #region Error Constants
        public const string InvalidArgument = "One of the arguments is invalid";
        #endregion

        #region Cache Constants
        public const string LessonSetCache = "LessonSets";
        public const string DashboardCache = "Dashboard_{0}_{1}";
        public const string TeacherDashboardCache = "TeacherDashboard_{0}";
        #endregion

        #region Cache Duration Constants In Minutes
        public const int LessonSetCacheDuration = 720;
        public const int DashboardCacheDuration = 720;
        public const int TeacherDashboardCacheDuration = 120;
        #endregion

        #region Data Entry Constants
        public const string ListenKeenlySectionIdentifier = "LISTEN_KEENLY";
        public const string SpeakWellSectionIdentifier = "SPEAK_WELL";
        public const string ReadAloudSectionIdentifier = "READ_ALOUD";
        public const string WriteRightSectionIdentifier = "WRITE_RIGHT";
        public const string AssessmentIdentifier = "ASSESSMENT";
        #endregion
    }
}
