ALTER TABLE [dbo].[ChoiceQuestionsCorrectChoices]
    ADD CONSTRAINT [FK_ChoiceQuestionsCorrectChoices_QuestionsInfo] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[QuestionsInfo] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

