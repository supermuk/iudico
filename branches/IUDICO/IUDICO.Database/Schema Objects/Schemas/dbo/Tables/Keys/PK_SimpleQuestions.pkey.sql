﻿ALTER TABLE [dbo].[SimpleQuestions]
    ADD CONSTRAINT [PK_SimpleQuestions] PRIMARY KEY CLUSTERED ([QuestionId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
