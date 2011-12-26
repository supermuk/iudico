ALTER TABLE [dbo].[Nodes]
    ADD CONSTRAINT [FK_Nodes_Cources] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

