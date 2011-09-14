IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'dbo')
EXEC sys.sp_executesql N'CREATE SCHEMA [dbo] AUTHORIZATION [dbo]'
GO
