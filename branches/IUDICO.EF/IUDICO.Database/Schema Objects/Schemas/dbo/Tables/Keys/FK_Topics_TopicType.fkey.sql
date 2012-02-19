ALTER TABLE [dbo].[Topics]
    ADD CONSTRAINT [FK_Topics_TopicType] FOREIGN KEY ([TopicTypeRef]) REFERENCES [dbo].[TopicTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

