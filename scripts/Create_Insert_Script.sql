-- -------------------------------------------------------------------------------------------------------------------------------------
--                                             DROP DATABASE AND TABLES SCRIPTS
-- -------------------------------------------------------------------------------------------------------------------------------------
-- DROP DATABASE VidyaVahini;

-- -------------------------------------------------------------------------------------------------------------------------------------
--                                             CREATE NEW DATABASE SCRIPT
-- -------------------------------------------------------------------------------------------------------------------------------------
-- CREATE DATABASE VidyaVahini;

-- USE VidyaVahini;

-- -------------------------------------------------------------------------------------------------------------------------------------
--                                             TABLES DEFINTION SCRIPT
-- -------------------------------------------------------------------------------------------------------------------------------------

-- --------------------------------------------COMMON TABLES START --------------------------------------------------
CREATE TABLE Gender
( 
	GenderId INT NOT NULL AUTO_INCREMENT,
    GenderCode CHAR(1) NOT NULL,
	GenderDescription VARCHAR(50) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Gender_PK PRIMARY KEY (GenderId),
    CONSTRAINT Gender_UC UNIQUE (GenderCode)
);

CREATE TABLE State
( 
	StateId INT NOT NULL AUTO_INCREMENT,
	StateName VARCHAR(100) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT State_PK PRIMARY KEY (StateId)
);

CREATE TABLE Language
(
	LanguageId INT NOT NULL AUTO_INCREMENT,
	LanguageName VARCHAR(100) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Language_PK PRIMARY KEY (LanguageId)
);

CREATE TABLE Role
( 
	RoleId INT NOT NULL AUTO_INCREMENT,
	RoleDescription VARCHAR(50) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Role_PK PRIMARY KEY (RoleId)
);

CREATE TABLE Qualification
( 
	QualificationId INT NOT NULL AUTO_INCREMENT,
	QualificationDescription VARCHAR(50) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Qualification_PK PRIMARY KEY (QualificationId)
);

CREATE TABLE Error
( 
	ErrorId INT NOT NULL AUTO_INCREMENT,
    ErrorCode VARCHAR(10) NOT NULL,
	ErrorMessage VARCHAR(200) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Error_PK PRIMARY KEY (ErrorId),
    CONSTRAINT Error_UC UNIQUE (ErrorCode)
);
-- --------------------------------------------COMMON TABLES END --------------------------------------------------

-- --------------------------------------------SCHOOL TABLES START --------------------------------------------------
CREATE TABLE School
(
	SchoolId INT NOT NULL AUTO_INCREMENT,
	SchoolCode VARCHAR(10) NOT NULL,
	SchoolName VARCHAR(100) NULL,
	ContactNumber VARCHAR(20) NULL,
	Email VARCHAR(50) NULL,
	AddressLine1 VARCHAR(200) NOT NULL,
	AddressLine2 VARCHAR(200) NULL,
	City VARCHAR(200) NOT NULL,
	Area VARCHAR(200) NOT NULL,
	StateId INT NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT School_PK PRIMARY KEY (SchoolId),
    CONSTRAINT School_UC UNIQUE (SchoolCode),
    CONSTRAINT School_FK_State FOREIGN KEY (StateId) REFERENCES State(StateId)    
);

CREATE TABLE Subject
(
	SubjectId INT NOT NULL AUTO_INCREMENT,
    SubjectCode VARCHAR(10) NOT NULL,
	SubjectName VARCHAR(100) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Subject_PK PRIMARY KEY (SubjectId),
	CONSTRAINT Subject_UC UNIQUE (SubjectCode)
);
	
CREATE TABLE Class
(
	ClassId INT NOT NULL AUTO_INCREMENT,
    ClassCode INT NOT NULL,
	ClassDescription VARCHAR(100) NOT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT Class_PK PRIMARY KEY (ClassId),
    CONSTRAINT Class_UC UNIQUE (ClassCode)
);
-- --------------------------------------------SCHOOL TABLES END --------------------------------------------------

-- --------------------------------------------USER TABLES START --------------------------------------------------
    
