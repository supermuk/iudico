﻿ALTER TABLE [dbo].[UserScores]
    ADD CONSTRAINT [FK_UserScores_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

