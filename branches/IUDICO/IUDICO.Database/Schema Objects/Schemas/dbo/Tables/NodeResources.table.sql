CREATE TABLE [dbo].[NodeResources] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [NodeId] INT            NULL,
    [Type]   INT            NULL,
    [Name]   NVARCHAR (50)  NOT NULL,
    [Path]   NVARCHAR (MAX) NOT NULL
);



