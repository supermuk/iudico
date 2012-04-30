ALTER TABLE [dbo].[SharedDisciplines]
    ADD CONSTRAINT [FK_SharedDisciplines_Disciplines] FOREIGN KEY ([DisciplineRef]) REFERENCES [dbo].[Disciplines] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

