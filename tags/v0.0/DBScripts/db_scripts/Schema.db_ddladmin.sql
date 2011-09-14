IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_ddladmin')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_ddladmin] AUTHORIZATION [db_ddladmin]'
GO
