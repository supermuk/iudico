IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_owner')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_owner] AUTHORIZATION [db_owner]'
GO
