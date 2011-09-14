IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_securityadmin')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_securityadmin] AUTHORIZATION [db_securityadmin]'
GO
