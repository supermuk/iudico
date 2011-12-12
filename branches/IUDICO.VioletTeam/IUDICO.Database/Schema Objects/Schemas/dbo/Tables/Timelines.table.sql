CREATE TABLE [dbo].[Timelines] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [StartDate]               DATETIME NOT NULL,
    [EndDate]                 DATETIME NOT NULL,
    [CurriculumAssignmentRef] INT      NOT NULL,
    [StageRef]                INT      NULL,
    [IsDeleted]               BIT      NOT NULL
);



