DROP TABLE Query;
DROP TABLE QuestionResponse;
DROP TABLE QuestionHint;
DROP TABLE QuestionMedia;
DROP TABLE Question;
DROP TABLE InstructionMedia;
DROP TABLE Instruction;
DROP TABLE LessonSection;
DROP TABLE Lesson;
DROP TABLE LessonSet;
DROP TABLE Level;
DROP TABLE Media;
DROP TABLE SectionType;
DROP TABLE MediaType;

CREATE TABLE MediaType
(
	MediaTypeId INT NOT NULL AUTO_INCREMENT,
	MediaTypeDescription VARCHAR(100) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT MediaType_PK PRIMARY KEY (MediaTypeId)
);

CREATE TABLE SectionType
(
	SectionTypeId INT NOT NULL AUTO_INCREMENT,
	SectionTypeCode VARCHAR(50) NOT NULL,
	SectionTypeDescription VARCHAR(100) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT SectionType_PK PRIMARY KEY (SectionTypeId),	
	CONSTRAINT SectionType_UC UNIQUE (SectionTypeCode)
);

CREATE TABLE QuestionType
(
	QuestionTypeId INT NOT NULL AUTO_INCREMENT,
	QuestionTypeDescription VARCHAR(100) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT QuestionType_PK PRIMARY KEY (QuestionTypeId)
);

CREATE TABLE Media
(
	MediaId CHAR(38) NOT NULL,
	MediaTypeId INT NOT NULL,
	MediaSource VARCHAR(300) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Media_PK PRIMARY KEY (MediaId),
	CONSTRAINT Media_FK_MediaType FOREIGN KEY (MediaTypeId) REFERENCES MediaType(MediaTypeId) ON DELETE CASCADE
);

CREATE TABLE Level
(
	LevelId INT NOT NULL AUTO_INCREMENT,
	LevelCode VARCHAR(50) NOT NULL,	
	LevelDescription VARCHAR(100),
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Level_PK PRIMARY KEY (LevelId),
	CONSTRAINT Level_UC UNIQUE (LevelCode)
);

CREATE TABLE LessonSet
(
	LessonSetId CHAR(38) NOT NULL,
	LessonSetOrder INT NOT NULL,
	LevelId INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT LessonSet_PK PRIMARY KEY (LessonSetId),
	CONSTRAINT LessonSet_UC UNIQUE (LessonSetOrder),
	CONSTRAINT LessonSet_FK_Level FOREIGN KEY (LevelId) REFERENCES Level(LevelId) ON DELETE CASCADE
);

CREATE TABLE Lesson
(
	LessonId CHAR(38) NOT NULL,
	LessonSetId CHAR(38) NOT NULL,
	LessonCode VARCHAR(50) NOT NULL,
	LessonNumber INT NOT NULL,
	LessonName VARCHAR(100) NOT NULL,	
	LessonDescription VARCHAR(100),
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Lesson_PK PRIMARY KEY (LessonId),
	CONSTRAINT Lesson_UC UNIQUE (LessonCode),
	CONSTRAINT Lesson_FK_LessonSet FOREIGN KEY (LessonSetId) REFERENCES LessonSet(LessonSetId) ON DELETE CASCADE
);

CREATE TABLE LessonSection
(
	LessonSectionId CHAR(38) NOT NULL,
	LessonId CHAR(38) NOT NULL,	
	SectionTypeId INT NOT NULL,
	LessonSectionName VARCHAR(100) NOT NULL,
	LessonSectionDescription VARCHAR(1000) NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT LessonSection_PK PRIMARY KEY (LessonSectionId),
	CONSTRAINT LessonSection_FK_Lesson FOREIGN KEY (LessonId) REFERENCES Lesson(LessonId) ON DELETE CASCADE,
	CONSTRAINT LessonSection_FK_SectionType FOREIGN KEY (SectionTypeId) REFERENCES SectionType(SectionTypeId) ON DELETE CASCADE
);

CREATE TABLE Instruction
(
	InstructionId INT NOT NULL AUTO_INCREMENT,
	LessonSectionId CHAR(38) NOT NULL,	
	InstructionDescription VARCHAR(100),
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Instruction_PK PRIMARY KEY (InstructionId),
	CONSTRAINT Instruction_UC UNIQUE (LessonSectionId),
	CONSTRAINT Instruction_FK_LessonSection FOREIGN KEY (LessonSectionId) REFERENCES LessonSection(LessonSectionId) ON DELETE CASCADE  
);

