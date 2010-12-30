ALTER TABLE [dbo].[GroupUsers]
    ADD CONSTRAINT [FK_GroupUsers_Group] FOREIGN KEY ([GroupRef]) REFERENCES [dbo].[Group] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

