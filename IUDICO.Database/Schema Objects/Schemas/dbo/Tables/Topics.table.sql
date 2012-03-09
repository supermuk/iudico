CREATE TABLE [dbo].[Topics] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (50) NOT NULL,
    [ChapterRef]         INT           NOT NULL,
    [TestCourseRef]      INT           NULL,
    [TestTopicTypeRef]   INT           NULL,
    [TheoryCourseRef]    INT           NULL,
    [TheoryTopicTypeRef] INT           NULL,
    [SortOrder]          INT           NOT NULL,
    [Created]            DATETIME      NOT NULL,
    [Updated]            DATETIME      NOT NULL,
    [IsDeleted]          BIT           NOT NULL
);



