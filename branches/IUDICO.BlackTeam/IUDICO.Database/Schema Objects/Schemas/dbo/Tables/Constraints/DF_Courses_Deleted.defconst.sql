ALTER TABLE [dbo].[Courses]
    ADD CONSTRAINT [DF_Courses_Deleted] DEFAULT ((0)) FOR [Deleted];