CREATE TABLE UserAccount
( 
	UserId CHAR(38) NOT NULL,
	Username VARCHAR(320) NOT NULL,
	PasswordSalt CHAR(38) NULL,
	PasswordHash VARCHAR(2000) NULL,
	IsActive BOOLEAN NOT NULL,
	IsRegistered BOOLEAN NOT NULL,
	ActivationEmailCount INT NOT NULL,
	FailedLoginAttempt INT NOT NULL,
	LastFailedLoginAttempt DATETIME NULL,
	LockExpiry DATETIME NULL,
	Token CHAR(38) NULL,
	TokenExpiry DATETIME NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT UserAccount_PK PRIMARY KEY (UserId),
	CONSTRAINT UserAccount_UC_Username UNIQUE (Username),
	CONSTRAINT UserAccount_UC_Token UNIQUE (Token)
);

CREATE TABLE UserProfile
(
	UserId CHAR(38) NOT NULL,
	Name VARCHAR(200) NOT NULL,
	GenderId INT NULL,
	MobileNumber VARCHAR(20) NULL,
	Email VARCHAR(320) NOT NULL,
	QualificationId INT NULL,
	OtherQualification VARCHAR(200) NULL,
	City VARCHAR(200) NULL,
	StateId INT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT UserProfile_PK PRIMARY KEY (UserId),
	CONSTRAINT UserProfile_UC_Email UNIQUE (Email),
	CONSTRAINT UserProfile_UC_Mobile UNIQUE (MobileNumber),
    CONSTRAINT UserProfile_FK_UserAccount FOREIGN KEY (UserId) REFERENCES UserAccount(UserId) ON DELETE CASCADE,
    CONSTRAINT UserProfile_FK_Gender FOREIGN KEY (GenderId) REFERENCES Gender(GenderId) ON DELETE CASCADE,
    CONSTRAINT UserProfile_FK_State FOREIGN KEY (StateId) REFERENCES State(StateId) ON DELETE CASCADE,
	CONSTRAINT UserProfile_FK_Qualification FOREIGN KEY (QualificationId) REFERENCES Qualification(QualificationId) ON DELETE CASCADE
);

CREATE TABLE UserRole
(
	UserRoleId INT NOT NULL AUTO_INCREMENT,
	UserId CHAR(38) NOT NULL,
	RoleId INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT UserRole_PK PRIMARY KEY (UserRoleId),
    CONSTRAINT UserRole_FK_UserAccount FOREIGN KEY (UserId) REFERENCES UserAccount(UserId) ON DELETE CASCADE,
    CONSTRAINT UserRole_FK_Role FOREIGN KEY (RoleId) REFERENCES Role(RoleId) ON DELETE CASCADE
);

CREATE TABLE UserLanguage
(
	UserLanguageId INT NOT NULL AUTO_INCREMENT,
	UserId CHAR(38) NOT NULL,
	LanguageId INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT UserLanguage_PK PRIMARY KEY (UserLanguageId),
    CONSTRAINT UserLanguage_FK_UserAccount FOREIGN KEY (UserId) REFERENCES UserAccount(UserId) ON DELETE CASCADE,
    CONSTRAINT UserLanguage_FK_Language FOREIGN KEY (LanguageId) REFERENCES Language(LanguageId) ON DELETE CASCADE
);
-- --------------------------------------------USER TABLES END --------------------------------------------------
-- --------------------------------------------MENTOR TABLES START --------------------------------------------------
CREATE TABLE Mentor
(
	MentorId CHAR(38) NOT NULL,	
	WorkingInSSSVV BOOLEAN NOT NULL,
	SSSVVVoluteerName VARCHAR(100) NULL,
	WorkingInSaiOrganization BOOLEAN NOT NULL,
	SaiOrganizationVoluteerName VARCHAR(100) NULL,
	EnglishTeachingExperience VARCHAR(50) NOT NULL,
	Occupation VARCHAR(50) NOT NULL,
	TimeCapacity INT NOT NULL,
	TeachersCapacity INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Mentor_PK PRIMARY KEY (MentorId),
    CONSTRAINT Mentor_FK_UserAccount FOREIGN KEY (MentorId) REFERENCES UserAccount(UserId) ON DELETE CASCADE
);
-- --------------------------------------------MENTOR TABLES END --------------------------------------------------
-- --------------------------------------------TEACHER TABLES START --------------------------------------------------
CREATE TABLE Teacher
(
	TeacherId CHAR(38) NOT NULL,
	SchoolId INT NOT NULL,
	MentorId CHAR(38) NULL,
	ActiveLessonSetId CHAR(38) NOT NULL,
	LoadNextLessonSet BOOLEAN NOT NULL,
	RedoActiveLessonSet BOOLEAN NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Teacher_PK PRIMARY KEY (TeacherId),
    CONSTRAINT Teacher_FK_UserAccount_TeacherId FOREIGN KEY (TeacherId) REFERENCES UserAccount(UserId) ON DELETE CASCADE,
	CONSTRAINT Teacher_FK_UserAccount_School FOREIGN KEY (SchoolId) REFERENCES School(SchoolId) ON DELETE CASCADE,
    CONSTRAINT Teacher_FK_UserAccount_Mentor FOREIGN KEY (MentorId) REFERENCES Mentor(MentorId) ON DELETE CASCADE,
	CONSTRAINT Teacher_FK_LessonSet FOREIGN KEY (ActiveLessonSetId) REFERENCES LessonSet(LessonSetId) ON DELETE CASCADE
);
	
