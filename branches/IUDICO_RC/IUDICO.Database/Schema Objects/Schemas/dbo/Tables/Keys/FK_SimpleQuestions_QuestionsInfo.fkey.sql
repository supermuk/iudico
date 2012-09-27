ALTER TABLE [dbo].[SimpleQuestions]
    ADD CONSTRAINT [FK_SimpleQuestions_QuestionsInfo] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[QuestionsInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

