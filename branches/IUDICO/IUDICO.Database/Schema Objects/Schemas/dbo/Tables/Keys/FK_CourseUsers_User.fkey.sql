ALTER TABLE [dbo].[CourseUsers]
    ADD CONSTRAINT [FK_CourseUsers_User] FOREIGN KEY ([UserRef]) REFERENCES [dbo].[User] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

