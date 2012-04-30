ALTER TABLE [dbo].[CurriculumChapters]
    ADD CONSTRAINT [FK_CurriculumChapters_Chapters] FOREIGN KEY ([ChapterRef]) REFERENCES [dbo].[Chapters] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

