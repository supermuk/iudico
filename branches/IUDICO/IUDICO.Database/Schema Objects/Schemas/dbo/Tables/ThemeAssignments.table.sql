CREATE TABLE [dbo].[ThemeAssignments] (
    [Id]                      INT IDENTITY (1, 1) NOT NULL,
    [ThemeRef]                INT NOT NULL,
    [CurriculumAssignmentRef] INT NOT NULL,
    [MaxScore]                INT NOT NULL,
    [IsDeleted]               BIT NOT NULL
);



