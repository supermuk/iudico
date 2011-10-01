CREATE TABLE [dbo].[ImageResources] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [CourseId] INT           NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [FileName] NVARCHAR (50) NOT NULL
);

