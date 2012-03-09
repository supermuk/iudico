ALTER TABLE [dbo].[CurriculumChapterTopics]
    ADD CONSTRAINT [DF_CurriculumChapterTopics_BlockCurriculumAtTesting] DEFAULT ((0)) FOR [BlockCurriculumAtTesting];

