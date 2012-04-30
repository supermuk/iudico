ALTER TABLE [dbo].[CurriculumChapters]
    ADD CONSTRAINT [DF_CurriculumChapters_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

