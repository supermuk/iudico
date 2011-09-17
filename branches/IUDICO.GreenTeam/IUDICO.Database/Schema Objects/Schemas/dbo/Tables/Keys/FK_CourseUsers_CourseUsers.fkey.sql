ALTER TABLE [dbo].[CourseUsers]
    ADD CONSTRAINT [FK_CourseUsers_CourseUsers] FOREIGN KEY ([CourseRef]) REFERENCES [dbo].[Courses] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

