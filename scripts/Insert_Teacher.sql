-- INSERT A TEACHER

-- Set Details
SET @userId = (SELECT uuid());
SET @email = 'testteacher@yopmail.com';
SET @username = 'NAME';

-- User Account
INSERT INTO UserAccount 
(
UserId, 
Username,
IsActive, 
IsRegistered, 
ActivationEmailCount, 
FailedLoginAttempt) 
VALUES 
(
@userId, 
@email, 
0, 
0, 
0, 
0);

-- UserProfile
INSERT INTO UserProfile 
(
UserID, 
Name) 
VALUES 
(
@userId, 
@username, 
@email);

-- UserRole
INSERT INTO UserRole (UserId, RoleId) VALUES (@userId, 6);

-- Teacher
INSERT INTO Teacher (UserId, SchoolId) VALUES (@userId, 1);



SET @teacherId = (SELECT TeacherId FROM Teacher WHERE UserId=@userId);

SELECT * FROM UserAccount WHERE UserId=@userId;
SELECT * FROM UserProfile WHERE UserId=@userId;
SELECT * FROM UserLanguage WHERE UserId=@userId;

SELECT * FROM TeacherClass WHERE TeacherId=@teacherId;
SELECT * FROM TeacherSubject WHERE TeacherId=@teacherId;