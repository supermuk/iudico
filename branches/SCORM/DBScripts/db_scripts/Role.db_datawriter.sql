IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_datawriter' AND type = 'R')
CREATE ROLE [db_datawriter] AUTHORIZATION [dbo]
GO
