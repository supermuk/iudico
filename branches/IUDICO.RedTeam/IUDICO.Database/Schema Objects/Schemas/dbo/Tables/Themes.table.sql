CREATE TABLE [dbo].[Themes] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [Created]      DATETIME      NOT NULL,
    [Updated]      DATETIME      NOT NULL,
    [StageRef]     INT           NOT NULL,
    [CourseRef]    INT           NOT NULL,
    [SortOrder]    INT           NOT NULL,
    [ThemeTypeRef] INT           NOT NULL,
    [IsDeleted]    BIT           NOT NULL
);

