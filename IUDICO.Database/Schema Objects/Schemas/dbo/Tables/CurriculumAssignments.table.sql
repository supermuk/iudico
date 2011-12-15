CREATE TABLE [dbo].[CurriculumAssignments] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [UserGroupRef]  INT NOT NULL,
    [CurriculumRef] INT NOT NULL,
    [IsDeleted]     BIT NOT NULL,
    [IsValid]       BIT NOT NULL
);



