IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'db_denydatawriter')
EXEC sys.sp_executesql N'CREATE SCHEMA [db_denydatawriter] AUTHORIZATION [db_denydatawriter]'
GO
