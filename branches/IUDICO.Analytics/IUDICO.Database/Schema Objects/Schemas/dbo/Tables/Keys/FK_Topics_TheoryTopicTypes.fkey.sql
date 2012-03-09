ALTER TABLE [dbo].[Topics]
    ADD CONSTRAINT [FK_Topics_TheoryTopicTypes] FOREIGN KEY ([TheoryTopicTypeRef]) REFERENCES [dbo].[TopicTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

