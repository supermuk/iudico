ALTER TABLE [dbo].[CompiledTestQuestions]
    ADD CONSTRAINT [FK_CompiledTestQuestions_QuestionsInfo] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[QuestionsInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

