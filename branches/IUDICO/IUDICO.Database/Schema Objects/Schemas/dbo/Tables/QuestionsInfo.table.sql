CREATE TABLE [dbo].[QuestionsInfo] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [NodeId]   INT            NOT NULL,
    [Text]     NVARCHAR (800) NOT NULL,
    [MaxScore] REAL           NOT NULL,
    [Type]     INT            NOT NULL
);



