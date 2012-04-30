CREATE TABLE [dbo].[CurriculumChapters] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [CurriculumRef] INT      NOT NULL,
    [ChapterRef]    INT      NOT NULL,
    [StartDate]     DATETIME NULL,
    [EndDate]       DATETIME NULL,
    [IsDeleted]     BIT      NOT NULL
);

