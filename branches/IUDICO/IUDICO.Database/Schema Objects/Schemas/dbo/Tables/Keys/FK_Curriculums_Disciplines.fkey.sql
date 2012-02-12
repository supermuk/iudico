ALTER TABLE [dbo].[Curriculums]
    ADD CONSTRAINT [FK_Curriculums_Disciplines] FOREIGN KEY ([DisciplineRef]) REFERENCES [dbo].[Disciplines] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

