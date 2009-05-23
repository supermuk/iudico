IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'sys')
EXEC sys.sp_executesql N'CREATE SCHEMA [sys] AUTHORIZATION [sys]'
GO
