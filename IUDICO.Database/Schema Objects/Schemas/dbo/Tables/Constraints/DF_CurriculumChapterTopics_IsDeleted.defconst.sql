ALTER TABLE [dbo].[CurriculumChapterTopics]
    ADD CONSTRAINT [DF_CurriculumChapterTopics_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

