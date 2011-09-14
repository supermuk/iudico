IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_datawriter')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_datawriter] AUTHORIZATION [db_datawriter]'
GO
