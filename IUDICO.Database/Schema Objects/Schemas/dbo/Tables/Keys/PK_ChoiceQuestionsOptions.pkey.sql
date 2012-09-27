﻿ALTER TABLE [dbo].[ChoiceQuestionsOptions]
    ADD CONSTRAINT [PK_ChoiceQuestionsOptions] PRIMARY KEY CLUSTERED ([QuestionId] ASC, [Option] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

