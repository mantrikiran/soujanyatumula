/*Media type table data*/
INSERT INTO MediaType  (MediaTypeDescription) VALUES ('File System');
INSERT INTO MediaType  (MediaTypeDescription) VALUES ('Youtube');
INSERT INTO MediaType  (MediaTypeDescription) VALUES ('Image');

/*Section type table data*/
INSERT INTO SectionType (SectionTypeCode, SectionTypeDescription) VALUES ('LISTEN_KEENLY', 'Listen Keenly');
INSERT INTO SectionType (SectionTypeCode, SectionTypeDescription) VALUES ('SPEAK_WELL', 'Speak Well');
INSERT INTO SectionType (SectionTypeCode, SectionTypeDescription) VALUES ('READ_ALOUD', 'Read Aloud');
INSERT INTO SectionType (SectionTypeCode, SectionTypeDescription) VALUES ('WRITE_RIGHT', 'Write Right');
INSERT INTO SectionType (SectionTypeCode, SectionTypeDescription) VALUES ('ASSESSMENT', 'Assessment');

/*Question type table data*/
INSERT INTO QuestionType  (QuestionTypeDescription) VALUES ('Multi-Choice with one answer');
INSERT INTO QuestionType  (QuestionTypeDescription) VALUES ('Multi-Choice with multiple answer no sequence');
INSERT INTO QuestionType  (QuestionTypeDescription) VALUES ('Multi-Choice with multiple answer strict sequence');
INSERT INTO QuestionType  (QuestionTypeDescription) VALUES ('Single question with multiple answer strict sequence');

