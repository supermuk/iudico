IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_datareader')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_datareader] AUTHORIZATION [db_datareader]'
GO
