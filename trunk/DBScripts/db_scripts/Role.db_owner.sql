IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_owner' AND type = 'R')
CREATE ROLE [db_owner] AUTHORIZATION [dbo]
GO
