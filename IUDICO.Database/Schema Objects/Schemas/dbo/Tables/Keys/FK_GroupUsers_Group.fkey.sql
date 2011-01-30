ALTER TABLE [dbo].[GroupUsers]
    ADD CONSTRAINT [FK_GroupUsers_Group] FOREIGN KEY ([GroupRef]) REFERENCES [dbo].[Group] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE;







