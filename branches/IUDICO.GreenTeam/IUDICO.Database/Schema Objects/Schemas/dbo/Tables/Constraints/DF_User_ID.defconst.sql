ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_ID] DEFAULT (newsequentialid()) FOR [Id];

