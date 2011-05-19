USE [IUDICO]
GO
/*
   12 December 201013:25:43
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
EXECUTE sp_rename N'dbo.[User].ID', N'Tmp_Id_5', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[User].OpenID', N'Tmp_OpenId_6', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[User].Tmp_Id_5', N'Id', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[User].Tmp_OpenId_6', N'OpenId', 'COLUMN' 
GO
ALTER TABLE dbo.[User] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[User]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'CONTROL') as Contr_Per 