/*Media table data*/
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('14db90d4-bf80-11ea-a2cd-08606e6ce1c1', 2, 'https://www.youtube.com/embed/zTTmU58JWLk');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('f4a26822-cc22-11ea-b7d6-448a5b2c2d83', 2, 'https://www.youtube.com/embed/90WhUCJCpak');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('f4c03c0d-cc22-11ea-b7d6-448a5b2c2d83', 2, 'https://www.youtube.com/embed/PCsgp5VSeN8');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('02e956d5-cc23-11ea-b7d6-448a5b2c2d83', 2, 'https://www.youtube.com/embed/1LhsEb3v-Rc');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('0308c533-cc23-11ea-b7d6-448a5b2c2d83', 2, 'https://www.youtube.com/embed/Vo-FKhw4CPY');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('ca6eb941-0c50-4c3c-a936-c8d9c17b92d0', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/sample1.mp3');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('da3be5e6-81bd-4b3d-baa8-02d1b05b8f5b', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/sample2.mp3');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('77ef1f45-3166-457e-87b5-d6ac67272d90', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Hint1.PNG');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('eea62aae-26fe-42ed-b408-c83a71a308aa', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_1.mp3');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('b6e14aee-c5ba-422e-bb9d-3ae911ce4fb3', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_2.mp3');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('006d2008-a2a3-44c1-87e2-e720b06bde6b', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_3.mp3');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('4e290b4e-bf34-4787-a35d-3de74da2f155', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_4.mp3');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('d5535a82-875d-4142-80a0-bc51f5b05f62', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_5.mp3');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('67983700-0628-4aef-a58d-07ea705d3cf4', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_ hindi_audio_1.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('72df84cb-ade4-4a26-bb79-3b895edb64d5', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_ hindi_audio_2.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('a6052efd-9e54-4f02-b9bf-88e803cb4f22', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_ hindi_audio_3.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('fd9a09dc-6180-461e-b907-03b02dd63163', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_ hindi_audio_4.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('7b9b4ede-60c8-4b9a-aa35-e7c88db435f0', 1, 'Media/Dashboard/9b5d13e7-b592-11ea-a2cd-08606e6ce1c1/Speak_Well_Audio_hindi_audio_5.m4a'); 
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('743c8521-1779-45fe-b0c6-6fe92311e0e8', 1, 'Media/Dashboard/86a5d93d-b592-11ea-a2cd-08606e6ce1c1/the-nice-london-bus-driver.mp3');
/*New question*/
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('4494738d-88f1-4808-a9fd-7f6b588af904', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3e1.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('933afb29-31ef-4259-8d04-093a2ceba47a', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3h1.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('79f71043-dd3f-48fa-94df-bd9fb7ac3699', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3e2.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('2d0bfb3d-e645-4623-bc0d-57c4e8514604', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3h2.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('f8bb80e2-91b8-4aaa-bd8e-72d88ec69d14', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3e3.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('427d1535-052c-4349-8893-bb6aaa0d5fbb', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3h3.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('13820f30-5146-49dd-84b3-471e305b2e09', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3e4.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('8f3d0d22-cd90-4338-acba-d6157db75202', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3h4.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('287df25a-cefd-4bb9-8f38-7d770b09cb34', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3e5.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('a5a97ca7-7fa0-4475-a145-36002c4bde9d', 1, 'Media/Dashboard/a393765c-4947-4d8c-be13-49587b22c42b/SW3h5.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('606ee2fb-ff3a-4690-83a5-3c33360b2a38', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWe1.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('cef9772b-79d3-41e5-96a9-12fc89d7ca18', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWh1.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('899c557f-880e-4862-ad2b-d6a9544e1e5c', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWe2.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('2f17cc9b-4416-4cd7-8fb9-106398550d21', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWh2.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('7d5814a3-4b31-4c2d-811d-613a5ef9adc3', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWe3.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('ee007507-06b2-4f5f-9dc1-a1581a28132f', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWh3.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('207e0b51-e4e3-440e-af9e-59e79ce701b3', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWe4.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('6e05824b-9920-4208-8b59-9939d8fd934a', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWh4.m4a');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('28348b4c-c4a5-4cd0-b516-0350d932b54b', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWe5.m4a'); 
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('30c34b66-0fa2-4358-8592-632b9c27da18', 1, 'Media/Dashboard/0854ae85-da74-49dc-ab96-5f60d864d0df/SWh5.m4a'); 
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('2545e82b-c986-4f9f-9f76-0ca5366b61f2', 1, 'Media/Dashboard/336267d1-65d7-4ff4-bcda-de9f8c108a8e/RA1Myfamily.mpeg');
INSERT INTO Media  (MediaId, MediaTypeId, MediaSource) VALUES ('0e78203a-7f48-430d-8211-19ec92e278ae', 1, 'Media/Dashboard/03af4b8b-38f4-49ed-969a-a3dba1ae7c5b/RA3TheFriendlyPostman.mpeg');
/*Level table data*/
INSERT INTO Level  (LevelId, LevelCode, LevelDescription) VALUES
(1,'BASIC','Basic Level');
INSERT INTO Level  (LevelId, LevelCode, LevelDescription) VALUES
(2,'INTERMEDIATE','Intermediate Level');
INSERT INTO Level  (LevelId, LevelCode, LevelDescription) VALUES
(3,'EXPERT','Expert Level');

/*Lesson Set table data*/
INSERT INTO LessonSet (LessonSetId,	LessonSetOrder,	LevelId) VALUES ('dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1', 1, 1);
INSERT INTO LessonSet (LessonSetId,	LessonSetOrder,	LevelId) VALUES ('e33aab39-cc1a-11ea-a2cd-08606e6ce1c1', 2, 1);
INSERT INTO LessonSet (LessonSetId,	LessonSetOrder,	LevelId) VALUES ('2533e13c-cc1d-11ea-a2cd-08606e6ce1c1', 3, 1);

/*lesson table data*/
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('e4d9286d-b590-11ea-a2cd-08606e6ce1c1',1,'lesson 1','lesson 1', 'BAS_LES_1', 'dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('96f2d754-b591-11ea-a2cd-08606e6ce1c1',2,'lesson 2','lesson 2', 'BAS_LES_2', 'dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('d44be2f1-b591-11ea-a2cd-08606e6ce1c1',3,'lesson 3','lesson 4', 'BAS_LES_3', 'dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('e6225a61-b591-11ea-a2cd-08606e6ce1c1',4,'lesson 4','lesson 4', 'BAS_LES_4', 'dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('6db95db1-b592-11ea-a2cd-08606e6ce1c1',5,'lesson 5','lesson 5', 'BAS_LES_5', 'dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('6e7dbdeb-90c8-48bb-9208-4ed674764894', -1,'Assessment','Assessment', 'ASSESSMENT_1', 'dcb21bb2-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('f0970c28-b591-11ea-a2cd-08606e6ce1c1',6,'lesson 6','lesson 6', 'BAS_LES_6', 'e33aab39-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('fbc9299f-b591-11ea-a2cd-08606e6ce1c1',7,'lesson 7','lesson 7', 'BAS_LES_7', 'e33aab39-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('033c6db7-b592-11ea-a2cd-08606e6ce1c1',8,'lesson 8','lesson 8', 'BAS_LES_8', 'e33aab39-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('0d08f45f-b592-11ea-a2cd-08606e6ce1c1',9,'lesson 9','lesson 9', 'BAS_LES_9', 'e33aab39-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('1a0c8507-b592-11ea-a2cd-08606e6ce1c1',10,'lesson 10','lesson 10', 'BAS_LES_10', 'e33aab39-cc1a-11ea-a2cd-08606e6ce1c1');
INSERT INTO Lesson (LessonId, LessonNumber, LessonName, LessonDescription, LessonCode, LessonSetId)
VALUES ('2b8584bb-b592-11ea-a2cd-08606e6ce1c1', -1,'Assessment','Assessment', 'ASSESSMENT_2', 'e33aab39-cc1a-11ea-a2cd-08606e6ce1c1');

/*lesson Section table data*/
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)
VALUES ('86a5d93d-b592-11ea-a2cd-08606e6ce1c1','e4d9286d-b590-11ea-a2cd-08606e6ce1c1','The Nice London Bus Driver','Listen Keenly', 1);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)
VALUES ('9b5d13e7-b592-11ea-a2cd-08606e6ce1c1','e4d9286d-b590-11ea-a2cd-08606e6ce1c1','Conversation Practice','Speak Well', 2);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)
VALUES ('a355a425-b592-11ea-a2cd-08606e6ce1c1','e4d9286d-b590-11ea-a2cd-08606e6ce1c1','A True Friend','Read Aloud', 3);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)
VALUES ('abf8ccb3-b592-11ea-a2cd-08606e6ce1c1','e4d9286d-b590-11ea-a2cd-08606e6ce1c1','Adjectives','Write Right', 4);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)
VALUES ('b5807a2f-b592-11ea-a2cd-08606e6ce1c1','6e7dbdeb-90c8-48bb-9208-4ed674764894','Assessment','Assessment', 5);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('4593096a-9c7e-4bc1-b2bd-f1ea159670e2','96f2d754-b591-11ea-a2cd-08606e6ce1c1','The Great Virtue of Forbearance Value Cartoons','Listen Keenly', 1);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('a393765c-4947-4d8c-be13-49587b22c42b','96f2d754-b591-11ea-a2cd-08606e6ce1c1','Conversation Practice','Speak Well', 2);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('03af4b8b-38f4-49ed-969a-a3dba1ae7c5b','96f2d754-b591-11ea-a2cd-08606e6ce1c1','Our Neighbourhood -The Friendly Postman','Read Aloud', 3);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('c8bad747-f71e-4a6e-8ff9-1d29702c3fad','96f2d754-b591-11ea-a2cd-08606e6ce1c1','Adjectives','Write Right', 4);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('4718f6fc-2844-4c7d-9248-a3fb04567aff','d44be2f1-b591-11ea-a2cd-08606e6ce1c1','The Great Virtue of Forbearance','Listen Keenly', 1);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('0854ae85-da74-49dc-ab96-5f60d864d0df','d44be2f1-b591-11ea-a2cd-08606e6ce1c1','Conversation Practice','Speak Well', 2);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('336267d1-65d7-4ff4-bcda-de9f8c108a8e','d44be2f1-b591-11ea-a2cd-08606e6ce1c1','My Family','Read Aloud', 3);
INSERT INTO LessonSection (LessonSectionId, LessonId, LessonSectionName, LessonSectionDescription, SectionTypeId)VALUES ('5c1df251-8d25-4a94-988f-6c17986abdd4','d44be2f1-b591-11ea-a2cd-08606e6ce1c1','Adjectives','Write Right', 4);

/*Instruction table data*/
INSERT INTO Instruction (LessonSectionId, InstructionDescription) VALUES ('86a5d93d-b592-11ea-a2cd-08606e6ce1c1', 'The Nice London Bus Driver Instruction');
INSERT INTO Instruction (LessonSectionId, InstructionDescription) VALUES ('9b5d13e7-b592-11ea-a2cd-08606e6ce1c1', 'Conversation Practice Instruction');
INSERT INTO Instruction (LessonSectionId, InstructionDescription) VALUES ('a355a425-b592-11ea-a2cd-08606e6ce1c1', 'A True Friend Instruction');
INSERT INTO Instruction (LessonSectionId, InstructionDescription) VALUES ('abf8ccb3-b592-11ea-a2cd-08606e6ce1c1', 'Adjectives Instruction');
INSERT INTO Instruction (LessonSectionId, InstructionDescription) VALUES ('b5807a2f-b592-11ea-a2cd-08606e6ce1c1', 'Assessment Instruction');

/*Instruction Media table data*/
INSERT INTO InstructionMedia (InstructionId, LanguageId, MediaId, MediaDescription) VALUES (1, 1, '14db90d4-bf80-11ea-a2cd-08606e6ce1c1', 'The Nice London Bus Driver Instruction');
INSERT INTO InstructionMedia (InstructionId, LanguageId, MediaId, MediaDescription) VALUES (2, 1, 'f4a26822-cc22-11ea-b7d6-448a5b2c2d83', 'Conversation Practice Instruction');
INSERT INTO InstructionMedia (InstructionId, LanguageId, MediaId, MediaDescription) VALUES (3, 1, 'f4c03c0d-cc22-11ea-b7d6-448a5b2c2d83', 'A True Friend Instruction');
INSERT INTO InstructionMedia (InstructionId, LanguageId, MediaId, MediaDescription) VALUES (4, 1, '02e956d5-cc23-11ea-b7d6-448a5b2c2d83', 'Adjectives Instruction');
INSERT INTO InstructionMedia (InstructionId, LanguageId, MediaId, MediaDescription) VALUES (5, 1, '0308c533-cc23-11ea-b7d6-448a5b2c2d83', 'Assessment Instruction');

/*Question  table data*/
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('dec15b89-c07e-11ea-a2cd-08606e6ce1c1', '9b5d13e7-b592-11ea-a2cd-08606e6ce1c1', '1','Who is your favourite friend?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('97a5b8e3-a0fc-4d20-b7b0-33438d32e690', '9b5d13e7-b592-11ea-a2cd-08606e6ce1c1', '2','Do you have many friends?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('e949e309-238f-4f5d-839b-bbf43ac94ed2', '9b5d13e7-b592-11ea-a2cd-08606e6ce1c1', '3','Are you in touch with your childhood friends?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('bbc43610-e042-40f2-878c-e5034c3dc31e', '9b5d13e7-b592-11ea-a2cd-08606e6ce1c1', '4','Who was your best friend in school?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('6efd39ee-e0c9-4e8d-a74c-757fc94a9a25', '9b5d13e7-b592-11ea-a2cd-08606e6ce1c1', '5','What did you like about him/her?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText, RecommendedAttempts) VALUES ('fc6e72a3-a732-4bb5-82bf-5cd6b144f138', '86a5d93d-b592-11ea-a2cd-08606e6ce1c1', 1, 'Take the Quiz', 6);
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('f9bb5275-824e-4669-ae63-c07da26331d3', 'a355a425-b592-11ea-a2cd-08606e6ce1c1', '1','One day an elephant was wandering in the forest.He was feeling so lonely and wanted to have friends. He was so <b>huge</b>, so all the animals ran away from him. He saw a little monkey and asked, "Will you be my friend, monkey?""Oh.no, you are so big and you cannot swing on the trees like me. Sorry, I cannot be your friend", said the monkey.\r\nThe elephant felt very <b>sad</b>. Just then he came across a tiny rabbit and asked her if she would be his friend. "You are so <b>big</b>; I am so <b>small</b>. We cannot be friends. Go away elephant", she said.\r\nFeeling <b>tired</b> and <b>unhappy</b>, the elephant walked along. He met a frog with <b>large</b> eyes and asked if she could be her friend. The frog said "You are too <b>big</b> and <b>heavy</b>.You cannot jump like me. I am sorry, but you cannot be my friend".\r\nNext, the elephant met a fox, and the <b>clever</b> fox also said the same thing, that he was too <b>big</b>. The next day, the elephant saw all the animals in the forest running in fear. The elephant was curious. He stoppped a bear and asked what was happening and why were all the animals running in fear. The <b>black</b> bear told him that a <b>fierce</b> tiger was attacking all the animals in the forest.\r\nThe elephant wanted to save the other <b>weak</b> animals. Therefore, he went to the tiger and said "Please tiger, leave my friends alone. Do not eat them". The tiger became angry and roared. "Go away, I am hungry and will eat what I want," said the tiger.\r\nThe elephant became angry too and kicked the tiger. It felt with a thud and looked at the <b>mighty</b> elephant with fear. The tiger took to his heels without looking back. All the other animals watched in fear and shouted, \'hurrah\' when the tiger ran away. "Size does not matter, you are our friend", "you are just the right size to be our friend," they said.');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText, RecommendedAttempts, SecondaryAttempts) VALUES ('5400659d-60dd-4850-856d-6a628d63e7d7', 'abf8ccb3-b592-11ea-a2cd-08606e6ce1c1', 1, 'Unscramble the adjectives', 6, 3);
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText, RecommendedAttempts, SecondaryAttempts) VALUES ('c4034094-9f9c-40a6-ba09-c52cdc7561dd', 'abf8ccb3-b592-11ea-a2cd-08606e6ce1c1', 1, 'Pick out the adjectives from the list below', 6, 3);
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText, RecommendedAttempts, SecondaryAttempts) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 'b5807a2f-b592-11ea-a2cd-08606e6ce1c1', '1','Fill in the blanks by dragging and dropping the appropriate adjective given in the box', 1, 1);

INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('303f758a-d61d-4ec0-bccf-80e23ed5e6ec', 'a393765c-4947-4d8c-be13-49587b22c42b', '6','Who are your neighbours?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('5dca9fbb-4d42-4e48-ba06-18ff5a2932c2', 'a393765c-4947-4d8c-be13-49587b22c42b', '7','Name some of the people you can find in your neighbourhood.');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('32ff9b36-b544-4edd-a900-aa8f64a3c610', 'a393765c-4947-4d8c-be13-49587b22c42b', '8','How do they help you?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('8ede869b-8b55-41a3-9372-64e676e1430e', 'a393765c-4947-4d8c-be13-49587b22c42b', '9','What do you think of neighbours in cities?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('9058cb55-51c3-41ed-bf48-19f0d584043a', 'a393765c-4947-4d8c-be13-49587b22c42b', '10','Describe life in a city and compare it with life in a small town or village.');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('a379a9ef-ebe9-4359-9183-a1689d049441', '0854ae85-da74-49dc-ab96-5f60d864d0df', '11','What is the meaning of a family?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('edec6ba9-3bf2-4ea6-9912-d08b268642bd', '0854ae85-da74-49dc-ab96-5f60d864d0df', '12','Who are there in your family?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('f9bdbfc4-ca27-479c-80f4-89b0532f7a74', '0854ae85-da74-49dc-ab96-5f60d864d0df', '13','What kind of a family is common in cities?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('4f392ae8-a675-48ab-8e72-d54f33d8cd16', '0854ae85-da74-49dc-ab96-5f60d864d0df', '14','What kind of families can we see in villages?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('9910ade8-f090-4187-8834-e83100ba51ac', '0854ae85-da74-49dc-ab96-5f60d864d0df', '15','What role does a family play in society?');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('73fa372a-bfa0-4d8f-b0f2-bfec9b3746b7', '336267d1-65d7-4ff4-bcda-de9f8c108a8e', '1','A family is a group of two, three or more persons, all related to one another, living together in one home. There are two kinds of families. One is a nuclear family, in which we can see a father, mother and children. This sort of family is common especially in cities nowadays. In a joint family, there are grandparents, uncles, aunts, cousins, nephews and nieces, all living together. In cities, in a nuclear family, both father and mother go to the office, and the child stays with the maidservant. As the parents are busy with their office life, they are compelled to leave the child with a house help or in a crèche. In a joint family, even if parents go to work, the grandparents take care of the children. They teach them right conduct and good things about life and narrate value based stories. Healthy family relationships help in promoting good habits, culture and traditions in the children. A healthy family plays a great role in moulding children and preparing them to become good citizens useful for the community and the nation.');
INSERT INTO Question  (QuestionId,LessonSectionId,QuestionOrder,QuestionText) VALUES ('564e32a0-161b-47c9-a895-14bd78316e18', '03af4b8b-38f4-49ed-969a-a3dba1ae7c5b', '1','Gopu is a postman, a public servant. He is very useful to society. He delivers mails, money orders, invitations and parcels to us. He wears a khaki uniform. He carries a bag across his shoulder. He keeps all the letters and parcels inside it. He gets up very early in the morning. At first, he goes to the post-office. After collecting letters from there he delivers those letters to us. He goes door to door and delivers the mail. He brings happy news as well as sad news. Every day, he takes two rounds in his area. Whether it rains or it is very hot, he does the work regularly and he is very punctual. We wait for him eagerly every day. He brings news of our relatives and friends. Whenever required, he reads the letters to the family as in many cases the villagers are illiterate and look to the postman to read the letters and record their responses, a task he takes upon himself cheerfully. He is honest and hard working. We should be thankful to him for working so hard for us.');
/*Question Media table data*/


INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, 'dec15b89-c07e-11ea-a2cd-08606e6ce1c1','eea62aae-26fe-42ed-b408-c83a71a308aa');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '97a5b8e3-a0fc-4d20-b7b0-33438d32e690','b6e14aee-c5ba-422e-bb9d-3ae911ce4fb3');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, 'e949e309-238f-4f5d-839b-bbf43ac94ed2','006d2008-a2a3-44c1-87e2-e720b06bde6b');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, 'bbc43610-e042-40f2-878c-e5034c3dc31e','4e290b4e-bf34-4787-a35d-3de74da2f155');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '6efd39ee-e0c9-4e8d-a74c-757fc94a9a25','d5535a82-875d-4142-80a0-bc51f5b05f62');

INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, 'dec15b89-c07e-11ea-a2cd-08606e6ce1c1','67983700-0628-4aef-a58d-07ea705d3cf4');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '97a5b8e3-a0fc-4d20-b7b0-33438d32e690','72df84cb-ade4-4a26-bb79-3b895edb64d5');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, 'e949e309-238f-4f5d-839b-bbf43ac94ed2','a6052efd-9e54-4f02-b9bf-88e803cb4f22');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, 'bbc43610-e042-40f2-878c-e5034c3dc31e','fd9a09dc-6180-461e-b907-03b02dd63163');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '6efd39ee-e0c9-4e8d-a74c-757fc94a9a25','7b9b4ede-60c8-4b9a-aa35-e7c88db435f0');

INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, 'fc6e72a3-a732-4bb5-82bf-5cd6b144f138','743c8521-1779-45fe-b0c6-6fe92311e0e8');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '303f758a-d61d-4ec0-bccf-80e23ed5e6ec','4494738d-88f1-4808-a9fd-7f6b588af904');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '303f758a-d61d-4ec0-bccf-80e23ed5e6ec','933afb29-31ef-4259-8d04-093a2ceba47a');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '5dca9fbb-4d42-4e48-ba06-18ff5a2932c2','79f71043-dd3f-48fa-94df-bd9fb7ac3699');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '5dca9fbb-4d42-4e48-ba06-18ff5a2932c2','2d0bfb3d-e645-4623-bc0d-57c4e8514604');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '32ff9b36-b544-4edd-a900-aa8f64a3c610','f8bb80e2-91b8-4aaa-bd8e-72d88ec69d14');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '32ff9b36-b544-4edd-a900-aa8f64a3c610','427d1535-052c-4349-8893-bb6aaa0d5fbb');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '8ede869b-8b55-41a3-9372-64e676e1430e','13820f30-5146-49dd-84b3-471e305b2e09');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '8ede869b-8b55-41a3-9372-64e676e1430e','8f3d0d22-cd90-4338-acba-d6157db75202');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '9058cb55-51c3-41ed-bf48-19f0d584043a','287df25a-cefd-4bb9-8f38-7d770b09cb34');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '9058cb55-51c3-41ed-bf48-19f0d584043a','a5a97ca7-7fa0-4475-a145-36002c4bde9d');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, 'a379a9ef-ebe9-4359-9183-a1689d049441','606ee2fb-ff3a-4690-83a5-3c33360b2a38');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, 'a379a9ef-ebe9-4359-9183-a1689d049441','cef9772b-79d3-41e5-96a9-12fc89d7ca18');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, 'edec6ba9-3bf2-4ea6-9912-d08b268642bd','899c557f-880e-4862-ad2b-d6a9544e1e5c');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, 'edec6ba9-3bf2-4ea6-9912-d08b268642bd','2f17cc9b-4416-4cd7-8fb9-106398550d21');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, 'f9bdbfc4-ca27-479c-80f4-89b0532f7a74','7d5814a3-4b31-4c2d-811d-613a5ef9adc3');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, 'f9bdbfc4-ca27-479c-80f4-89b0532f7a74','ee007507-06b2-4f5f-9dc1-a1581a28132f');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '4f392ae8-a675-48ab-8e72-d54f33d8cd16','207e0b51-e4e3-440e-af9e-59e79ce701b3');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '4f392ae8-a675-48ab-8e72-d54f33d8cd16','6e05824b-9920-4208-8b59-9939d8fd934a');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '9910ade8-f090-4187-8834-e83100ba51ac','28348b4c-c4a5-4cd0-b516-0350d932b54b');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (2, '9910ade8-f090-4187-8834-e83100ba51ac','30c34b66-0fa2-4358-8592-632b9c27da18');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '73fa372a-bfa0-4d8f-b0f2-bfec9b3746b7','2545e82b-c986-4f9f-9f76-0ca5366b61f2');
INSERT INTO QuestionMedia  (LanguageId, QuestionId,MediaId) VALUES (1, '564e32a0-161b-47c9-a895-14bd78316e18','0e78203a-7f48-430d-8211-19ec92e278ae');
/*Question Hint table data*/
INSERT INTO QuestionHint  (QuestionId,HintText,MediaId) VALUES ('dec15b89-c07e-11ea-a2cd-08606e6ce1c1','A greengrocer is a shopkeeper who sells fruits and vegetables.','77ef1f45-3166-457e-87b5-d6ac67272d90');
INSERT INTO QuestionHint  (QuestionId,HintText,MediaId) VALUES ('dec15b89-c07e-11ea-a2cd-08606e6ce1c1','A vendor is someone who sells things.','77ef1f45-3166-457e-87b5-d6ac67272d90');

