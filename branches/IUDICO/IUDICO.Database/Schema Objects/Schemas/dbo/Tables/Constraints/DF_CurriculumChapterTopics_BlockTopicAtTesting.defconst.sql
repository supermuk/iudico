ALTER TABLE [dbo].[CurriculumChapterTopics]
    ADD CONSTRAINT [DF_CurriculumChapterTopics_BlockTopicAtTesting] DEFAULT ((0)) FOR [BlockTopicAtTesting];

