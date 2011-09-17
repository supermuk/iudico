ALTER TABLE [dbo].[GroupUsers]
    ADD CONSTRAINT [FK_GroupUsers_User] FOREIGN KEY ([UserRef]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE;







