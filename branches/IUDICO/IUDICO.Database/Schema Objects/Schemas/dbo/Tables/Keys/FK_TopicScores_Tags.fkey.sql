﻿ALTER TABLE [dbo].[TopicScores]
    ADD CONSTRAINT [FK_TopicScores_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

