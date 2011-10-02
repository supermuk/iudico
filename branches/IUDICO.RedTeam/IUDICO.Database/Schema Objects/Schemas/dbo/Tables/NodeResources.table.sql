CREATE TABLE [dbo].[NodeResources] (
    [Id]     INT            NOT NULL,
    [NodeId] INT            NOT NULL,
    [Type]   NVARCHAR (50)  NOT NULL,
    [Name]   NVARCHAR (50)  NOT NULL,
    [Path]   NVARCHAR (MAX) NOT NULL
);

