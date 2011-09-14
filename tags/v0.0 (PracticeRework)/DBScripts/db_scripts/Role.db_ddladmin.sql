IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_ddladmin' AND type = 'R')
CREATE ROLE [db_ddladmin] AUTHORIZATION [dbo]
GO