CREATE TABLE TeacherSubject
(
	TeacherSubjectId INT NOT NULL AUTO_INCREMENT,
	TeacherId CHAR(38) NOT NULL,
	SubjectId INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT TeacherSubject_PK PRIMARY KEY (TeacherSubjectId),
    CONSTRAINT TeacherSubject_FK_Teacher FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId) ON DELETE CASCADE,
    CONSTRAINT TeacherSubject_FK_Subject FOREIGN KEY (SubjectId) REFERENCES Subject(SubjectId) ON DELETE CASCADE
);
	
CREATE TABLE TeacherClass
(
	TeacherClassId INT NOT NULL AUTO_INCREMENT,
	TeacherId CHAR(38) NOT NULL,
	ClassId INT NOT NULL,
    Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT TeacherClass_PK PRIMARY KEY (TeacherClassId),
    CONSTRAINT TeacherClass_FK_Teacher FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId) ON DELETE CASCADE,
    CONSTRAINT TeacherClass_FK_Class FOREIGN KEY (ClassId) REFERENCES Class(ClassId) ON DELETE CASCADE
);
-- --------------------------------------------TEACHER TABLES END --------------------------------------------------



-- --------------------------------------------SCHOOL TABLES START --------------------------------------------------

-- -------------------------------------------------------------------------------------------------------------------------------------
--                                             INSERT SCRIPTS FOR TABLES
-- -------------------------------------------------------------------------------------------------------------------------------------

-- --------------------------------------------COMMON TABLES START --------------------------------------------------

-- Gender
INSERT INTO Gender (GenderCode, GenderDescription) VALUES ('M', 'Male');
INSERT INTO Gender (GenderCode, GenderDescription) VALUES ('F', 'Female');