CREATE TABLE InstructionMedia
(
	InstructionMediaId INT NOT NULL AUTO_INCREMENT,
	InstructionId INT NOT NULL,	
	LanguageId INT NOT NULL,
	MediaId CHAR(38) NOT NULL,
	MediaDescription VARCHAR(1000),
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT InstructionMedia_PK PRIMARY KEY (InstructionMediaId),
	CONSTRAINT InstructionMedia_FK_Media FOREIGN KEY (MediaId) REFERENCES Media(MediaId) ON DELETE CASCADE,
	CONSTRAINT InstructionMedia_FK_Instruction FOREIGN KEY (InstructionId) REFERENCES Instruction(InstructionId) ON DELETE CASCADE,
	CONSTRAINT InstructionMedia_FK_Language FOREIGN KEY (LanguageId) REFERENCES Language(LanguageId) ON DELETE CASCADE  
);

CREATE TABLE Question
(
	QuestionId CHAR(38) NOT NULL,
	LessonSectionId CHAR(38) NOT NULL,
	QuestionOrder INT NOT NULL,
	QuestionText TEXT NOT NULL,
	MediaId CHAR(38) NULL,
	RecommendedAttempts INT NULL,
	SecondaryAttempts INT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Question_PK PRIMARY KEY (QuestionId),
	CONSTRAINT Question_FK_Media FOREIGN KEY (MediaId) REFERENCES Media(MediaId) ON DELETE CASCADE,
	CONSTRAINT Question_FK_LessonSection FOREIGN KEY (LessonSectionId) REFERENCES LessonSection(LessonSectionId) ON DELETE CASCADE
);

CREATE TABLE QuestionMedia
(
	QuestionMediaId INT NOT NULL AUTO_INCREMENT,
	LanguageId INT NOT NULL,	
	QuestionId CHAR(38) NOT NULL,
	MediaId CHAR(38) NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT QuestionMedia_PK PRIMARY KEY (QuestionMediaId),
	CONSTRAINT QuestionMedia_FK_Media FOREIGN KEY (MediaId) REFERENCES Media(MediaId) ON DELETE CASCADE,
	CONSTRAINT QuestionMedia_FK_Language FOREIGN KEY (LanguageId) REFERENCES Language(LanguageId) ON DELETE CASCADE,
    CONSTRAINT QuestionMedia_FK_Question FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId) ON DELETE CASCADE
);

CREATE TABLE QuestionHint
(
	QuestionHintId INT NOT NULL AUTO_INCREMENT,
	QuestionId CHAR(38) NOT NULL,
	HintText VARCHAR(200) NOT NULL,
	MediaId CHAR(38) NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT QuestionHint_PK PRIMARY KEY (QuestionHintId),
	CONSTRAINT QuestionHint_FK_Media FOREIGN KEY (MediaId) REFERENCES Media(MediaId) ON DELETE CASCADE,
    CONSTRAINT QuestionHint_FK_Question FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId) ON DELETE CASCADE
);

CREATE TABLE SubQuestion
(
	SubQuestionId INT NOT NULL AUTO_INCREMENT,
	QuestionId CHAR(38) NOT NULL,
	QuestionTypeId INT NOT NULL,
	QuestionOrder INT NOT NULL,
	QuestionText VARCHAR(200) NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT SubQuestion_PK PRIMARY KEY (SubQuestionId),
	CONSTRAINT SubQuestion_FK_Question FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId) ON DELETE CASCADE,
	CONSTRAINT SubQuestion_FK_QuestionType FOREIGN KEY (QuestionTypeId) REFERENCES QuestionType(QuestionTypeId) ON DELETE CASCADE
);

CREATE TABLE SubQuestionOption
(
	SubQuestionOptionId INT NOT NULL AUTO_INCREMENT,
	SubQuestionId INT NOT NULL,
	OptionText VARCHAR(200) NOT NULL,
	OptionOrder INT NOT NULL,
	OptionHidden BOOLEAN NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT SubQuestionOption_PK PRIMARY KEY (SubQuestionOptionId),
	CONSTRAINT SubQuestionOption_FK_SubQuestion FOREIGN KEY (SubQuestionId) REFERENCES SubQuestion(SubQuestionId) ON DELETE CASCADE
);

CREATE TABLE SubQuestionAnswer
(
	SubQuestionAnswerId INT NOT NULL AUTO_INCREMENT,
	SubQuestionId INT NOT NULL,
	SubQuestionOptionId INT NOT NULL,
	AnswerOrder INT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT SubQuestionAnswer_PK PRIMARY KEY (SubQuestionAnswerId),
	CONSTRAINT SubQuestionAnswer_FK_SubQuestion FOREIGN KEY (SubQuestionId) REFERENCES SubQuestion(SubQuestionId) ON DELETE CASCADE,
	CONSTRAINT SubQuestionAnswer_FK_SubQuestionOption FOREIGN KEY (SubQuestionOptionId) REFERENCES SubQuestionOption(SubQuestionOptionId) ON DELETE CASCADE
);

