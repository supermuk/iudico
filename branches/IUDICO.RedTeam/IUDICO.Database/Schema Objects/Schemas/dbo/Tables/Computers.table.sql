CREATE TABLE [dbo].[Computers] (
    [Id]          INT            NOT NULL,
    [IpAddress]   NVARCHAR (20)  NOT NULL,
    [Banned]      BIT            NOT NULL,
    [CurrentUser] NVARCHAR (100) NULL,
    [RoomRef]     INT            NULL
);