/*SubQuestion table data*/
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('fc6e72a3-a732-4bb5-82bf-5cd6b144f138', 1, 1, 'Which city did the businessman visit?');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('fc6e72a3-a732-4bb5-82bf-5cd6b144f138', 1, 2, 'What did he realize?');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('fc6e72a3-a732-4bb5-82bf-5cd6b144f138', 1, 3, 'What mode of transport did he use?');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('fc6e72a3-a732-4bb5-82bf-5cd6b144f138', 1, 4, 'How did the driver welcome the passengers?');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('fc6e72a3-a732-4bb5-82bf-5cd6b144f138', 1, 5, 'How did the driver greet the passengers?');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('fc6e72a3-a732-4bb5-82bf-5cd6b144f138', 1, 6, 'When the passengers got off, how would he greet them?');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('5400659d-60dd-4850-856d-6a628d63e7d7', 3, 1, 'Unscramble the adjectives');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('5400659d-60dd-4850-856d-6a628d63e7d7', 3, 2, 'Unscramble the adjectives');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('5400659d-60dd-4850-856d-6a628d63e7d7', 3, 3, 'Unscramble the adjectives');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('5400659d-60dd-4850-856d-6a628d63e7d7', 3, 4, 'Unscramble the adjectives');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('5400659d-60dd-4850-856d-6a628d63e7d7', 3, 5, 'Unscramble the adjectives');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('5400659d-60dd-4850-856d-6a628d63e7d7', 3, 6, 'Unscramble the adjectives');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('c4034094-9f9c-40a6-ba09-c52cdc7561dd', 2, 1, 'Pick out the adjectives from the list below');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 1, 'The elephant is <input> but the moneky is <input>');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 2, 'The box is <input> but the feather is <input>');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 3, 'The rabbit is <input> but the tiger is <input>');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 4, 'He was <input> to the office but <input> to the cinema');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 5, 'She came <input> in class but <input> in games');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 6, 'The monster is <input> but the ant is <input>');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 7, 'The building is <input> but the ceiling is <input>');
INSERT INTO SubQuestion (QuestionId, QuestionTypeId, QuestionOrder, QuestionText) VALUES ('d48f0b52-7a81-44f6-ba8d-fd22b6fffda7', 4, 8, 'The demon was <input> but the saint was <input>');

