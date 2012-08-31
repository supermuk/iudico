ALTER TABLE [dbo].[QuestionsInfo]
    ADD CONSTRAINT [FK_QuestionsInfo_QuestionsInfo] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[NodesInfo] ([NodeId]) ON DELETE NO ACTION ON UPDATE NO ACTION;



