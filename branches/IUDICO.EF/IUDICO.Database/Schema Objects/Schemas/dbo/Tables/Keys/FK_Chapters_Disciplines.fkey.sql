ALTER TABLE [dbo].[Chapters]
    ADD CONSTRAINT [FK_Chapters_Disciplines] FOREIGN KEY ([DisciplineRef]) REFERENCES [dbo].[Disciplines] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

