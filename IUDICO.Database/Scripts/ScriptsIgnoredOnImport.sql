
BEGIN TRANSACTION
GO
SET QUOTED_IDENTIFIER ON
GO
SET ARITHABORT ON
GO
SET NUMERIC_ROUNDABORT OFF
GO
SET CONCAT_NULL_YIELDS_NULL ON
GO
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET ANSI_WARNINGS ON
GO
COMMIT
GO
BEGIN TRANSACTION
GO
COMMIT
GO
select Has_Perms_By_Name(N'dbo.ForecastingTree', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ForecastingTree', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ForecastingTree', 'Object', 'CONTROL') as Contr_Per
GO
--Syntax Error: Incorrect syntax near SET.
--ALTER TABLE dbo.ForecastingTree SET (LOCK_ESCALATION = TABLE)



GO