/*SubQuestionOption table data*/
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (1, 'Paris', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (1, 'London', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (1, 'New York', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (1, 'Tokyo', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (2, 'He had lost his wallet', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (2, 'He had lost his car keys', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (2, 'He had lost his hat', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (2, 'He had lost his bag', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (3, 'Train', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (3, 'Aeroplane', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (3, 'Metro', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (3, 'Bus', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (4, 'Sadly', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (4, 'Cheerfully', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (4, 'Grumpily', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (4, 'Briskly', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (5, 'Good morning', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (5, 'Welcome', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (5, 'Hi! How are you?', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (5, 'Good day', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (6, 'Bye, have a great day', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (6, 'See you later', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (6, 'Good day', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (6, 'Come back soon', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (7, 'I', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (7, 'B', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (7, 'G', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (8, 'H', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (8, 'T', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (8, 'O', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (9, 'T', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (9, 'F', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (9, 'S', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (9, 'A', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (10, 'E', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (10, 'W', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (10, 'N', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (11, 'A', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (11, 'H', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (11, 'C', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (11, 'P', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (11, 'E', 5);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (12, 'L', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (12, 'A', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (12, 'E', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (12, 'T', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Good', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'New', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Student', 3);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Book', 4);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Dress', 5);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Five', 6);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Gold', 7);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Bangle', 8);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Thin', 9);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (13, 'Fat', 10); -- 56
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (14, 'big', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (14, 'small', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (15, 'heavy', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (15, 'light', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (16, 'weak', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (16, 'strong', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (17, 'late', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (17, 'early', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (18, 'last', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (18, 'first', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (19, 'huge', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (19, 'tiny', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (20, 'high', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (20, 'low', 2);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (21, 'cruel', 1);
INSERT INTO SubQuestionOption (SubQuestionId, OptionText, OptionOrder) VALUES (21, 'kind', 2); -- 72


/*SubQuestionAnswer table data*/
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (1, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (2, 6);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (3, 12);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (4, 14);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (5, 19);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (6, 21);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (7, 26, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (7, 25, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (7, 27, 3);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (8, 28, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (8, 30, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (8, 29, 3);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (9, 32, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (9, 34, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (9, 33, 3);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (9, 31, 4);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (10, 37, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (10, 35, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (10, 36, 3);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (11, 40, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (11, 39, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (11, 42, 3);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (11, 38, 4);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (11, 41, 5);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (12, 43, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (12, 44, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (12, 46, 3);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (12, 45, 4);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (13, 47);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (13, 48);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (13, 52);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (13, 55);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId) VALUES (13, 56);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (14, 57, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (14, 58, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (15, 59, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (15, 60, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (16, 61, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (16, 62, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (17, 63, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (17, 64, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (18, 65, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (18, 66, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (19, 67, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (19, 68, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (20, 69, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (20, 70, 2);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (21, 71, 1);
INSERT INTO SubQuestionAnswer (SubQuestionId, SubQuestionOptionId, AnswerOrder) VALUES (21, 72, 2);