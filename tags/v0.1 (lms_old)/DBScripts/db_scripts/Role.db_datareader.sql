IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_datareader' AND type = 'R')
CREATE ROLE [db_datareader] AUTHORIZATION [dbo]
GO
