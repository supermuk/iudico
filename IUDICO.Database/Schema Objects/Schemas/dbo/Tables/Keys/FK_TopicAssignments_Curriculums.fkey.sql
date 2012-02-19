ALTER TABLE [dbo].[TopicAssignments]
    ADD CONSTRAINT [FK_TopicAssignments_Curriculums] FOREIGN KEY ([CurriculumRef]) REFERENCES [dbo].[Curriculums] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