CREATE TABLE TeacherResponseStatus
(
	TeacherResponseStatusId INT NOT NULL AUTO_INCREMENT,
	QuestionId CHAR(38) NOT NULL,
	TeacherId CHAR(38) NOT NULL,
	ResponseState INT NOT NULL,
	UpdatedBy CHAR(38) NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT TeacherResponseStatus_PK PRIMARY KEY (TeacherResponseStatusId),
	CONSTRAINT TeacherResponseStatus_FK_Teacher FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId) ON DELETE CASCADE,
    CONSTRAINT TeacherResponseStatus_FK_Question FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId) ON DELETE CASCADE,
	CONSTRAINT TeacherResponseStatus_FK_UserAccount FOREIGN KEY (UpdatedBy) REFERENCES UserAccount(UserId) ON DELETE CASCADE
);


CREATE TABLE TeacherResponse
(
	TeacherResponseId INT NOT NULL AUTO_INCREMENT,
	QuestionId CHAR(38) NOT NULL,
	TeacherId CHAR(38) NOT NULL,
	ResponseText VARCHAR(200) NOT NULL,
	MediaId CHAR(38) NULL,
	IsRevised BOOLEAN NOT NULL,
	Attempts INT NULL,
	Score INT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT TeacherResponse_PK PRIMARY KEY (TeacherResponseId),
	CONSTRAINT TeacherResponse_FK_Media FOREIGN KEY (MediaId) REFERENCES Media(MediaId) ON DELETE CASCADE,
    CONSTRAINT TeacherResponse_FK_Question FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId) ON DELETE CASCADE,
	CONSTRAINT TeacherResponse_FK_Teacher FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId) ON DELETE CASCADE
);

CREATE TABLE TeacherSubQuestionResponse
(
	TeacherSubQuestionResponseId INT NOT NULL AUTO_INCREMENT,
	TeacherResponseId INT NOT NULL,
	SubQuestionId INT NOT NULL,
	SubQuestionOptionId INT NOT NULL,
	AnswerOrder INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT TeacherSubQuestionResponse_PK PRIMARY KEY (TeacherSubQuestionResponseId),
	CONSTRAINT TeacherSubQuestionResponse_FK_TeacherResponse FOREIGN KEY (TeacherResponseId) REFERENCES TeacherResponse(TeacherResponseId) ON DELETE CASCADE,
    CONSTRAINT TeacherSubQuestionResponse_FK_SubQuestion FOREIGN KEY (SubQuestionId) REFERENCES SubQuestion(SubQuestionId) ON DELETE CASCADE,
	CONSTRAINT TeacherSubQuestionResponse_FK_SubQuestionOption FOREIGN KEY (SubQuestionOptionId) REFERENCES SubQuestionOption(SubQuestionOptionId) ON DELETE CASCADE
);

CREATE TABLE MentorResponse
(
	MentorResponseId INT NOT NULL AUTO_INCREMENT,
	TeacherResponseId INT NOT NULL,
	MentorId CHAR(38) NOT NULL,
	MentorComments VARCHAR(200) NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT MentorResponse_PK PRIMARY KEY (MentorResponseId),
	CONSTRAINT MentorResponse_UC UNIQUE (TeacherResponseId),
	CONSTRAINT MentorResponse_FK_TeacherResponse FOREIGN KEY (TeacherResponseId) REFERENCES TeacherResponse(TeacherResponseId) ON DELETE CASCADE,
	CONSTRAINT MentorResponse_FK_Mentor FOREIGN KEY (MentorId) REFERENCES Mentor(MentorId) ON DELETE CASCADE
);

CREATE TABLE Query
(
	QueryId CHAR(38) NOT NULL,
	QuestionId CHAR(38) NOT NULL,
	TeacherId CHAR(38) NOT NULL,
	QueryStatus INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Query_PK PRIMARY KEY (QueryId),
    CONSTRAINT Query_FK_Question FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId) ON DELETE CASCADE,
	CONSTRAINT Query_FK_Teacher FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId) ON DELETE CASCADE
);

CREATE TABLE QueryData
(
	QueryDataId INT NOT NULL AUTO_INCREMENT,
	QueryId CHAR(38) NOT NULL,
	UserId CHAR(38) NOT NULL,
	QueryText VARCHAR(200) NOT NULL,
	MediaId CHAR(38) NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT QueryData_PK PRIMARY KEY (QueryDataId),
	CONSTRAINT QueryData_FK_Media FOREIGN KEY (MediaId) REFERENCES Media(MediaId) ON DELETE CASCADE,
    CONSTRAINT QueryData_FK_Query FOREIGN KEY (QueryId) REFERENCES Query(QueryId) ON DELETE CASCADE,
	CONSTRAINT QueryData_FK_UserAccount FOREIGN KEY (UserId) REFERENCES UserAccount(UserId) ON DELETE CASCADE
);
