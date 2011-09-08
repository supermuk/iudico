ALTER TABLE [dbo].[Themes]
    ADD CONSTRAINT [FK_Themes_Stages] FOREIGN KEY ([StageRef]) REFERENCES [dbo].[Stages] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

