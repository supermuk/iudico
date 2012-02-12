CREATE TABLE [dbo].[Topics] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [Created]      DATETIME      NOT NULL,
    [Updated]      DATETIME      NOT NULL,
    [ChapterRef]   INT           NOT NULL,
    [CourseRef]    INT           NULL,
    [SortOrder]    INT           NOT NULL,
    [TopicTypeRef] INT           NOT NULL,
    [IsDeleted]    BIT           NOT NULL
);

