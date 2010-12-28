USE [IUDICO]
GO
BEGIN TRANSACTION
SET IDENTITY_INSERT [dbo].[Operation] ON
INSERT INTO [dbo].[Operation] ([Id], [Name]) VALUES (1, 'View')
GO
INSERT INTO [dbo].[Operation] ([Id], [Name]) VALUES (2, 'Pass')
GO
SET IDENTITY_INSERT [dbo].[Operation] OFF
COMMIT