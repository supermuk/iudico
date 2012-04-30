ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_Deleted] DEFAULT ((0)) FOR [Deleted];

