ALTER TABLE [dbo].[Themes]
    ADD CONSTRAINT [FK_Themes_ThemeType] FOREIGN KEY ([ThemeTypeRef]) REFERENCES [dbo].[ThemeTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;



