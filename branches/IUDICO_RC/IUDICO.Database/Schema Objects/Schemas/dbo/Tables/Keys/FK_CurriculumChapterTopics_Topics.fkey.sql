ALTER TABLE [dbo].[CurriculumChapterTopics]
    ADD CONSTRAINT [FK_CurriculumChapterTopics_Topics] FOREIGN KEY ([TopicRef]) REFERENCES [dbo].[Topics] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

