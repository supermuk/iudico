ALTER TABLE [dbo].[Topics]
    ADD CONSTRAINT [FK_Topics_TestTopicTypes] FOREIGN KEY ([TestTopicTypeRef]) REFERENCES [dbo].[TopicTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

