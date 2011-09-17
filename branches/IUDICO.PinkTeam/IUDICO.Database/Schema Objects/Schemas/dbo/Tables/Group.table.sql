CREATE TABLE [dbo].[Group] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) COLLATE Ukrainian_CI_AS NOT NULL,
    [Deleted] BIT           NOT NULL
);



