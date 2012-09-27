ALTER TABLE [dbo].[ChoiceQuestionsOptions]
    ADD CONSTRAINT [FK_ChoiceQuestionsOptions_QuestionsInfo] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[QuestionsInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

