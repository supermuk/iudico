ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_TestsTotal] DEFAULT ((0)) FOR [TestsTotal];