-- State
INSERT INTO State (StateName) VALUES ('Andhra Pradesh');
INSERT INTO State (StateName) VALUES ('Assam');
INSERT INTO State (StateName) VALUES ('Arunachal Pradesh');
INSERT INTO State (StateName) VALUES ('Chattisgarh');
INSERT INTO State (StateName) VALUES ('Gujrat');
INSERT INTO State (StateName) VALUES ('Bihar');
INSERT INTO State (StateName) VALUES ('Haryana');
INSERT INTO State (StateName) VALUES ('Himachal Pradesh');
INSERT INTO State (StateName) VALUES ('Jammu & Kashmir');
INSERT INTO State (StateName) VALUES ('Jharkhand');
INSERT INTO State (StateName) VALUES ('Karnataka');
INSERT INTO State (StateName) VALUES ('Kerala');
INSERT INTO State (StateName) VALUES ('Madhya Pradesh');
INSERT INTO State (StateName) VALUES ('Maharashtra');
INSERT INTO State (StateName) VALUES ('Manipur');
INSERT INTO State (StateName) VALUES ('Meghalaya');
INSERT INTO State (StateName) VALUES ('Mizoram');
INSERT INTO State (StateName) VALUES ('Nagaland');
INSERT INTO State (StateName) VALUES ('Odisha');
INSERT INTO State (StateName) VALUES ('Punjab');
INSERT INTO State (StateName) VALUES ('Rajasthan');
INSERT INTO State (StateName) VALUES ('Sikkim');
INSERT INTO State (StateName) VALUES ('Tamil Nadu');
INSERT INTO State (StateName) VALUES ('Telangana');
INSERT INTO State (StateName) VALUES ('Tripura');
INSERT INTO State (StateName) VALUES ('Uttar Pradesh');
INSERT INTO State (StateName) VALUES ('Uttarakhand');
INSERT INTO State (StateName) VALUES ('West Bengal');
INSERT INTO State (StateName) VALUES ('Delhi');
INSERT INTO State (StateName) VALUES ('Goa');
INSERT INTO State (StateName) VALUES ('Puducherry');
INSERT INTO State (StateName) VALUES ('Lakshadweep');
INSERT INTO State (StateName) VALUES ('Daman & Diu');
INSERT INTO State (StateName) VALUES ('Dadra & Nagar');
INSERT INTO State (StateName) VALUES ('Chandigarh');
INSERT INTO State (StateName) VALUES ('Andaman & Nicobar');

-- Language
INSERT INTO Language (LanguageName) VALUES ('English');
INSERT INTO Language (LanguageName) VALUES ('Hindi');
INSERT INTO Language (LanguageName) VALUES ('Bengali');
INSERT INTO Language (LanguageName) VALUES ('Kannada');
INSERT INTO Language (LanguageName) VALUES ('Malayalam');
INSERT INTO Language (LanguageName) VALUES ('Manipuri');
INSERT INTO Language (LanguageName) VALUES ('Marathi');
INSERT INTO Language (LanguageName) VALUES ('Marwari');
INSERT INTO Language (LanguageName) VALUES ('Odia');
INSERT INTO Language (LanguageName) VALUES ('Punjabi');
INSERT INTO Language (LanguageName) VALUES ('Tamil');
INSERT INTO Language (LanguageName) VALUES ('Telegu');

-- Role
INSERT INTO Role (RoleDescription) VALUES ('Administrator');
INSERT INTO Role (RoleDescription) VALUES ('Core Team');
INSERT INTO Role (RoleDescription) VALUES ('Senior Mentor');
INSERT INTO Role (RoleDescription) VALUES ('Mentor');
INSERT INTO Role (RoleDescription) VALUES ('Teacher');

-- Qualification
INSERT INTO Qualification (QualificationDescription) VALUES ('Undergraduate');
INSERT INTO Qualification (QualificationDescription) VALUES ('Graduate');
INSERT INTO Qualification (QualificationDescription) VALUES ('Doctorate');
INSERT INTO Qualification (QualificationDescription) VALUES ('Other');

-- Error
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV001', 'An unexpected error occured during execution. Please contact the administrator');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV002', 'Your details are not present in our system. Please contact your school principal');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV003', 'The activation link is either invalid or expired. Please contact the administrator.');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV004', 'Invalid Credentials. Please try again.');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV005', 'This email is already registered. Please use a different email.');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV006', 'This user acccount does not exist.');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV007', 'Error sending notification.');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV008', 'Question not found');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV009', 'Section not found');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV010', 'Media not found');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV011', 'No mentors are available for assignment');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV012', 'No teachers missing mentor');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV013', 'Invalid teacher or mentor');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV014', 'No lessons available to load in the dashboard');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV015', 'Question or user response not found');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV016', 'Invalid response state');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV017', 'Either mentor not found or has not active mentees');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV018', 'Invalid Lesson Set');
INSERT INTO Error (ErrorCode, ErrorMessage) VALUES ('VV019', 'Unable to fetch the next lesson set or last lesson set completed');

-- --------------------------------------------COMMON TABLES END --------------------------------------------------

-- --------------------------------------------SCHOOL TABLES START --------------------------------------------------

