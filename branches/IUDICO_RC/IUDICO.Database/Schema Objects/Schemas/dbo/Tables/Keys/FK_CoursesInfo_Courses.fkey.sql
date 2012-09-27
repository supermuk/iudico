ALTER TABLE [dbo].[CoursesInfo]
    ADD CONSTRAINT [FK_CoursesInfo_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

