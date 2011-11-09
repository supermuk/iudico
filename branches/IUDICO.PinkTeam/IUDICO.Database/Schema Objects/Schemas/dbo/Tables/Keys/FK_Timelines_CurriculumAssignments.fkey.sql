ALTER TABLE [dbo].[Timelines]
    ADD CONSTRAINT [FK_Timelines_CurriculumAssignments] FOREIGN KEY ([CurriculumAssignmentRef]) REFERENCES [dbo].[CurriculumAssignments] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

