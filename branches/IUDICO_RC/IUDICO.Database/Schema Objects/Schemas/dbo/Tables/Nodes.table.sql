CREATE TABLE [dbo].[Nodes] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (500) COLLATE Ukrainian_CI_AS NOT NULL,
    [CourseId]   INT           NOT NULL,
    [ParentId]   INT           NULL,
    [IsFolder]   BIT           NOT NULL,
    [Position]   INT           NOT NULL,
    [Sequencing] XML           NULL
);











