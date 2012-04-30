ALTER TABLE [dbo].[CurriculumChapterTopics]
    ADD CONSTRAINT [FK_CurriculumChapterTopics_CurriculumChapters] FOREIGN KEY ([CurriculumChapterRef]) REFERENCES [dbo].[CurriculumChapters] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

