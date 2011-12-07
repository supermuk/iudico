ALTER TABLE [dbo].[Computers]
    ADD CONSTRAINT [FK_Computers_Rooms] FOREIGN KEY ([RoomRef]) REFERENCES [dbo].[Rooms] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

