ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_TestsSum] DEFAULT ((0)) FOR [TestsSum];

