IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_accessadmin' AND type = 'R')
CREATE ROLE [db_accessadmin] AUTHORIZATION [dbo]
GO
