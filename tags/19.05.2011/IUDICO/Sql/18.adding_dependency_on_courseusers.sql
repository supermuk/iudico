/*
   2 січня 2011 р.18:24:11
   User: 
   Server: SM-PC\SQLEXPRESS
   Database: IUDICO
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
COMMIT
select Has_Perms_By_Name(N'dbo.[User]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Courses', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Courses', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Courses', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CourseUsers
	DROP CONSTRAINT FK_CourseUsers_CourseUsers
GO
ALTER TABLE dbo.CourseUsers ADD CONSTRAINT
	FK_CourseUsers_CourseUsers FOREIGN KEY
	(
	CourseRef
	) REFERENCES dbo.Courses
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CourseUsers ADD CONSTRAINT
	FK_CourseUsers_User FOREIGN KEY
	(
	UserRef
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CourseUsers', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CourseUsers', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CourseUsers', 'Object', 'CONTROL') as Contr_Per 