ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_CreationDate] DEFAULT (getdate()) FOR [CreationDate];

