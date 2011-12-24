ALTER TABLE [dbo].[ManualResults]  WITH CHECK ADD  CONSTRAINT [FK_ManualResults_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[Themes] ([Id])


GO
ALTER TABLE [dbo].[ManualResults] CHECK CONSTRAINT [FK_ManualResults_Themes]

