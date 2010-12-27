USE [IUDICO]
GO

/*
   27 грудня 2010 р.3:29:40
   User: 
   Server: .\
   Database: OLOLO2
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CurriculumAssignments
	DROP CONSTRAINT FK_CurriculumAssignments_Curriculums
GO
ALTER TABLE dbo.Curriculums SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Curriculums', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Curriculums', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Curriculums', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CurriculumAssignments
	(
	Id int NOT NULL IDENTITY (1, 1),
	UserGroupRef int NOT NULL,
	CurriculumRef int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CurriculumAssignments SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CurriculumAssignments ON
GO
IF EXISTS(SELECT * FROM dbo.CurriculumAssignments)
	 EXEC('INSERT INTO dbo.Tmp_CurriculumAssignments (Id, UserGroupRef, CurriculumRef)
		SELECT Id, UserGroupRef, CurriculumRef FROM dbo.CurriculumAssignments WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CurriculumAssignments OFF
GO
ALTER TABLE dbo.Timeline
	DROP CONSTRAINT FK_Timeline_CurriculumAssignments1
GO
DROP TABLE dbo.CurriculumAssignments
GO
EXECUTE sp_rename N'dbo.Tmp_CurriculumAssignments', N'CurriculumAssignments', 'OBJECT' 
GO
ALTER TABLE dbo.CurriculumAssignments ADD CONSTRAINT
	PK_CurriculumAssignment PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CurriculumAssignments ADD CONSTRAINT
	FK_CurriculumAssignments_Curriculums FOREIGN KEY
	(
	Id
	) REFERENCES dbo.Curriculums
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Timeline ADD CONSTRAINT
	FK_Timeline_CurriculumAssignments1 FOREIGN KEY
	(
	Id
	) REFERENCES dbo.CurriculumAssignments
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Timeline SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'CONTROL') as Contr_Per 


/*
   27 грудня 2010 р.3:31:05
   User: 
   Server: .\
   Database: OLOLO2
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CurriculumAssignments
	DROP CONSTRAINT FK_CurriculumAssignments_Curriculums
GO
ALTER TABLE dbo.Curriculums SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Curriculums', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Curriculums', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Curriculums', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Timeline
	DROP CONSTRAINT FK_Timeline_CurriculumAssignments1
GO
ALTER TABLE dbo.CurriculumAssignments ADD
	IsDeleted bit NOT NULL CONSTRAINT DF_CurriculumAssignments_IsDeleted DEFAULT 0
GO
ALTER TABLE dbo.CurriculumAssignments ADD CONSTRAINT
	FK_CurriculumAssignments_Curriculums FOREIGN KEY
	(
	CurriculumRef
	) REFERENCES dbo.Curriculums
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CurriculumAssignments SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Timeline ADD CONSTRAINT
	FK_Timeline_CurriculumAssignments FOREIGN KEY
	(
	CurriculumAssignmentRef
	) REFERENCES dbo.CurriculumAssignments
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Timeline SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'CONTROL') as Contr_Per 


/*
   27 грудня 2010 р.3:34:21
   User: 
   Server: .\
   Database: OLOLO2
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Timeline
	DROP CONSTRAINT FK_Timeline_CurriculumAssignments
GO
ALTER TABLE dbo.CurriculumAssignments SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CurriculumAssignments', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Timeline
	(
	Id int NOT NULL IDENTITY (1, 1),
	StartDate datetime NOT NULL,
	EndDate datetime NOT NULL,
	CurriculumAssignmentRef int NOT NULL,
	OperationRef int NOT NULL,
	StageRef int NULL,
	IsDeleted bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Timeline SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Timeline ADD CONSTRAINT
	DF_Timeline_IsDeleted DEFAULT 0 FOR IsDeleted
GO
SET IDENTITY_INSERT dbo.Tmp_Timeline ON
GO
IF EXISTS(SELECT * FROM dbo.Timeline)
	 EXEC('INSERT INTO dbo.Tmp_Timeline (Id, StartDate, EndDate, CurriculumAssignmentRef, OperationRef, StageRef)
		SELECT Id, StartDate, EndDate, CurriculumAssignmentRef, OperationRef, StageRef FROM dbo.Timeline WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Timeline OFF
GO
ALTER TABLE dbo.Operation
	DROP CONSTRAINT FK_Operation_Timeline
GO
DROP TABLE dbo.Timeline
GO
EXECUTE sp_rename N'dbo.Tmp_Timeline', N'Timeline', 'OBJECT' 
GO
ALTER TABLE dbo.Timeline ADD CONSTRAINT
	PK_Timeline PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Timeline ADD CONSTRAINT
	FK_Timeline_CurriculumAssignments FOREIGN KEY
	(
	CurriculumAssignmentRef
	) REFERENCES dbo.CurriculumAssignments
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Timeline', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Timeline ADD CONSTRAINT
	FK_Timeline_Operation FOREIGN KEY
	(
	OperationRef
	) REFERENCES dbo.Operation
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Operation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Operation', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Operation', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Operation', 'Object', 'CONTROL') as Contr_Per 


/*
   27 грудня 2010 р.3:36:18
   User: 
   Server: .\
   Database: OLOLO2
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.ThemeType
	(
	Id int NOT NULL,
	Name nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ThemeType ADD CONSTRAINT
	PK_ThemeType PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ThemeType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'CONTROL') as Contr_Per 


/*
   27 грудня 2010 р.3:38:12
   User: 
   Server: .\
   Database: OLOLO2
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Themes
	DROP CONSTRAINT FK_Themes_Stages
GO
ALTER TABLE dbo.Stages SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Stages', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Stages', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Stages', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.ThemeType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Themes
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(50) NOT NULL,
	Created datetime NOT NULL,
	Updated datetime NOT NULL,
	StageRef int NOT NULL,
	CourseRef int NOT NULL,
	SortOrder int NOT NULL,
	ThemeTypeRef int NOT NULL,
	IsDeleted bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Themes SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Themes ON
GO
IF EXISTS(SELECT * FROM dbo.Themes)
	 EXEC('INSERT INTO dbo.Tmp_Themes (Id, Name, Created, Updated, StageRef, CourseRef, SortOrder, IsDeleted)
		SELECT Id, Name, Created, Updated, StageRef, CourseRef, SortOrder, IsDeleted FROM dbo.Themes WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Themes OFF
GO
DROP TABLE dbo.Themes
GO
EXECUTE sp_rename N'dbo.Tmp_Themes', N'Themes', 'OBJECT' 
GO
ALTER TABLE dbo.Themes ADD CONSTRAINT
	PK_Themes PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Themes ADD CONSTRAINT
	FK_Themes_Stages FOREIGN KEY
	(
	ThemeTypeRef
	) REFERENCES dbo.ThemeType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Themes', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Themes', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Themes', 'Object', 'CONTROL') as Contr_Per 


/****** Object:  Table [dbo].[ThemeType]    Script Date: 12/27/2010 03:44:57 ******/
DELETE FROM [dbo].[ThemeType]
GO
/****** Object:  Table [dbo].[ThemeType]    Script Date: 12/27/2010 03:44:57 ******/
INSERT [dbo].[ThemeType] ([Id], [Name]) VALUES (1, N'Test')
INSERT [dbo].[ThemeType] ([Id], [Name]) VALUES (2, N'Theory')


/*
   27 грудня 2010 р.3:56:39
   User: 
   Server: .\
   Database: Fuck
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Themes
	DROP CONSTRAINT FK_Themes_Stages
GO
ALTER TABLE dbo.ThemeType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ThemeType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Stages SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Stages', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Stages', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Stages', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Themes ADD CONSTRAINT
	FK_Themes_Stages FOREIGN KEY
	(
	StageRef
	) REFERENCES dbo.Stages
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Themes ADD CONSTRAINT
	FK_Themes_ThemeType FOREIGN KEY
	(
	ThemeTypeRef
	) REFERENCES dbo.ThemeType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Themes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Themes', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Themes', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Themes', 'Object', 'CONTROL') as Contr_Per 