-- School 
INSERT INTO School (SchoolCode, SchoolName, ContactNumber, Email, AddressLine1, Area, City, StateId) VALUES('SSSVVHYD', 'Sri Sathya Sai Vidya Vihar',  '040-27408468', 'test@test.com', 'D D Colony, Bagh Amberpet', 'Amberpet', 'Hyderabad', 24);
INSERT INTO School (SchoolCode, SchoolName, ContactNumber, Email, AddressLine1, Area, City, StateId) VALUES('SSSHSSAP', 'Sri Sathya Sai Higher Secondary School',  '08555-289289', 'ssshss@gmail.com', 'P.O. Prasanthi Nilayam', 'District Anantapur', 'Anantapur', 1);

-- Subject
INSERT INTO Subject (SubjectCode, SubjectName) VALUES ('SC', 'Science');
INSERT INTO Subject (SubjectCode, SubjectName) VALUES ('MAT', 'Mathematics');
INSERT INTO Subject (SubjectCode, SubjectName) VALUES ('EVS', 'Environmental Studies');
INSERT INTO Subject (SubjectCode, SubjectName) VALUES ('HST', 'History');
INSERT INTO Subject (SubjectCode, SubjectName) VALUES ('GEO', 'Geography');

-- Class
INSERT INTO Class (ClassCode, ClassDescription) VALUES (1, 'Class 1');
INSERT INTO Class (ClassCode, ClassDescription) VALUES (2, 'Class 2');
INSERT INTO Class (ClassCode, ClassDescription) VALUES (3, 'Class 3');
INSERT INTO Class (ClassCode, ClassDescription) VALUES (4, 'Class 4');
INSERT INTO Class (ClassCode, ClassDescription) VALUES (5, 'Class 5');

-- --------------------------------------------SCHOOL TABLES END --------------------------------------------------

-- --------------------------------------------USER TABLES START --------------------------------------------------

-- UserAccount
INSERT INTO UserAccount (UserId, Username, PasswordSalt, PasswordHash, IsActive, IsRegistered, ActivationEmailCount, FailedLoginAttempt, LastFailedLoginAttempt, LockExpiry, Token, TokenExpiry) VALUES ('a4c4dad9-cbff-11ea-b7d6-448a5b2c2d83', 'vvadmin@yopmail.com', 'ec31c075-7803-44b3-8e9f-409af40d37bf', 'VGVzdEAxMjM0ZWMzMWMwNzUtNzgwMy00NGIzLThlOWYtNDA5YWY0MGQzN2Jm', 1, 1, 1, 0, NULL, NULL, NULL, NULL);
INSERT INTO UserAccount (UserId, Username, PasswordSalt, PasswordHash, IsActive, IsRegistered, ActivationEmailCount, FailedLoginAttempt, LastFailedLoginAttempt, LockExpiry, Token, TokenExpiry) VALUES ('c064c9df-cbff-11ea-b7d6-448a5b2c2d83', 'vvcore@yopmail.com', 'ec31c075-7803-44b3-8e9f-409af40d37bf', 'VGVzdEAxMjM0ZWMzMWMwNzUtNzgwMy00NGIzLThlOWYtNDA5YWY0MGQzN2Jm', 1, 1, 1, 0, NULL, NULL, NULL, NULL);
INSERT INTO UserAccount (UserId, Username, PasswordSalt, PasswordHash, IsActive, IsRegistered, ActivationEmailCount, FailedLoginAttempt, LastFailedLoginAttempt, LockExpiry, Token, TokenExpiry) VALUES ('d7b270c2-7718-11ea-8365-e4e749a19355', 'vvmentor@yopmail.com', 'ec31c075-7803-44b3-8e9f-409af40d37bf', 'VGVzdEAxMjM0ZWMzMWMwNzUtNzgwMy00NGIzLThlOWYtNDA5YWY0MGQzN2Jm', 1, 1, 1, 0, NULL, NULL, NULL, NULL);
INSERT INTO UserAccount (UserId, Username, PasswordSalt, PasswordHash, IsActive, IsRegistered, ActivationEmailCount, FailedLoginAttempt, LastFailedLoginAttempt, LockExpiry, Token, TokenExpiry) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 'vvteacher@yopmail.com', 'ec31c075-7803-44b3-8e9f-409af40d37bf', 'VGVzdEAxMjM0ZWMzMWMwNzUtNzgwMy00NGIzLThlOWYtNDA5YWY0MGQzN2Jm', 1, 1, 1, 0, NULL, NULL, NULL, NULL);

