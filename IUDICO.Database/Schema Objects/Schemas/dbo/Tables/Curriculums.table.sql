CREATE TABLE [dbo].[Curriculums] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [UserGroupRef]  INT      NOT NULL,
    [DisciplineRef] INT      NOT NULL,
    [StartDate]     DATETIME NULL,
    [EndDate]       DATETIME NULL,
    [IsDeleted]     BIT      NOT NULL,
    [IsValid]       BIT      NOT NULL
);









