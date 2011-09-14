ALTER TABLE [dbo].[CurriculumAssignments]
    ADD CONSTRAINT [DF_CurriculumAssignments_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

