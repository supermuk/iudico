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
DELETE FROM [dbo].[User] WHERE [dbo].[User].[Id] = N'd47e8c09-2827-e011-840f-93b2f3060fee' AND [dbo].[User].[Username] = N'lex'
COMMIT TRANSACTION
