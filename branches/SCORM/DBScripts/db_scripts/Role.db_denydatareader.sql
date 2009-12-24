IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_denydatareader' AND type = 'R')
CREATE ROLE [db_denydatareader] AUTHORIZATION [dbo]
GO
