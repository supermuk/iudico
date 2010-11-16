IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_securityadmin' AND type = 'R')
CREATE ROLE [db_securityadmin] AUTHORIZATION [dbo]
GO