-- UserProfile
INSERT INTO UserProfile (UserID, Name, GenderId, MobileNumber, Email, QualificationId, OtherQualification, City, StateId) VALUES ('a4c4dad9-cbff-11ea-b7d6-448a5b2c2d83', 'Administrator', 1, NULL, 'vvadmin@yopmail.com', 2, NULL, 'Hyderabad', 24);
INSERT INTO UserProfile (UserID, Name, GenderId, MobileNumber, Email, QualificationId, OtherQualification, City, StateId) VALUES ('c064c9df-cbff-11ea-b7d6-448a5b2c2d83', 'Core Team', NULL, NULL, 'vvcore@yopmail.com', NULL, NULL, NULL, NULL);
INSERT INTO UserProfile (UserID, Name, GenderId, MobileNumber, Email, QualificationId, OtherQualification, City, StateId) VALUES ('d7b270c2-7718-11ea-8365-e4e749a19355', 'Mentor Test', 1, NULL, 'vvmentor@yopmail.com', 2, NULL, 'Hyderabad', 24);
INSERT INTO UserProfile (UserID, Name, GenderId, MobileNumber, Email, QualificationId, OtherQualification, City, StateId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 'Teacher Test', 1, '9999999999', 'vvteacher@yopmail.com', 1, NULL, 'Hyderabad', 24);

-- UserRole
INSERT INTO UserRole (UserId, RoleId) VALUES ('a4c4dad9-cbff-11ea-b7d6-448a5b2c2d83', 1);
INSERT INTO UserRole (UserId, RoleId) VALUES ('c064c9df-cbff-11ea-b7d6-448a5b2c2d83', 2);
INSERT INTO UserRole (UserId, RoleId) VALUES ('d7b270c2-7718-11ea-8365-e4e749a19355', 4);
INSERT INTO UserRole (UserId, RoleId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 5);

-- UserLanguage
INSERT INTO UserLanguage (UserId, LanguageId) VALUES ('d7b270c2-7718-11ea-8365-e4e749a19355', 2);
INSERT INTO UserLanguage (UserId, LanguageId) VALUES ('d7b270c2-7718-11ea-8365-e4e749a19355', 4);
INSERT INTO UserLanguage (UserId, LanguageId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 2);
INSERT INTO UserLanguage (UserId, LanguageId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 3);
INSERT INTO UserLanguage (UserId, LanguageId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 5);
INSERT INTO UserLanguage (UserId, LanguageId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 7);

-- --------------------------------------------USER TABLES END --------------------------------------------------

-- --------------------------------------------TEACHER TABLES START --------------------------------------------------

-- Teacher
INSERT INTO Teacher (TeacherId, SchoolId, ActiveLessonSetId, LoadNextLessonSet,	RedoActiveLessonSet) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 2, 'dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1', 0, 0);

-- TeacherSubject
INSERT INTO TeacherSubject (TeacherId, SubjectId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 2);
INSERT INTO TeacherSubject (TeacherId, SubjectId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 4);
INSERT INTO TeacherSubject (TeacherId, SubjectId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 5);

-- TeacherClass
INSERT INTO TeacherClass (TeacherId, ClassId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 2);
INSERT INTO TeacherClass (TeacherId, ClassId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 4);
INSERT INTO TeacherClass (TeacherId, ClassId) VALUES ('e3b591c7-7718-11ea-8365-e4e749a19355', 5);


-- --------------------------------------------TEACHER TABLES END --------------------------------------------------

-- --------------------------------------------MENTOR TABLES START --------------------------------------------------
INSERT INTO Mentor (MentorId, WorkingInSSSVV, SSSVVVoluteerName, WorkingInSaiOrganization, SaiOrganizationVoluteerName, EnglishTeachingExperience, Occupation, TimeCapacity, TeachersCapacity) VALUES ('d7b270c2-7718-11ea-8365-e4e749a19355', 1, 'Sssvv Volunteer', 1, 'Sai Volunteer', 'Basic English Experience', 'Teacher', 5, 5);

-- --------------------------------------------MENTOR TABLES END --------------------------------------------------
