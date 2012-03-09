ALTER TABLE [dbo].[CurriculumChapters]
    ADD CONSTRAINT [FK_CurriculumChapters_Curriculums] FOREIGN KEY ([CurriculumRef]) REFERENCES [dbo].[Curriculums] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

