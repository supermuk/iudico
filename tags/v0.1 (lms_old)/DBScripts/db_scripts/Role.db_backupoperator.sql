IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_backupoperator' AND type = 'R')
CREATE ROLE [db_backupoperator] AUTHORIZATION [dbo]
GO
