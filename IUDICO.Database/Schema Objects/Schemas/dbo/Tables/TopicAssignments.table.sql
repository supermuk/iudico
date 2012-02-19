CREATE TABLE [dbo].[TopicAssignments] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [TopicRef]      INT NOT NULL,
    [CurriculumRef] INT NOT NULL,
    [MaxScore]      INT NOT NULL,
    [IsDeleted]     BIT NOT NULL
);

