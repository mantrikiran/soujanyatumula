using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VidyaVahini.DataAccess.Models
{
    public partial class vidyavahiniContext : DbContext
    {
        public vidyavahiniContext()
        {
        }

        public vidyavahiniContext(DbContextOptions<vidyavahiniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }
        public virtual DbSet<InstructionMedia> InstructionMedias { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<LessonSection> LessonSections { get; set; }
        public virtual DbSet<LessonSet> LessonSets { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Mentor> Mentors { get; set; }
        public virtual DbSet<MentorResponse> MentorResponses { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Query> Queries { get; set; }
        public virtual DbSet<QueryData> QueryDatas { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionHint> QuestionHints { get; set; }
        public virtual DbSet<QuestionMedia> QuestionMedias { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SectionType> SectionTypes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Country> Country { get; set; }

        public virtual DbSet<SectionInstruction> SectionInstruction { get; set; }

        public virtual DbSet<SubQuestion> SubQuestions { get; set; }
        public virtual DbSet<SubQuestionAnswer> SubQuestionAnswers { get; set; }
        public virtual DbSet<SubQuestionOption> SubQuestionOptions { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherClass> TeacherClasses { get; set; }
        public virtual DbSet<TeacherResponse> TeacherResponses { get; set; }
        public virtual DbSet<TeacherResponseStatu> TeacherResponseStatus { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<TeacherSubQuestionResponse> TeacherSubQuestionResponses { get; set; }
        public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserLanguage> UserLanguages { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.HasIndex(e => e.ClassCode)
                    .HasName("Class_UC")
                    .IsUnique();

                entity.Property(e => e.ClassDescription)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Error>(entity =>
            {
                entity.ToTable("Error");

                entity.HasIndex(e => e.ErrorCode)
                    .HasName("Error_UC")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ErrorCode)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ErrorMessage)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.HasIndex(e => e.GenderCode)
                    .HasName("Gender_UC")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.GenderCode)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.GenderDescription)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Instruction>(entity =>
            {
                entity.ToTable("Instruction");

                entity.HasIndex(e => e.LessonSectionId)
                    .HasName("Instruction_UC")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.InstructionDescription)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LessonSectionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.LessonSection)
                    .WithOne(p => p.Instruction)
                    .HasForeignKey<Instruction>(d => d.LessonSectionId)
                    .HasConstraintName("Instruction_FK_LessonSection");
            });

            modelBuilder.Entity<InstructionMedia>(entity =>
            {
                entity.ToTable("InstructionMedia");

                entity.HasIndex(e => e.InstructionId)
                    .HasName("InstructionMedia_FK_Instruction");

                entity.HasIndex(e => e.LanguageId)
                    .HasName("InstructionMedia_FK_Language");

                entity.HasIndex(e => e.MediaId)
                    .HasName("InstructionMedia_FK_Media");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MediaDescription)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MediaId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Instruction)
                    .WithMany(p => p.InstructionMedias)
                    .HasForeignKey(d => d.InstructionId)
                    .HasConstraintName("InstructionMedia_FK_Instruction");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.InstructionMedias)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("InstructionMedia_FK_Language");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.InstructionMedias)
                    .HasForeignKey(d => d.MediaId)
                    .HasConstraintName("InstructionMedia_FK_Media");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.HasIndex(e => e.LessonCode)
                    .HasName("Lesson_UC")
                    .IsUnique();

                entity.HasIndex(e => e.LessonSetId)
                    .HasName("Lesson_FK_LessonSet");

                entity.Property(e => e.LessonId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LessonCode)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LessonDescription)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LessonName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LessonSetId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.LessonSet)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.LessonSetId)
                    .HasConstraintName("Lesson_FK_LessonSet");
            });

            modelBuilder.Entity<LessonSection>(entity =>
            {
                entity.ToTable("LessonSection");

                entity.HasIndex(e => e.LessonId)
                    .HasName("LessonSection_FK_Lesson");

                entity.HasIndex(e => e.SectionTypeId)
                    .HasName("LessonSection_FK_SectionType");

                entity.Property(e => e.LessonSectionId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LessonId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LessonSectionDescription)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LessonSectionName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonSections)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("LessonSection_FK_Lesson");

                entity.HasOne(d => d.SectionType)
                    .WithMany(p => p.LessonSections)
                    .HasForeignKey(d => d.SectionTypeId)
                    .HasConstraintName("LessonSection_FK_SectionType");
            });

            modelBuilder.Entity<LessonSet>(entity =>
            {
                entity.ToTable("LessonSet");

                entity.HasIndex(e => e.LessonSetOrder)
                    .HasName("LessonSet_UC")
                    .IsUnique();

                entity.HasIndex(e => e.LevelId)
                    .HasName("LessonSet_FK_Level");

                entity.Property(e => e.LessonSetId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.LessonSets)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("LessonSet_FK_Level");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("Level");

                entity.HasIndex(e => e.LevelCode)
                    .HasName("Level_UC")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LevelCode)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LevelDescription)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasIndex(e => e.MediaTypeId)
                    .HasName("Media_FK_MediaType");

                entity.Property(e => e.MediaId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MediaSource)
                    .IsRequired()
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.MediaTypeId)
                    .HasConstraintName("Media_FK_MediaType");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("MediaType");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MediaTypeDescription)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Mentor>(entity =>
            {
                entity.ToTable("Mentor");

                entity.Property(e => e.MentorId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EnglishTeachingExperience)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Occupation)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SaiOrganizationVoluteerName)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SssvvvoluteerName)
                    .HasColumnName("SSSVVVoluteerName")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.WorkingInSssvv).HasColumnName("WorkingInSSSVV");

                entity.HasOne(d => d.MentorNavigation)
                    .WithOne(p => p.Mentor)
                    .HasForeignKey<Mentor>(d => d.MentorId)
                    .HasConstraintName("Mentor_FK_UserAccount");
            });

            modelBuilder.Entity<MentorResponse>(entity =>
            {
                entity.ToTable("MentorResponse");

                entity.HasIndex(e => e.MentorId)
                    .HasName("MentorResponse_FK_Mentor");

                entity.HasIndex(e => e.TeacherResponseId)
                    .HasName("MentorResponse_UC")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MentorComments)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MentorId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.MentorResponses)
                    .HasForeignKey(d => d.MentorId)
                    .HasConstraintName("MentorResponse_FK_Mentor");

                entity.HasOne(d => d.TeacherResponse)
                    .WithOne(p => p.MentorResponse)
                    .HasForeignKey<MentorResponse>(d => d.TeacherResponseId)
                    .HasConstraintName("MentorResponse_FK_TeacherResponse");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("Qualification");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.QualificationDescription)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Query>(entity =>
            {
                entity.ToTable("Query");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("Query_FK_Question");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("Query_FK_Teacher");

                entity.Property(e => e.QueryId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Queries)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("Query_FK_Question");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Queries)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("Query_FK_Teacher");
            });

            modelBuilder.Entity<QueryData>(entity =>
            {
                entity.ToTable("QueryData");

                entity.HasIndex(e => e.MediaId)
                    .HasName("QueryData_FK_Media");

                entity.HasIndex(e => e.QueryId)
                    .HasName("QueryData_FK_Query");

                entity.HasIndex(e => e.UserId)
                    .HasName("QueryData_FK_UserAccount");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MediaId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QueryId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QueryText)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.QueryDatas)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("QueryData_FK_Media");

                entity.HasOne(d => d.Query)
                    .WithMany(p => p.QueryDatas)
                    .HasForeignKey(d => d.QueryId)
                    .HasConstraintName("QueryData_FK_Query");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QueryDatas)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("QueryData_FK_UserAccount");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.HasIndex(e => e.LessonSectionId)
                    .HasName("Question_FK_LessonSection");

                entity.HasIndex(e => e.MediaId)
                    .HasName("Question_FK_Media");

                entity.Property(e => e.QuestionId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LessonSectionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MediaId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.LessonSection)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.LessonSectionId)
                    .HasConstraintName("Question_FK_LessonSection");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Question_FK_Media");
            });

            modelBuilder.Entity<QuestionHint>(entity =>
            {
                entity.ToTable("QuestionHint");

                entity.HasIndex(e => e.MediaId)
                    .HasName("QuestionHint_FK_Media");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("QuestionHint_FK_Question");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.HintText)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MediaId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.QuestionHints)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("QuestionHint_FK_Media");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionHints)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("QuestionHint_FK_Question");
            });

            modelBuilder.Entity<QuestionMedia>(entity =>
            {
                entity.ToTable("QuestionMedia");

                entity.HasIndex(e => e.LanguageId)
                    .HasName("QuestionMedia_FK_Language");

                entity.HasIndex(e => e.MediaId)
                    .HasName("QuestionMedia_FK_Media");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("QuestionMedia_FK_Question");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MediaId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.QuestionMedias)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("QuestionMedia_FK_Language");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.QuestionMedias)
                    .HasForeignKey(d => d.MediaId)
                    .HasConstraintName("QuestionMedia_FK_Media");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionMedias)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("QuestionMedia_FK_Question");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.ToTable("QuestionType");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.QuestionTypeDescription)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.RoleDescription)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("School");

                entity.HasIndex(e => e.SchoolCode)
                    .HasName("School_UC")
                    .IsUnique();

                entity.HasIndex(e => e.StateId)
                    .HasName("School_FK_State");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AddressLine2)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ContactNumber)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.SchoolCode)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SchoolName)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Schools)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("School_FK_State");
            });

            modelBuilder.Entity<SectionType>(entity =>
            {
                entity.ToTable("SectionType");

                entity.HasIndex(e => e.SectionTypeCode)
                    .HasName("SectionType_UC")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.SectionTypeCode)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SectionTypeDescription)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<SubQuestion>(entity =>
            {
                entity.ToTable("SubQuestion");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("SubQuestion_FK_Question");

                entity.HasIndex(e => e.QuestionTypeId)
                    .HasName("SubQuestion_FK_QuestionType");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SubQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("SubQuestion_FK_Question");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.SubQuestions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .HasConstraintName("SubQuestion_FK_QuestionType");
            });

            modelBuilder.Entity<SubQuestionAnswer>(entity =>
            {
                entity.ToTable("SubQuestionAnswer");

                entity.HasIndex(e => e.SubQuestionId)
                    .HasName("SubQuestionAnswer_FK_SubQuestion");

                entity.HasIndex(e => e.SubQuestionOptionId)
                    .HasName("SubQuestionAnswer_FK_SubQuestionOption");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.SubQuestion)
                    .WithMany(p => p.SubQuestionAnswers)
                    .HasForeignKey(d => d.SubQuestionId)
                    .HasConstraintName("SubQuestionAnswer_FK_SubQuestion");

                entity.HasOne(d => d.SubQuestionOption)
                    .WithMany(p => p.SubQuestionAnswers)
                    .HasForeignKey(d => d.SubQuestionOptionId)
                    .HasConstraintName("SubQuestionAnswer_FK_SubQuestionOption");
            });

            modelBuilder.Entity<SubQuestionOption>(entity =>
            {
                entity.ToTable("SubQuestionOption");

                entity.HasIndex(e => e.SubQuestionId)
                    .HasName("SubQuestionOption_FK_SubQuestion");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.OptionText)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.SubQuestion)
                    .WithMany(p => p.SubQuestionOptions)
                    .HasForeignKey(d => d.SubQuestionId)
                    .HasConstraintName("SubQuestionOption_FK_SubQuestion");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.HasIndex(e => e.SubjectCode)
                    .HasName("Subject_UC")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.SubjectCode)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasIndex(e => e.ActiveLessonSetId)
                    .HasName("Teacher_FK_LessonSet");

                entity.HasIndex(e => e.MentorId)
                    .HasName("Teacher_FK_UserAccount_Mentor");

                entity.HasIndex(e => e.SchoolId)
                    .HasName("Teacher_FK_UserAccount_School");

                entity.Property(e => e.TeacherId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ActiveLessonSetId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MentorId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.ActiveLessonSet)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.ActiveLessonSetId)
                    .HasConstraintName("Teacher_FK_LessonSet");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Teacher_FK_UserAccount_Mentor");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("Teacher_FK_UserAccount_School");

                entity.HasOne(d => d.TeacherNavigation)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.TeacherId)
                    .HasConstraintName("Teacher_FK_UserAccount_TeacherId");
            });

            modelBuilder.Entity<TeacherClass>(entity =>
            {
                entity.ToTable("TeacherClass");

                entity.HasIndex(e => e.ClassId)
                    .HasName("TeacherClass_FK_Class");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("TeacherClass_FK_Teacher");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TeacherClasses)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("TeacherClass_FK_Class");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherClasses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("TeacherClass_FK_Teacher");
            });

            modelBuilder.Entity<TeacherResponse>(entity =>
            {
                entity.ToTable("TeacherResponse");

                entity.HasIndex(e => e.MediaId)
                    .HasName("TeacherResponse_FK_Media");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("TeacherResponse_FK_Question");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("TeacherResponse_FK_Teacher");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MediaId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ResponseText)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.TeacherResponses)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TeacherResponse_FK_Media");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TeacherResponses)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("TeacherResponse_FK_Question");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherResponses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("TeacherResponse_FK_Teacher");
            });

            modelBuilder.Entity<TeacherResponseStatu>(entity =>
            {
                entity.HasKey(e => e.TeacherResponseStatusId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("TeacherResponseStatus_FK_Question");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("TeacherResponseStatus_FK_Teacher");

                entity.HasIndex(e => e.UpdatedBy)
                    .HasName("TeacherResponseStatus_FK_UserAccount");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TeacherResponseStatus)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("TeacherResponseStatus_FK_Question");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherResponseStatus)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("TeacherResponseStatus_FK_Teacher");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.TeacherResponseStatus)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("TeacherResponseStatus_FK_UserAccount");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");                

                entity.Property(e => e.msgfrom)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                entity.Property(e => e.msgto)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                entity.Property(e => e.roleid)
                    .IsRequired()
                    .HasColumnType("int(11)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                entity.Property(e => e.message)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                entity.Property(e => e.created_date)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
                entity.Property(e => e.status)
                    .IsRequired()
                    .HasColumnType("int(11)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

            });

            modelBuilder.Entity<syncdateinfo>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.userId)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.syncdate)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");             

            });


            modelBuilder.Entity<TeacherSubQuestionResponse>(entity =>
            {
                entity.ToTable("TeacherSubQuestionResponse");

                entity.HasIndex(e => e.SubQuestionId)
                    .HasName("TeacherSubQuestionResponse_FK_SubQuestion");

                entity.HasIndex(e => e.SubQuestionOptionId)
                    .HasName("TeacherSubQuestionResponse_FK_SubQuestionOption");

                entity.HasIndex(e => e.TeacherResponseId)
                    .HasName("TeacherSubQuestionResponse_FK_TeacherResponse");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.SubQuestion)
                    .WithMany(p => p.TeacherSubQuestionResponses)
                    .HasForeignKey(d => d.SubQuestionId)
                    .HasConstraintName("TeacherSubQuestionResponse_FK_SubQuestion");

                entity.HasOne(d => d.SubQuestionOption)
                    .WithMany(p => p.TeacherSubQuestionResponses)
                    .HasForeignKey(d => d.SubQuestionOptionId)
                    .HasConstraintName("TeacherSubQuestionResponse_FK_SubQuestionOption");

                entity.HasOne(d => d.TeacherResponse)
                    .WithMany(p => p.TeacherSubQuestionResponses)
                    .HasForeignKey(d => d.TeacherResponseId)
                    .HasConstraintName("TeacherSubQuestionResponse_FK_TeacherResponse");
            });

            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.ToTable("TeacherSubject");

                entity.HasIndex(e => e.SubjectId)
                    .HasName("TeacherSubject_FK_Subject");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("TeacherSubject_FK_Teacher");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TeacherSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("TeacherSubject_FK_Subject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherSubjects)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("TeacherSubject_FK_Teacher");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("UserAccount");

                entity.HasIndex(e => e.Token)
                    .HasName("UserAccount_UC_Token")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UserAccount_UC_Username")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastFailedLoginAttempt).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LockExpiry).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PasswordSalt)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Token)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TokenExpiry).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(320)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<UserLanguage>(entity =>
            {
                entity.ToTable("UserLanguage");

                entity.HasIndex(e => e.LanguageId)
                    .HasName("UserLanguage_FK_Language");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserLanguage_FK_UserAccount");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.UserLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("UserLanguage_FK_Language");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLanguages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserLanguage_FK_UserAccount");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("UserProfile");

                entity.HasIndex(e => e.Email)
                    .HasName("UserProfile_UC_Email")
                    .IsUnique();

                entity.HasIndex(e => e.GenderId)
                    .HasName("UserProfile_FK_Gender");

                entity.HasIndex(e => e.MobileNumber)
                    .HasName("UserProfile_UC_Mobile")
                    .IsUnique();

                entity.HasIndex(e => e.QualificationId)
                    .HasName("UserProfile_FK_Qualification");

                entity.HasIndex(e => e.StateId)
                    .HasName("UserProfile_FK_State");

                entity.Property(e => e.UserId)
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.City)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(320)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.MobileNumber)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.OtherQualification)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UserProfile_FK_Gender");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UserProfile_FK_Qualification");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UserProfile_FK_State");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserProfile)
                    .HasForeignKey<UserProfile>(d => d.UserId)
                    .HasConstraintName("UserProfile_FK_UserAccount");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasIndex(e => e.RoleId)
                    .HasName("UserRole_FK_Role");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserRole_FK_UserAccount");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("char(38)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("UserRole_FK_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserRole_FK_UserAccount");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
