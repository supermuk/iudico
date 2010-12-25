USE [IUDICO]
GO
/*
   25 December 201017:39:49
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
ALTER TABLE dbo.[User]
	DROP CONSTRAINT DF_User_ID
GO
CREATE TABLE dbo.Tmp_User
	(
	Id uniqueidentifier NOT NULL,
	Username nvarchar(100) COLLATE Ukrainian_CI_AS NOT NULL,
	Password nvarchar(50) COLLATE Ukrainian_CI_AS NOT NULL,
	Email nvarchar(100) COLLATE Ukrainian_CI_AS NOT NULL,
	OpenId nvarchar(200) COLLATE Ukrainian_CI_AS NOT NULL,
	Name nvarchar(200) COLLATE Ukrainian_CI_AS NOT NULL,
	IsApproved bit NOT NULL,
	RoleRef int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_User SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_User ADD CONSTRAINT
	DF_User_ID DEFAULT (newsequentialid()) FOR Id
GO
IF EXISTS(SELECT * FROM dbo.[User])
	 EXEC('INSERT INTO dbo.Tmp_User (Id, Username, Password, Email, OpenId, Name, IsApproved)
		SELECT Id, Username, Password, Email, OpenId, Name, IsApproved FROM dbo.[User] WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.GroupUsers
	DROP CONSTRAINT FK_GroupUsers_User
GO
ALTER TABLE dbo.RoleUsers
	DROP CONSTRAINT FK_RoleUsers_User
GO
DROP TABLE dbo.[User]
GO
EXECUTE sp_rename N'dbo.Tmp_User', N'User', 'OBJECT' 
GO
ALTER TABLE dbo.[User] ADD CONSTRAINT
	PK_User PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.[User]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.RoleUsers ADD CONSTRAINT
	FK_RoleUsers_User FOREIGN KEY
	(
	UserRef
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.RoleUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.RoleUsers', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.RoleUsers', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.RoleUsers', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.GroupUsers ADD CONSTRAINT
	FK_GroupUsers_User FOREIGN KEY
	(
	UserRef
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GroupUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

select Has_Perms_By_Name(N'dbo.GroupUsers', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.GroupUsers', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.GroupUsers', 'Object', 'CONTROL') as Contr_Per 