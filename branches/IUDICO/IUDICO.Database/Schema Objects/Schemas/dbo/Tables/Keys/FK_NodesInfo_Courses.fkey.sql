ALTER TABLE [dbo].[NodesInfo]
    ADD CONSTRAINT [FK_NodesInfo_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;



