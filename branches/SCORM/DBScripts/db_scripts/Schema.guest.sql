IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'guest')
EXEC sys.sp_executesql N'CREATE SCHEMA [guest] AUTHORIZATION [guest]'
GO
