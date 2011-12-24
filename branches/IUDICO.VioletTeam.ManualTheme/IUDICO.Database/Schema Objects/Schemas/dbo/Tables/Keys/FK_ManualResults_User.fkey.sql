ALTER TABLE [dbo].[ManualResults]  WITH CHECK ADD  CONSTRAINT [FK_ManualResults_User] FOREIGN KEY([UserRef])
REFERENCES [dbo].[User] ([Id])


GO
ALTER TABLE [dbo].[ManualResults] CHECK CONSTRAINT [FK_ManualResults_User]

