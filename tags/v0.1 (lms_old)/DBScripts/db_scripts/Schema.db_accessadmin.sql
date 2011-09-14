IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_accessadmin')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_accessadmin] AUTHORIZATION [db_accessadmin]'
GO
