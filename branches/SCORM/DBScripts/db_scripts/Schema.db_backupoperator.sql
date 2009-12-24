IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_backupoperator')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_backupoperator] AUTHORIZATION [db_backupoperator]'
GO
