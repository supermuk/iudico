CREATE TABLE [dbo].[Timeline] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [StartDate]               DATETIME NOT NULL,
    [EndDate]                 DATETIME NOT NULL,
    [CurriculumAssignmentRef] INT      NOT NULL,
    [OperationRef]            INT      NOT NULL,
    [StageRef]                INT      NULL,
    [IsDeleted]               BIT      NOT NULL
);

