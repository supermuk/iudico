ALTER TABLE [dbo].[NodeResources]
    ADD CONSTRAINT [FK_NodeResources_Nodes] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[Nodes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

