ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_Created] DEFAULT (getdate()) FOR [Created];

