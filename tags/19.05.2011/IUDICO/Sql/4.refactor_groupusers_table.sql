USE [IUDICO]
GO
/*
   12 December 201013:26:59
   User: 
   Server: .\SQLEXPRESS
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
EXECUTE sp_rename N'dbo.GroupUsers.GroupID', N'Tmp_GroupRef_8', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.GroupUsers.UserID', N'Tmp_UserRef_9', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.GroupUsers.Tmp_GroupRef_8', N'GroupRef', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.GroupUsers.Tmp_UserRef_9', N'UserRef', 'COLUMN' 
GO
ALTER TABLE dbo.GroupUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.GroupUsers', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.GroupUsers', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.GroupUsers', 'Object', 'CONTROL') as Contr_Per 