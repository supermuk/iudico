﻿CREATE TABLE [dbo].[Roles] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) COLLATE Ukrainian_CI_AS NOT NULL,
    [ParentId] INT           NULL
);

