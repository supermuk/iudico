CREATE TABLE [dbo].[Nodes] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [Name]                         NVARCHAR (50) COLLATE Ukrainian_CI_AS NOT NULL,
    [CourseId]                     INT           NOT NULL,
    [ParentId]                     INT           NULL,
    [IsFolder]                     BIT           NOT NULL,
    [Position]                     INT           NOT NULL,
    [SequencingPattern]            INT           NULL,
    [Choise]                       BIT           NULL,
    [ChoiseExit]                   BIT           NULL,
    [Flow]                         BIT           NULL,
    [ForwardOnly]                  BIT           NULL,
    [AttemptLimit]                 INT           NULL,
    [AttemptAbsoluteDurationLimit] NVARCHAR (50) NULL,
    [Sequencing]                   XML           NULL
);









