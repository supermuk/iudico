ALTER TABLE [dbo].[TopicTags]
    ADD CONSTRAINT [FK_TopicFeatures_Features] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

