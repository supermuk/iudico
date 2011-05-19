USE [IUDICO]
GO
BEGIN TRANSACTION
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT INTO [dbo].[Roles] ([Id], [Name], [ParentId]) VALUES (1, 'Student', NULL)
GO
INSERT INTO [dbo].[Roles] ([Id], [Name], [ParentId]) VALUES (2, 'Teacher', 1)
GO
INSERT INTO [dbo].[Roles] ([Id], [Name], [ParentId]) VALUES (3, 'Admin', 2)
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
COMMIT