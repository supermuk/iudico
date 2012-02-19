ALTER TABLE [dbo].[Topics]
    ADD CONSTRAINT [FK_Topics_Chapters] FOREIGN KEY ([ChapterRef]) REFERENCES [dbo].[Chapters] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

