-- =============================================
-- Script Template
-- =============================================
/*
This script was created by Visual Studio on 23.01.2011 at 22:36.
Run this script on [THP-NETBOOK\SQLEXPRESS.IUDICO.Empty] to make it the same as [THP-NETBOOK\SQLEXPRESS.IUDICO.Admin].
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
UPDATE [dbo].[User] SET [dbo].[User].[ApprovedBy] = null WHERE [dbo].[User].[ApprovedBy] = N'd47e8c09-2827-e011-840f-93b2f3060fee'
COMMIT TRANSACTION
BEGIN TRANSACTION
DELETE FROM [dbo].[User] WHERE [dbo].[User].[Id] = N'd47e8c09-2827-e011-840f-93b2f3060fee' AND [dbo].[User].[Username] = N'lex'
COMMIT TRANSACTION
BEGIN TRANSACTION
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Email], [OpenId], [Name], [IsApproved], [Deleted], [UserID]) VALUES (N'd47e8c09-2827-e011-840f-93b2f3060fee', N'lex', N'D1F3732A9A6A6D5AE438388E1DF2164BDB35D371', N'lex@iudico', N'panzarulz.livejournal.com', 1, 3, 0, N'ADMIN 000001')
COMMIT TRANSACTION
BEGIN TRANSACTION
INSERT INTO [dbo].[UserRoles] (UserRef, RoleRef) VALUES(N'd47e8c09-2827-e011-840f-93b2f3060fee', 4);
COMMIT TRANSACTION
BEGIN TRANSACTION
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Email], [OpenId], [Name], [IsApproved], [Deleted], [UserID]) VALUES (N'fd29cab2-a7c3-4e2a-bc44-f2fb2717254e', N'prof', N'D9F02D46BE016F1B301F7C02A4B9C4EBE0DDE7EF', N'prof@mail.com', N'prof', N'prof', 3, 0, N'prof 000001')
COMMIT TRANSACTION
BEGIN TRANSACTION
INSERT INTO [dbo].[UserRoles] (UserRef, RoleRef) VALUES(N'fd29cab2-a7c3-4e2a-bc44-f2fb2717254e', 2);
COMMIT TRANSACTION
BEGIN TRANSACTION
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Email], [OpenId], [Name], [IsApproved], [Deleted], [UserID]) VALUES (N'fc7dc1d0-863d-46e1-90b6-c963f0ce1008', N'prof2', N'F90FFB852C92DD9CAD2B643F8E96E41222C0AA85', N'prof2@mail.com', N'prof2', N'prof2', 3, 0, N'prof 000002')
COMMIT TRANSACTION
BEGIN TRANSACTION
INSERT INTO [dbo].[UserRoles] (UserRef, RoleRef) VALUES(N'fc7dc1d0-863d-46e1-90b6-c963f0ce1008', 2);
COMMIT TRANSACTION
BEGIN TRANSACTION
UPDATE [dbo].[User] SET [dbo].[User].[ApprovedBy] = N'd47e8c09-2827-e011-840f-93b2f3060fee' WHERE [dbo].[User].[ApprovedBy] is null AND [dbo].[User].[IsApproved] = 1
COMMIT TRANSACTION

/****** Object:  Table [dbo].[ThemeTypes]    Script Date: 01/25/2011 01:34:49 ******/
DELETE FROM [dbo].[TopicTypes]
GO
/****** Object:  Table [dbo].[ThemeTypes]    Script Date: 01/25/2011 01:34:49 ******/
INSERT [dbo].[TopicTypes] ([Id], [Name]) VALUES (1, N'Test')
INSERT [dbo].[TopicTypes] ([Id], [Name]) VALUES (2, N'Theory')
INSERT [dbo].[TopicTypes] ([Id], [Name]) VALUES (3, N'TestWithoutCourse')
