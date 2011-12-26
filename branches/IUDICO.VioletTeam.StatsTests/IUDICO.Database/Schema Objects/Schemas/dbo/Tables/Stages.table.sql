CREATE TABLE [dbo].[Stages] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) COLLATE Ukrainian_CI_AS NOT NULL,
    [Created]       DATETIME      NOT NULL,
    [Updated]       DATETIME      NOT NULL,
    [CurriculumRef] INT           NOT NULL,
    [IsDeleted]     BIT           NOT NULL
);

