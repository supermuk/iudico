ALTER TABLE [dbo].[QuestionsInfo]
    ADD CONSTRAINT [FK_QuestionsInfo_QuestionTypes] FOREIGN KEY ([Type]) REFERENCES [dbo].[QuestionTypes] ([Type]) ON DELETE NO ACTION ON UPDATE NO ACTION;

